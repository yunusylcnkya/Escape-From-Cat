using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Transform _playerVisualTransform;
    private PlayerController _playerController;
    private Rigidbody _playerRigidbody;
    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();

        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.Boost(_playerController);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GiveDamage(_playerRigidbody, _playerVisualTransform);
        }
    }
}