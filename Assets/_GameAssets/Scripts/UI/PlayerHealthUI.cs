using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Bu sınıf, oyuncunun sağlığını ekranda gösteriyor ve hasar aldığında animasyon oynatıyor.
public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] _playerHealthImages; // Sağlık ikonları (kalpler gibi)

    [Header("Sprites")]
    [SerializeField] private Sprite _playerHealtySprite;   // Canlı kalp resmi
    [SerializeField] private Sprite _playerUnhealtySprite; // Hasar almış kalp resmi

    [Header("Settings")]
    [SerializeField] private float _scaleDuration; // Kalbin küçülüp büyüme süresis

    private RectTransform[] _playerHealthTransforms; // Kalplerin boyutunu kontrol etmek için

    void Awake()
    {
        // Her kalbin boyutunu sakla
        _playerHealthTransforms = new RectTransform[_playerHealthImages.Length];
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            _playerHealthTransforms[i] = _playerHealthImages[i].gameObject.GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        // Test amaçlı: O tuşuna basınca 1 kalp hasar alıyor
        if (Input.GetKeyDown(KeyCode.O))
        {
            AnimateDamage();
        }
        // Test amaçlı: P tuşuna basınca tüm kalpler hasar alıyor
        if (Input.GetKeyDown(KeyCode.P))
        {
            AnimateDamageForAll();
        }
    }

    // Sadece 1 kalbi hasarlandır
    public void AnimateDamage()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            if (_playerHealthImages[i].sprite == _playerHealtySprite)
            {
                AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransforms[i]);
                break;
            }
        }
    }

    // Tüm kalpleri hasarlandır
    public void AnimateDamageForAll()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransforms[i]);
        }
    }

    // Kalp hasar animasyonu
    private void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        if (activeImageTransform == null || activeImage == null) return;

        // Önce eski animasyonu durdur
        activeImageTransform.DOKill();

        // Kalbi önce küçült
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            if (activeImageTransform == null || activeImage == null) return;

            // Kalbi hasarlı sprite ile değiştir
            activeImage.sprite = _playerUnhealtySprite;

            // Kalbi tekrar eski boyutuna getir
            activeImageTransform.DOKill();
            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }
}
