using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Bu sınıf, oyunu kaybettiğinde çıkan "kaybettin" ekranını yönetiyor.
public class LosePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _tryAgainButton;   // Tekrar dene butonu
    [SerializeField] private Button _mainMenuButton;   // Ana menüye dön butonu
    [SerializeField] private TMP_Text _timerText;      // Oyun süresini gösterir
    [SerializeField] private TimerUI _timerUI;         // Zamanı hesaplayan UI

    void OnEnable()
    {
        // Kaybettin ekranı açıldığında arka plan müziğini kapat
        BackgroundMusic.Instance.PlayBackgroundMusic(false);

        // Kaybetme sesi çal
        AudioManager.Instance.Play(SoundType.LoseSound);

        // Oyuncuya geçen zamanı göster
        _timerText.text = _timerUI.GetFinalTime();

        // Butonlara tıklandığında ne olacağını ayarla
        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);

        _mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound); // Butona tık sesi
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE); // Ana menüye dön
        });
    }

    // Tekrar dene butonuna basıldığında
    private void OnTryAgainButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.TransitionSound); // Ses çal
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE); // Oyunu baştan başlat
    }
}
