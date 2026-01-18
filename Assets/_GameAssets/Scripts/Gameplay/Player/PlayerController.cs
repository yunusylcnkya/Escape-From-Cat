using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Bu sınıf, oyuncunun oyun içindeki hareketlerini kontrol ediyor.
// Yani koşma, zıplama, kayma gibi hareketleri ve hangi durumda olduğunu yönetiyor.
public class PlayerController : MonoBehaviour
{
    // Oyuncu zıpladığında haber vermek için bir olay
    public event Action OnPlayerJump;
    // Oyuncunun durumu değiştiğinde haber vermek için bir olay
    public event Action<PlayerState> OnPlayerStateChangend;

    [Header("References")]
    [SerializeField] private Transform _orientationTransform; // Oyuncunun yönünü bilmek için

    [Header("Movement Settings")]
    [SerializeField] private KeyCode _movementKey; // Hareket tuşu
    [SerializeField] private float _movementSpeed; // Hareket hızı

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey; // Zıplama tuşu
    [SerializeField] private float _jumpForce; // Zıplama kuvveti
    [SerializeField] private float _jumpCooldown; // Zıplama süresi
    [SerializeField] private float _airMultiplieer; // Havadayken hareket kuvveti
    [SerializeField] private float _airDrag;        // Havadayken sürtünme
    [SerializeField] private bool _canJump;         // Zıplayabilir mi?

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slideKey; // Kayma tuşu
    [SerializeField] private float _slideMultiplier; // Kayarken hız çarpanı
    [SerializeField] private float _slideDrag;       // Kayarken sürtünme

    [Header("Ground Check Settings")]
    [SerializeField] private float _playerHeight; // Oyuncu boyu
    [SerializeField] private LayerMask _groundLayer; // Yerin hangi katmanı
    [SerializeField] private float _groundDrag; // Yerdeki sürtünme

    private StateController _stateController; // Oyuncunun durumunu saklayan sınıf
    private Rigidbody _playerRigidBody;       // Oyuncunun fizik hareketini sağlayan bileşen

    private float _startingMovementSpeed; // Başlangıç hızı
    private float _startingJumpForce;     // Başlangıç zıplama kuvveti

    private float _horizontalInput, _verticalInput; // Klavye yön girişleri
    private Vector3 _movementDirection;             // Oyuncunun hareket yönü
    private bool _isSliding;                        // Kayıyor mu?

    void Awake()
    {
        _stateController = GetComponent<StateController>();
        _playerRigidBody = GetComponent<Rigidbody>();
        _playerRigidBody.freezeRotation = true; // Fizik nedeniyle dönmesini engelle
        _startingMovementSpeed = _movementSpeed;
        _startingJumpForce = _jumpForce;
    }

    private void Update()
    {
        // Eğer oyun oynanmıyorsa hiçbir şey yapma
        if ((GameManager.Instance.GetCurrentGameState() != GameState.Play) &&
            (GameManager.Instance.GetCurrentGameState() != GameState.Resume))
        {
            return;
        }

        SetInputs();    // Klavyeden gelen tuşları al
        SetStates();    // Oyuncunun durumunu belirle
        SetPlayerDrag(); // Sürtünmeyi ayarla
        LimitPlayerSpeed(); // Hızı sınırlama
    }

    void FixedUpdate()
    {
        // Fizik hareketini FixedUpdate içinde yap
        if ((GameManager.Instance.GetCurrentGameState() != GameState.Play) &&
            (GameManager.Instance.GetCurrentGameState() != GameState.Resume))
        {
            return;
        }

        SetPlayerMovement(); // Hareket uygula
    }

    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal"); // A-D veya ok tuşları
        _verticalInput = Input.GetAxisRaw("Vertical");     // W-S veya ok tuşları

        if (Input.GetKeyDown(_slideKey)) { _isSliding = true; }
        else if (Input.GetKeyDown(_movementKey)) { _isSliding = false; }
        else if (Input.GetKey(_jumpKey) && _canJump && IsGrounded())
        {
            _canJump = false;
            SetPlayerJumping(); // Zıplama uygula
            Invoke(nameof(ResetJumping), _jumpCooldown); // Zıplama süresi sonra tekrar zıplayabilsin
            AudioManager.Instance.Play(SoundType.JumpSound);
        }
    }

    private void SetStates()
    {
        // Oyuncunun şu anki durumunu belirle (duruyor, koşuyor, kayıyor, zıplıyor)
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var isSliding = IsSliding();
        var currentState = _stateController.GetCurrentState();

        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when !_canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };

        if (newState != currentState)
        {
            _stateController.ChangeState(newState);
            OnPlayerStateChangend?.Invoke(newState);
        }
    }

    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;

        float forceMultiplier = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => _slideMultiplier,
            PlayerState.Jump => _airMultiplieer,
            _ => 1f
        };

        _playerRigidBody.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);
    }

    private void SetPlayerDrag()
    {
        _playerRigidBody.linearDamping = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => _groundDrag,
            PlayerState.Slide => _slideDrag,
            PlayerState.Jump => _airDrag,
            _ => _playerRigidBody.linearDamping
        };
    }

    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidBody.linearVelocity.x, 0f, _playerRigidBody.linearVelocity.z);
        if (flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _playerRigidBody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidBody.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void SetPlayerJumping()
    {
        OnPlayerJump?.Invoke(); // Zıplama animasyonu veya olayı çalışsın
        _playerRigidBody.linearVelocity = new Vector3(_playerRigidBody.linearVelocity.x, 0f, _playerRigidBody.linearVelocity.z);
        _playerRigidBody.AddForce(transform.up * _jumpForce, ForceMode.Impulse); // Zıplama kuvveti uygula
    }

    private void ResetJumping() { _canJump = true; }

    #region Helper Functions
    private bool IsGrounded()
    {
        // Oyuncu yerde mi kontrolü
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }

    private Vector3 GetMovementDirection() { return _movementDirection.normalized; }
    private bool IsSliding() { return _isSliding; }

    public void SetMovementSpeed(float speed, float duration)
    {
        _movementSpeed += speed;
        Invoke(nameof(ResetMovementSpeed), duration);
    }
    private void ResetMovementSpeed() { _movementSpeed = _startingMovementSpeed; }

    public void SetJumpForce(float force, float duration)
    {
        _jumpForce += force;
        Invoke(nameof(ResetJumpForce), duration);
    }
    private void ResetJumpForce() { _jumpForce = _startingJumpForce; }

    public Rigidbody GetPlayerRigidbody() { return _playerRigidBody; }

    public bool CanCatChase()
    {
        // Oyuncu yerdeyse kedi peşinden koşabilir
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _playerHeight * 0.5f + 0.2f))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Consts.Layers.FLOOR_LAYER))
                return true;
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Consts.Layers.GROUND_LAYER))
                return false;
        }
        return false;
    }
    #endregion
}
