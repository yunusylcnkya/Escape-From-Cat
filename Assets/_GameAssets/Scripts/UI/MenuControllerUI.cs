using MaskTransitions;
using UnityEngine;
using UnityEngine.UI;

// Bu sınıf, oyunun ana menüsündeki butonları yönetiyor.
public class MenuControllerUI : MonoBehaviour
{
    [SerializeField] private Button _playButton; // Oyunu başlatma butonu
    [SerializeField] private Button _quitButton; // Oyundan çıkma butonu

    void Awake()
    {
        // "Play" butonuna basılınca ne olacağını ayarla
        _playButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound); // Ses çal
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE); // Oyunu başlat
        });

        // "Quit" butonuna basılınca ne olacağını ayarla
        _quitButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.ButtonClickSound); // Ses çal
            Debug.Log("quitting"); // Konsola yaz (oyun kapanırken görebilirsin)
            Application.Quit(); // Oyundan çık
        });
    }
}
