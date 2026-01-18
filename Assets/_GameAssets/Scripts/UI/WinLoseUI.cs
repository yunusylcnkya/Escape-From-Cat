using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Bu sınıf, oyunu kazanma veya kaybetme durumunda
// ekranda çıkan kutuları ve arka planı gösterir.
public class WinLoseUI : MonoBehaviour
{
    [Header("Referencens")]
    [SerializeField] private GameObject _blackBackgroundObject; // Arka plan (ekranı karartır)
    [SerializeField] private GameObject _winPopup;             // Kazanma mesajı
    [SerializeField] private GameObject _losePopup;            // Kaybetme mesajı

    [Header("Settings")]
    [SerializeField] private float _animationDuration = 0.3f;  // Animasyonun süresi

    private Image _blackBackgroundImage;
    private RectTransform _winPopupTransform;
    private RectTransform _losePopupTransform;

    void Awake()
    {
        // Arka plan ve popupların referanslarını alıyoruz
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        _winPopupTransform = _winPopup.GetComponent<RectTransform>();
        _losePopupTransform = _losePopup.GetComponent<RectTransform>();

        // Başlangıçta popupları görünmez yapmak için scale 0
        _winPopupTransform.localScale = Vector3.zero;
        _losePopupTransform.localScale = Vector3.zero;
    }

    // Oyunu kazandığımızda çalışacak fonksiyon
    public void OnGameWin()
    {
        KillTweens(); // Daha önceki animasyonları durdur

        _blackBackgroundObject.SetActive(true); // Arka planı göster
        _winPopup.SetActive(true);              // Kazanma mesajını göster

        // Arka planı yavaşça karart
        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        // Kazanma popup'ını büyüt ve ortaya getir
        _winPopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }

    // Oyunu kaybettiğimizde çalışacak fonksiyon
    public void OnGameLose()
    {
        KillTweens(); // Daha önceki animasyonları durdur

        _blackBackgroundObject.SetActive(true); // Arka planı göster
        _losePopup.SetActive(true);             // Kaybetme mesajını göster

        // Arka planı yavaşça karart
        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        // Kaybetme popup'ını büyüt ve ortaya getir
        _losePopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }

    // Önceki animasyonları iptal etmek için
    private void KillTweens()
    {
        _blackBackgroundImage?.DOKill();
        _winPopupTransform?.DOKill();
        _losePopupTransform?.DOKill();
    }
}
