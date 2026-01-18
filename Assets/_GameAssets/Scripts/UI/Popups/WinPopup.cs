using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Bu sınıf, oyunu kazandığında çıkan "kazandın" ekranını yönetiyor.
public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _oneMoreButton;    // Tekrar oyna butonu
    [SerializeField] private Button _mainMenuButton;   // Ana menüye dön butonu
    [SerializeField] private TMP_Text _timerText;      // Oyunda geçen zamanı gösterir
    [SerializeField] private TimerUI _timerUI;         // Zamanı hesaplayan UI

    void OnEnable()
    {
        // Kazandın ekranı açıldığında arka plan müziğini kapat
        BackgroundMusic.Instance.PlayBackgroundMusic(false);

        // Kazanma sesi çal
        AudioManager.Instance.Play(SoundType.WinSound);

        // Oyuncuya geçen zamanı göster
        _timerText.text = _timerUI.GetFinalTime();

        // Butonlara tıklandığında ne olacağını ayarla
        _oneMoreButton.onClick.AddListener(OnOneMoreButtonClicked);

        _mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound); // Butona tık sesi
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE); // Ana menüye dön
        });
    }

    // Tekrar oyna butonuna basıldığında
    private void OnOneMoreButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.TransitionSound); // Ses çal
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE); // Oyunu baştan başlat
    }
}
