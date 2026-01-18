using UnityEngine;

// Bu sınıf, oyuncunun çevresiyle etkileşime girmesini sağlıyor.
// Yani oyuncu nesnelere dokunuyor, onları topluyor, hızlanıyor veya hasar alıyor.
public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Transform _playerVisualTransform; // Oyuncunun modeli
    private PlayerController _playerController; // Oyuncunun hareketlerini kontrol eden sınıf
    private Rigidbody _playerRigidbody; // Fizik hareketini sağlayan bileşen

    void Awake()
    {
        // Oyuncunun diğer bileşenlerini al
        _playerController = GetComponent<PlayerController>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    // Eğer oyuncu bir toplama nesnesine dokunursa
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect(); // Nesneyi topla
        }
    }

    // Eğer oyuncu bir hız artıran nesneye çarparsa
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.Boost(_playerController); // Hızı artır
        }
    }

    // Eğer oyuncu bir zararlı parçacığa çarparsa
    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GiveDamage(_playerRigidbody, _playerVisualTransform); // Hasar al
            CameraShake.Instance.ShakeCamera(1f, 0.5f); // Ekranı salla
        }
    }
}
