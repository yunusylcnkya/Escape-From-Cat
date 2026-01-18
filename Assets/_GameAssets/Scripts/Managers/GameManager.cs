using System;
using System.Collections;
using UnityEngine;

// Bu sınıf, oyunun genel yönetimini yapıyor.
// Yani oyunun ne zaman başlayacağını, biteceğini, kazanmayı veya kaybetmeyi kontrol ediyor.
public class GameManager : MonoBehaviour
{
    // Her yerden bu sınıfa kolayca ulaşmak için
    public static GameManager Instance { get; private set; }

    // Oyun durumu değiştiğinde diğer sınıflara haber vermek için
    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private CatController _catController; // Oyundaki kedi
    [SerializeField] private EggCounterUI _eggCounterUI;   // Yumurta sayısını gösteren UI
    [SerializeField] private WinLoseUI _winLoseUI;         // Kazan/Kaybet ekranı
    [SerializeField] private PlayerHealthUI _playerHealtUI; // Oyuncunun canını gösteren UI

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5; // Kaç yumurta toplarsak kazanırız
    [SerializeField] private float _delay;        // Oyun bitince bekleme süresi

    private GameState _currentGameState;  // Oyunun şu anki durumu
    private int _currentEggCount;         // Toplanan yumurta sayısı
    private bool _isCatCatched;           // Kedi oyuncuyu yakaladı mı?

    void Awake()
    {
        Instance = this; // Her yerden erişim için
    }

    void Start()
    {
        // Oyuncu öldüğünde veya kedi yakaladığında haber ver
        HealthManager.Instance.OnPlayerDeath += HealthManager_OnPlayerDeath;
        _catController.OnCatCatched += CatController_OnCatCatched;
    }

    // Kedi oyuncuyu yakaladığında
    private void CatController_OnCatCatched()
    {
        if (!_isCatCatched)
        {
            _playerHealtUI.AnimateDamageForAll(); // Oyuncuya hasar animasyonu
            StartCoroutine(OnGameOver(true)); // Oyun bitiriliyor
            CameraShake.Instance.ShakeCamera(1.5f, 2f, 0.5f); // Ekranı salla
            _isCatCatched = true; // Tekrar çalışmasın
        }
    }

    // Oyuncu öldüğünde
    private void HealthManager_OnPlayerDeath()
    {
        StartCoroutine(OnGameOver(false));
    }

    void OnEnable()
    {
        ChangeGameState(GameState.CutScene); // Oyun başladığında kesit sahnesi
        BackgroundMusic.Instance.PlayBackgroundMusic(true); // Arkaplan müziği çal
    }

    // Oyun durumunu değiştiren fonksiyon
    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState); // Diğer sınıflara haber ver
        _currentGameState = gameState;          // Durumu kaydet
        Debug.Log("Game State: " + gameState);  // Konsola yaz
    }

    // Şu anki oyun durumunu öğren
    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }

    // Oyuncu yumurta topladığında
    public void OnEggCollected()
    {
        _currentEggCount++; // Yumurta sayısını artır
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount); // UI'yi güncelle

        if (_currentEggCount == _maxEggCount)
        {
            // Eğer yeteri kadar yumurta toplandıysa, oyun kazanıldı
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin(); // Kazanma ekranını aç
        }
    }

    // Oyun bittiğinde çalışacak fonksiyon
    private IEnumerator OnGameOver(bool isCatCatched)
    {
        yield return new WaitForSeconds(_delay); // Biraz bekle
        ChangeGameState(GameState.GameOver);     // Oyun bitti
        _winLoseUI.OnGameLose();                 // Kaybetme ekranını aç
        if (isCatCatched)
        {
            AudioManager.Instance.Play(SoundType.CatSound); // Kedi sesi çal
        }
    }
}
