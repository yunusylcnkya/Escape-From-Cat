using UnityEngine;

// Bu sınıf bir "SpatulaBooster" yani oyuncuyu ileri fırlatan bir güçlendirme.
// IBoostable kullanıyor, yani "Boost" fonksiyonuna sahip olmak zorunda.
public class SpatulaBooster : MonoBehaviour, IBoostable
{
    [Header("References")]
    [SerializeField] private Animator _spatulaAnimator; // Spatulayı oynatan animasyon
    [Header("Settings")]
    [SerializeField] private float _jumForce; // Oyuncuyu fırlatacak güç miktarı

    private bool _isActivated; // Booster'ın şu anda çalışıp çalışmadığını kontrol eder

    // Bu fonksiyon, oyuncuyu güçlendirmek (boost) için çağrılır
    public void Boost(PlayerController playerController)
    {
        if (_isActivated) { return; } // Eğer zaten aktifse tekrar çalıştırmaz
        PlayBoostAnimation(); // Animasyonu oynat

        // Oyuncunun Rigidbody'sini alıyoruz ki fizik ile hareket ettirebilelim
        Rigidbody playerRigidbody = playerController.GetPlayerRigidbody();

        // Önce oyuncunun dikey hızını sıfırlıyoruz, böylece düzgün fırlasın
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);

        // Oyuncuyu ileri doğru fırlatıyoruz
        playerRigidbody.AddForce(transform.forward * _jumForce, ForceMode.Impulse);

        _isActivated = true; // Booster aktif oldu
        Invoke(nameof(ResetActivation), 0.2f); // 0.2 saniye sonra tekrar kullanılabilir hale getir

        // Ses efektini çal
        AudioManager.Instance.Play(SoundType.SpatulaSound);
    }

    // Spatula animasyonunu oynatan fonksiyon
    private void PlayBoostAnimation()
    {
        _spatulaAnimator.SetTrigger(Consts.OtherAnimations.IS_SPATULA_JUMPING);
    }

    // Booster tekrar kullanılabilir hale getiriliyor
    private void ResetActivation()
    {
        _isActivated = false;
    }
}
