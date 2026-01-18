using UnityEngine;

// Bu sınıf, "ateş" gibi bir şeye çarpıldığında ne olacağını belirliyor.
// IDamageable demek, bu nesnenin hasar verebileceğini söylüyor.
public class FireDamageable : MonoBehaviour, IDamageable
{
    // _force değişkeni, oyuncuyu geri itmek için kullanılacak gücü tutuyor.
    // SerializeField sayesinde bu değeri Unity editöründen değiştirebiliriz.
    [SerializeField] private float _force = 10f;

    // GiveDamage metodu çağrıldığında, oyuncuya zarar veriyor ve onu geri itiyor.
    public void GiveDamage(Rigidbody playerRigidbody, Transform playerVisualTransform)
    {
        // HealthManager.Instance.Damage(1); --> Oyuncunun canını 1 azaltıyor.
        HealthManager.Instance.Damage(1);

        // playerRigidbody.AddForce(...) --> Oyuncuyu geri fırlatıyor.
        // -playerVisualTransform.forward demek, oyuncunun baktığı yönün tam tersine doğru itmek demek.
        // _force ile bu itme kuvvetini ayarlıyoruz.
        playerRigidbody.AddForce(-playerVisualTransform.forward * _force, ForceMode.Impulse);

        // AudioManager.Instance.Play(...) --> Oyuncuya çarptığında bir ses çalıyor.
        AudioManager.Instance.Play(SoundType.ChickSound);

        // Destroy(gameObject); --> Bu "ateş" objesini sahneden kaldırıyor, yani artık görünmüyor ve çarpmaz.
        Destroy(gameObject);
    }
}
