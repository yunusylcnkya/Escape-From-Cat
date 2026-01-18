using UnityEngine;

// EggCollectible: Oyuncu bu yumurtayı topladığında çalışacak nesne
// ICollectible kullanıyor, yani "Collect" fonksiyonunu yapmak zorunda
public class EggCollectible : MonoBehaviour, ICollectible
{
    // Bu fonksiyon çağrıldığında oyuncu yumurtayı toplar
    public void Collect()
    {
        // 1️⃣ GameManager'a bildir: Bir yumurta toplandı
        GameManager.Instance.OnEggCollected();

        // 2️⃣ Kamera biraz sallansın, topladığını görsün oyuncu
        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);

        // 3️⃣ Ses efekti çal, oyuncu iyi bir şey topladığını duysun
        AudioManager.Instance.Play(SoundType.PickupGoodSound);

        // 4️⃣ Bu nesneyi sahneden kaldır, çünkü toplandı
        Destroy(gameObject);
    }
}
