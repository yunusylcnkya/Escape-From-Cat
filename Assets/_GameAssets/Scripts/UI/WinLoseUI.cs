using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    [Header("Referencens")]
    [SerializeField] private GameObject _blackBackgroundObject;
    [SerializeField] private GameObject _winPopup;
    [SerializeField] private GameObject _losePopup;

    [Header("Settings")]
    [SerializeField] private float _animationDuration = 0.3f;

    private Image _blackBackgroundImage;
    private RectTransform _winPopupTransform;
    private RectTransform _losePopupTransform;

    void Awake()
    {
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        _winPopupTransform = _winPopup.GetComponent<RectTransform>();
        _losePopupTransform = _losePopup.GetComponent<RectTransform>();

        // Başlangıç scale'i 0 olsun ki animasyon daha temiz olsun
        _winPopupTransform.localScale = Vector3.zero;
        _losePopupTransform.localScale = Vector3.zero;
    }

    public void OnGameWin()
    {
        KillTweens();

        _blackBackgroundObject.SetActive(true);
        _winPopup.SetActive(true);

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _winPopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }

    public void OnGameLose()
    {
        KillTweens();

        _blackBackgroundObject.SetActive(true);
        _losePopup.SetActive(true);

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _losePopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }

    private void KillTweens()
    {
        _blackBackgroundImage?.DOKill();
        _winPopupTransform?.DOKill();
        _losePopupTransform?.DOKill();
    }
}