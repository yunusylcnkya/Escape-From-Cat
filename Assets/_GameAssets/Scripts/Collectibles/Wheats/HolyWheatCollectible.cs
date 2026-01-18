using UnityEngine;
using UnityEngine.UI;

// HolyWheatCollectible, yani kutsal buğday toplandığında oyuncunun zıplama gücünü artıran nesne
// ICollectible kullanıyor, yani "Collect" fonksiyonunu yapmak zorunda
public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSO; // Buğdayın verdiği zıplama gücü ve görseller
    [SerializeField] private PlayerController _playerController; // Oyuncuyu kontrol eden script
    [SerializeField] private PlayerStateUI _playerStateUI; // Oyuncu arayüzü (UI)

    private RectTransform _playerBoosterTransform; // UI'daki zıplama göstergesi
    private Image _playerBoosterImage; // UI görseli

    void Awake()
    {
        // UI'daki zıplama göstergesinin konumu ve resmi
        _playerBoosterTransform = _playerStateUI.GetBoosterJumpTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    // Bu fonksiyon çağrıldığında oyuncu bu kutsal buğdayı toplar
    public void Collect()
    {
        // 1️⃣ Oyuncunun zıplama gücünü artır / güçlendirme uygula
        _playerController.SetJumpForce(
            _wheatDesignSO.IncreaseDecreaseMultiplier, // ne kadar daha yükseğe zıplayacak
            _wheatDesignSO.ResetBoostDuration         // bu güç ne kadar sürecek
        );

        // 2️⃣ UI'daki zıplama göstergesini animasyonlarla güncelle
        _playerStateUI.PlayBoosterUIAnimations(
            _playerBoosterTransform,
            _playerBoosterImage,
            _playerStateUI.GetHolyBoosterWheatImage,
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
