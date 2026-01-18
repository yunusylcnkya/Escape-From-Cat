using UnityEngine;
using UnityEngine.UI;

// RottenWheatCollectible, yani çürük buğday toplandığında oyuncunun hareket hızını azaltan nesne
// ICollectible kullanıyor, yani "Collect" fonksiyonunu yapmak zorunda
public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSO; // Buğdayın hızı düşürme oranı ve görselleri
    [SerializeField] private PlayerController _playerController; // Oyuncuyu kontrol eden script
    [SerializeField] private PlayerStateUI _playerStateUI; // Oyuncu arayüzü (UI)

    private RectTransform _playerBoosterTransform; // UI'daki yavaşlama göstergesi
    private Image _playerBoosterImage; // UI görseli

    void Awake()
    {
        // UI'daki yavaşlama göstergesinin konumu ve resmi
        _playerBoosterTransform = _playerStateUI.GetBoosterSlowTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    // Bu fonksiyon çağrıldığında oyuncu bu çürük buğdayı toplar
    public void Collect()
    {
        // 1️⃣ Oyuncunun hareket hızını azalt / yavaşlat
        _playerController.SetMovementSpeed(
            _wheatDesignSO.IncreaseDecreaseMultiplier, // ne kadar yavaşlayacak
            _wheatDesignSO.ResetBoostDuration         // bu yavaşlama ne kadar sürecek
        );

        // 2️⃣ UI'daki yavaşlama göstergesini animasyonlarla güncelle
        _playerStateUI.PlayBoosterUIAnimations(
            _playerBoosterTransform,
            _playerBoosterImage,
            _playerStateUI.GetRottenBoosterWheatImage,
            _wheatDesignSO.ActiveSprite,
            _wheatDesignSO.PassiveSprite,
            _wheatDesignSO.ActiveWheatSprite,
            _wheatDesignSO.PassiveWheatSprite,
            _wheatDesignSO.ResetBoostDuration
        );

        // 3️⃣ Kamerayı biraz sallayarak yavaşlamayı görsel hale getir
        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);

        // 4️⃣ Ses efekti çal, oyuncu kötü bir şey topladığını duysun
        AudioManager.Instance.Play(SoundType.PickupBadSound);

        // 5️⃣ Bu nesneyi sahneden kaldır, çünkü toplandı
        Destroy(gameObject);
    }
}
