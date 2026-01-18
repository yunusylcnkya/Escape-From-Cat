using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

// Bu sınıf, oyundaki süreyi ve timer animasyonunu yönetir.
public class TimerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _timerRotatableTransform; // Dönen saat veya simge
    [SerializeField] private TMP_Text _timerText;                     // Ekrandaki süre yazısı

    [Header("Settings")]
    [SerializeField] private float _rotationDuration; // Animasyon süresi
    [SerializeField] private Ease _rotationEase;      // Animasyonun yumuşaklığı

    private float _elapsedTime;       // Geçen süre
    private bool _isTimerRunning;     // Timer çalışıyor mu?
    private Tween _rotationTween;     // Döndürme animasyonu

    private string _finalTime;        // Oyunun bitimindeki süre

    void Start()
    {
        // Oyun durumu değişince ne olacağını belirle
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Play:
                PlayRotationAnimation(); // Timer simgesini döndür
                StartTimer();            // Sayacı başlat
                break;
            case GameState.Pause:
                StopTimer();             // Sayacı durdur
                break;
            case GameState.Resume:
                ResumeTimer();           // Sayacı devam ettir
                break;
            case GameState.GameOver:
                FinishTimer();           // Sayacı bitir
                break;
        }
    }

    private void PlayRotationAnimation()
    {
        // Timer simgesini sonsuz şekilde döndür
        _rotationTween = _timerRotatableTransform
            .DORotate(new Vector3(0f, 0f, -360f), _rotationDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(_rotationEase);
    }

    private void StartTimer()
    {
        _isTimerRunning = true;
        _elapsedTime = 0f;
        // Her 1 saniyede bir UpdateTimerUI fonksiyonunu çağır
        InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
    }

    private void StopTimer()
    {
        _isTimerRunning = false;
        CancelInvoke(nameof(UpdateTimerUI));
        _rotationTween.Pause(); // Timer simgesini durdur
    }

    private void ResumeTimer()
    {
        if (!_isTimerRunning)
        {
            _isTimerRunning = true;
            InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
            _rotationTween.Play(); // Timer simgesini tekrar döndür
        }
    }

    private void FinishTimer()
    {
        StopTimer();                   // Timer durur
        _finalTime = GetFormattedElapsedTime(); // Son zamanı kaydet
    }

    private string GetFormattedElapsedTime()
    {
        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        return _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateTimerUI()
    {
        if (!_isTimerRunning) { return; }

        _elapsedTime += 1f; // Her saniye bir artır

        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Ekrana yaz
    }

    public string GetFinalTime()
    {
        return _finalTime; // Oyunun bitimindeki süreyi ver
    }
}
