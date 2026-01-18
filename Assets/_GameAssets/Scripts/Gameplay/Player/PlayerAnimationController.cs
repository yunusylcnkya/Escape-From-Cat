using System;
using UnityEngine;

// Bu sınıf, oyundaki oyuncunun animasyonlarını kontrol ediyor.
// Yani oyuncu duruyorsa, hareket ediyorsa, zıplıyorsa veya kayıyorsa doğru animasyonları oynatıyor.
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator; // Oyuncunun animatörünü tutuyor
    private PlayerController _playerController;       // Oyuncunun hareketlerini kontrol eden sınıf
    private StateController _stateController;         // Oyuncunun mevcut durumunu tutan sınıf

    private void Awake()
    {
        // Bu objeden diğer gerekli bileşenleri alıyoruz
        _playerController = GetComponent<PlayerController>();
        _stateController = GetComponent<StateController>();
    }

    void Start()
    {
        // Oyuncu zıpladığında PlayerController bize haber veriyor
        _playerController.OnPlayerJump += PlayerController_OnPlayerJumped;
    }

    // Oyuncu zıpladığında çağrılan fonksiyon
    private void PlayerController_OnPlayerJumped()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true); // Zıplama animasyonunu aç
        Invoke(nameof(ResetJumping), 0.5f); // 0.5 saniye sonra zıplama animasyonunu kapat
    }

    private void ResetJumping()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false); // Zıplama animasyonunu kapat
    }

    private void Update()
    {
        // Eğer oyun oynanmıyorsa hiçbir şey yapma
        if ((GameManager.Instance.GetCurrentGameState() != GameState.Play) &&
            (GameManager.Instance.GetCurrentGameState() != GameState.Resume))
        {
            return;
        }

        SetPlayerAnimations(); // Oyuncunun durumuna göre animasyonları güncelle
    }

    private void SetPlayerAnimations()
    {
        var currentState = _stateController.GetCurrentState(); // Oyuncunun mevcut durumunu al

        switch (currentState)
        {
            case PlayerState.Idle:
                // Duruyorsa hiçbir hareket animasyonu açık olmasın
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                break;

            case PlayerState.Move:
                // Oyuncu hareket ediyorsa hareket animasyonu açılsın
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                break;

            case PlayerState.SlideIdle:
                // Oyuncu kayma pozisyonundayken duruyorsa kayma animasyonu açık olsun ama aktif olmasın
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                break;

            case PlayerState.Slide:
                // Oyuncu kayıyorsa kayma animasyonu ve aktif kayma animasyonu açılsın
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                break;
        }
    }
}
