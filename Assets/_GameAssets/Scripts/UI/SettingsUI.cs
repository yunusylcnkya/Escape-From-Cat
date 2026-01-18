using System;
using DG.Tweening;
using MaskTransitions;
using UnityEngine;
using UnityEngine.UI;

// Bu sınıf, oyun içi ayarlar menüsünü yönetiyor.
public class SettingsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _settingsPopupObject;   // Ayarlar penceresi
    [SerializeField] private GameObject _blackBackgroundObject; // Arkadaki karartma paneli

    [Header("Buttons")]
    [SerializeField] private Button _settingsButton; // Ayarlar menüsünü açan buton
    [SerializeField] private Button _musicButton;    // Müziği açıp kapatan buton
    [SerializeField] private Button _soundButton;    // Ses efektlerini açıp kapatan buton
    [SerializeField] private Button _resumeButton;   // Oyuna geri dönme butonu
    [SerializeField] private Button _mainMenuButton; // Ana menüye dönme butonu

    [Header("Sprites")]
    [SerializeField] private Sprite _musicActiveSprite;   // Müziğin açık olduğunu gösteren simge
    [SerializeField] private Sprite _musicPassiveSprite;  // Müziğin kapalı olduğunu gösteren simge
    [SerializeField] private Sprite _soundActiveSprite;   // Ses efektlerinin açık olduğunu gösteren simge
    [SerializeField] private Sprite _soundPassiveSprite;  // Ses efektlerinin kapalı olduğunu gösteren simge

    [Header("Settings")]
    [SerializeField] private float _animationDuration; // Animasyonların süresi

    private Image _blackBackgroundImage;
    private bool _isMusicActive = true;
    private bool _isSoundActive = true;

    void Awake()
    {
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();

        // Başlangıçta ayarlar menüsü görünmesin
        _settingsPopupObject.transform.localScale = Vector3.zero;

        // Butonlara tıklama olaylarını bağla
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
        _mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE); // Ana menüye dön
        });

        _musicButton.onClick.AddListener(OnMusicButtonClicked);
        _soundButton.onClick.AddListener(OnSoundButtonClicked);
    }

    private void OnMusicButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _isMusicActive = !_isMusicActive; // Açık/kapalı değiştir
        _musicButton.image.sprite = _isMusicActive ? _musicActiveSprite : _musicPassiveSprite;
        BackgroundMusic.Instance.SetMusicMute(!_isMusicActive); // Müziği aç veya kapat
    }

    private void OnSoundButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _isSoundActive = !_isSoundActive; // Açık/kapalı değiştir
        _soundButton.image.sprite = _isSoundActive ? _soundActiveSprite : _soundPassiveSprite;
        AudioManager.Instance.SetSoundEffectsMute(!_isSoundActive); // Ses efektlerini aç veya kapat
    }

    private void OnSettingsButtonClicked()
    {
        // Önce eski animasyonları durdur
        _settingsPopupObject.transform.DOKill();
        _blackBackgroundImage.DOKill();

        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        GameManager.Instance.ChangeGameState(GameState.Pause); // Oyunu duraklat

        // Menü ve karartmayı aç ve animasyonla göster
        _blackBackgroundObject.SetActive(true);
        _settingsPopupObject.SetActive(true);
        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _settingsPopupObject.transform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }

    private void OnResumeButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);

        // Menü ve karartma animasyonunu başlat
        _settingsPopupObject.transform.DOKill();
        _blackBackgroundImage.DOKill();

        _blackBackgroundImage.DOFade(0.0f, _animationDuration).SetEase(Ease.Linear);
        _settingsPopupObject.transform.DOScale(0f, _animationDuration).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            GameManager.Instance.ChangeGameState(GameState.Resume); // Oyuna geri dön
            _blackBackgroundObject.SetActive(false);
            _settingsPopupObject.SetActive(false);
        });
    }
}
