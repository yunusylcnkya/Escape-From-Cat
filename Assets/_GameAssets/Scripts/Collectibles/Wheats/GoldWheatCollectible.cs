using UnityEngine;
using UnityEngine.UI;

// GoldWheatCollectible, yani altın buğday topladığında oyuncuya bir güçlendirme veren nesne
// ICollectible kullanıyor, yani "Collect" fonksiyonunu yapmak zorunda
public class GoldWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSO; // Buğdayın verdiği güç ve görseller
    [SerializeField] private PlayerController _playerController; // Oyuncuyu kontrol eden script
    [SerializeField] private PlayerStateUI _playerStateUI; // Oyuncu arayüzü (UI)

    private RectTransform _playerBoosterTransform; // UI'daki hız göstergesi
    private Image _playerBoosterImage; // UI görseli

    void Awake()
    {
        // UI'daki hız göstergesinin konumu ve resmi
        _playerBoosterTransform = _playerStateUI.GetBoosterSpeedTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    // Bu fonksiyon çağrıldığında oyuncu bu buğdayı toplar
    public void Collect()
    {
        // 1️⃣ Oyuncunun hızını artır / güçlendirme uygula
        _playerController.SetMovementSpeed(
            _wheatDesignSO.IncreaseDecreaseMultiplier, // ne kadar hızlanacak
            _wheatDesignSO.ResetBoostDuration         // bu hız ne kadar sürecek
        );

        // 2️⃣ UI'daki hız göstergesini animasyonlarla güncelle
        _playerStateUI.PlayBoosterUIAnimations(
            _playerBoosterTransform,
            _playerBoosterImage,
            _playerStateUI.GetGoldBoosterWheatImage,
            _wheatDesignSO.ActiveSprite,
            _wheatDesignSO.PassiveSprite,
            _wheatDesignSO.ActiveWheatSprite,
            _wheatDesignSO.PassiveWheatSprite,
            _wheatDesignSO.ResetBoostDuration
        );

        // 3️⃣ Kamerayı biraz sallayarak güçlendirme etkisini görsel hale getir
        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);

        // 4️⃣ Ses efekti çal, oyuncu topladığını duysun
        AudioManager.Instance.Play(SoundType.PickupGoodSound);

        // 5️⃣ Bu nesneyi sahneden kaldır, çünkü toplandı
        Destroy(gameObject);
    }
}
