using System;
using UnityEngine;
using UnityEngine.AI;

// Bu sınıf, oyundaki kedimizin hareketlerini kontrol ediyor.
// Kedi ya etrafta geziyor (patrol), ya da oyuncuyu gördüğünde peşinden koşuyor (chase).
public class CatController : MonoBehaviour
{
    // Eğer kedi oyuncuya yetişirse bu olayı çalıştırıyoruz
    public event Action OnCatCatched;

    [Header("References")]
    [SerializeField] private PlayerController _playerController; // Oyuncuyu kontrol eden sınıf
    [SerializeField] private Transform _playerTransform;          // Oyuncunun konumu

    [Header("Settings")]
    [SerializeField] private float _defaultSpeed = 5f;  // Kedi normal gezerken hızı
    [SerializeField] private float _chaseSpeed = 7f;    // Kedi oyuncuyu kovalarken hızı

    [Header("Navigation Settings")]
    [SerializeField] private float _patrolRadius = 10f;        // Kedinin dolaşabileceği alan
    [SerializeField] private float _waitTime = 2f;             // Kedinin duraklama süresi
    [SerializeField] private int _maxDestinationAttempts = 5;  // Hedef bulma deneme sayısı
    [SerializeField] private float _chaseDistanceThreshold = 1.5f; // Peşinden koşarken durma mesafesi
    [SerializeField] private float _chaseDistance = 2f;       // Yetişme mesafesi

    private NavMeshAgent _catAgent;           // Kedinin hareket etmesini sağlayan bileşen
    private CatStateController _catStateController; // Kedinin animasyon durumunu kontrol eden sınıf
    private float _timer;
    private bool _isWaiting;
    private bool _isChasing;
    private Vector3 _initialPosition;

    void Awake()
    {
        _catAgent = GetComponent<NavMeshAgent>(); // Kedinin hareket sistemini al
        _catStateController = GetComponent<CatStateController>(); // Animasyon kontrolcüyü al
    }

    void Start()
    {
        _initialPosition = transform.position; // Kedinin başladığı yeri kaydet
        SetRandomDestination();               // Rastgele dolaşma hedefi belirle
    }

    void Update()
    {
        // Eğer oyun oynanmıyorsa kedi duruyor
        if ((GameManager.Instance.GetCurrentGameState() != GameState.Play) &&
            (GameManager.Instance.GetCurrentGameState() != GameState.Resume) &&
            (GameManager.Instance.GetCurrentGameState() != GameState.CutScene))
        {
            _catAgent.speed = 0f;
            return;
        }

        // Eğer oyuncu kedinin dikkatini çektiyse kovalama hareketi yap
        if (_playerController.CanCatChase())
        {
            SetChaseMovement();
        }
        else
        {
            // Yoksa normal dolaşma hareketi yap
            SetPatrolMovement();
        }
    }

    private void SetChaseMovement()
    {
        _isChasing = true;
        _catAgent.SetDestination(_playerTransform.position); // Oyuncuya doğru git
        _catAgent.speed = _chaseSpeed;                        // Hızlı koş
        _catStateController.ChangeState(CatState.Running);   // Koşma animasyonunu aç

        // Eğer kedi oyuncuya çok yaklaştıysa
        if (Vector3.Distance(transform.position, _playerTransform.position) <= _chaseDistance && _isChasing)
        {
            OnCatCatched?.Invoke();                          // Oyuncu yakalandı olayı
            _catStateController.ChangeState(CatState.Attacking); // Saldırı animasyonu
            _isChasing = false;
        }
    }

    private void SetPatrolMovement()
    {
        _catAgent.speed = _defaultSpeed;

        if (!_catAgent.pathPending && _catAgent.remainingDistance <= _catAgent.stoppingDistance)
        {
            if (!_isWaiting)
            {
                _isWaiting = true;   // Kedinin duraklamasını sağla
                _timer = _waitTime;
                _catStateController.ChangeState(CatState.Idle); // Bekleme animasyonu
            }
        }

        if (_isWaiting)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _isWaiting = false;
                SetRandomDestination();                     // Yeni hedef belirle
                _catStateController.ChangeState(CatState.Walking); // Yürüme animasyonu
            }
        }
    }

    private void SetRandomDestination()
    {
        int attempts = 0;
        bool destinationSet = false;

        while (attempts < _maxDestinationAttempts && !destinationSet)
        {
            // Rastgele bir nokta seç
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _patrolRadius + _initialPosition;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _patrolRadius, NavMesh.AllAreas))
            {
                Vector3 finalPosition = hit.position;
                if (!IsPositionBlocked(finalPosition))
                {
                    _catAgent.SetDestination(finalPosition); // Kediyi buraya gönder
                    destinationSet = true;
                }
                else
                {
                    attempts++;
                }
            }
            else
            {
                attempts++;
            }
        }

        if (!destinationSet)
        {
            Debug.LogWarning("Failed to find destination");
            _isWaiting = true;
            _timer = _waitTime * 2;
        }
    }

    private bool IsPositionBlocked(Vector3 position)
    {
        if (NavMesh.Raycast(transform.position, position, out NavMeshHit hit, NavMesh.AllAreas))
        {
            return true; // Yol kapalıysa dön
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = (_initialPosition != Vector3.zero) ? _initialPosition : transform.position;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, _patrolRadius); // Kedinin dolaşabileceği alanı gösterir
    }
}
