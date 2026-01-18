using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

// Bu sınıf, ekrandaki oyuncu durumlarını ve güçlendirici göstergelerini yönetiyor.
public class PlayerStateUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerController _playerController; // Oyuncunun kendisi

    [SerializeField] private RectTransform _playerWalkingTransform; // Yürüyüş simgesi
    [SerializeField] private RectTransform _playerSlidingTransform; // Kayma simgesi

    [SerializeField] private RectTransform _boosterSpeedTransform; // Hız güçlendirici göstergesi
    [SerializeField] private RectTransform _boosterJumpTransform;  // Zıplama güçlendirici göstergesi
    [SerializeField] private RectTransform _boosterSlowTransform;  // Yavaşlatma göstergesi
    [SerializeField] private PlayableDirector _playableDirector;   // Zaman çizelgesi animasyonları

    [Header("Images")]
    [SerializeField] private Image _goldBoosterWheatImage;   // Altın buğday göstergesi
    [SerializeField] private Image _holyBoosterWheatImage;   // Kutsal buğday göstergesi
    [SerializeField] private Image _rottenBoosterWheatImage; // Çürük buğday göstergesi

    [Header("Sprites")]
    [SerializeField] private Sprite _playerWalkingActiveSprite;  // Yürüyüş aktif resmi
    [SerializeField] private Sprite _playerWalkingPassiveSprite; // Yürüyüş pasif resmi

    [SerializeField] private Sprite _playerSlidingActiveSprite;  // Kayma aktif resmi
    [SerializeField] private Sprite _playerSlidingPassiveSprite; // Kayma pasif resmi

    [Header("Settings")]
    [SerializeField] private float _moveDuration; // Animasyon süresi
    [SerializeField] private Ease _moveEase;      // Animasyon eğrisi

    private Image _playerWalkingImage;
    private Image _playerSlidingImage;

    void Awake()
    {
        _playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        _playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();
    }

    void Start()
    {
        // Oyuncu durumu değiştiğinde hangi simgenin aktif olacağını ayarla
        _playerController.OnPlayerStateChangend += PlayerController_OnPlayerStateChanged;

        // Zaman çizelgesi bittiğinde simgeleri ayarla
        _playableDirector.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        // Oyuncu yürüyüş simgesi aktif, kayma pasif olsun
        SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
    }

    private void PlayerController_OnPlayerStateChanged(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                // Oyuncu duruyor veya yürüyor: yürüyüş aktif, kayma pasif
                SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
                break;

            case PlayerState.SlideIdle:
            case PlayerState.Slide:
                // Oyuncu kayıyor: kayma aktif, yürüyüş pasif
                SetStateUserInterfaces(_playerWalkingPassiveSprite, _playerSlidingActiveSprite, _playerSlidingTransform, _playerWalkingTransform);
                break;
        }
    }

    private void SetStateUserInterfaces(Sprite playerWalkingSprite, Sprite playerSlidingSprite,
      RectTransform activeTransform, RectTransform passiveTransform)
    {
        // Simgelerin resimlerini değiştir
        _playerWalkingImage.sprite = playerWalkingSprite;
        _playerSlidingImage.sprite = playerSlidingSprite;

        // Aktif simgeyi biraz daha öne getir (animasyon)
        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);
        passiveTransform.DOAnchorPosX(-90f, _moveDuration).SetEase(_moveEase);
    }

    // Güçlendirici göstergelerini animasyonla açıp kapatma
    private IEnumerator SetBoosterUserInterfaces(
        RectTransform activeTransform,
        Image boosterImage,
        Image wheatImage,
        Sprite activeSprite,
        Sprite passiveSprite,
        Sprite activeWheatSprite,
        Sprite passiveWheatSprite,
        float duration)
    {
        // Önce aktif hallerini göster
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(25f, _moveDuration).SetEase(_moveEase);

        yield return new WaitForSeconds(duration);

        // Süre bitince tekrar pasif hallerine dön
        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(90f, _moveDuration).SetEase(_moveEase);
    }

    public void PlayBoosterUIAnimations(
        RectTransform activeTransform,
        Image boosterImage,
        Image wheatImage,
        Sprite activeSprite,
        Sprite passiveSprite,
        Sprite activeWheatSprite,
        Sprite passiveWheatSprite,
        float duration)
    {
        StartCoroutine(SetBoosterUserInterfaces(
            activeTransform,
            boosterImage,
            wheatImage,
            activeSprite,
            passiveSprite,
            activeWheatSprite,
            passiveWheatSprite,
            duration));
    }
}
