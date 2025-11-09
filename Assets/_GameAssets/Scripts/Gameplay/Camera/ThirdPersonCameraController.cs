using UnityEngine;

// Bu kod, kameranın arkasından baktığı bir karakterin yönünü kontrol eder.
// Yani kamera karakterin arkasında durur ve karakter hangi yöne gitmek isterse o yöne döner.
public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")] // Unity'de başlık olarak görünür (sadece düzen kolaylığı sağlar)

    // Oyuncunun (karakterin) yerini tutar.
    [SerializeField] private Transform _playerTransform;

    // Oyuncunun yönünü (nereye baktığını) belirlememize yardım eder.
    [SerializeField] private Transform _orientationTransform;

    // Oyuncunun görsel kısmını (örneğin 3D modelini) temsil eder.
    [SerializeField] private Transform _playerVisualTransform;

    [Header("Settings")] // Ayarların başlığı

    // Oyuncunun ne kadar hızlı döneceğini belirler (ne kadar hızlı yön değiştirir)
    [SerializeField] private float _rotationSpeed;

    void Update()
    {
        // Bu kısımda kamera ile oyuncunun arasındaki farkı buluyoruz.
        // Böylece kamera, oyuncunun nereye baktığını bilebiliyor.
        Vector3 viewDirection = _playerTransform.position - new Vector3(
            transform.position.x,
            _playerTransform.position.y,
            transform.position.z);

        // Orientation nesnesini, bu fark yönüne doğru döndürüyoruz.
        // Yani oyuncu kameraya göre doğru şekilde yönleniyor.
        _orientationTransform.forward = viewDirection.normalized;

        // Klavyeden gelen yön tuşlarını (WASD) okuyoruz.
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // A-D veya ok tuşları (sol-sağ)
        float verticalInput = Input.GetAxisRaw("Vertical");     // W-S veya ok tuşları (ileri-geri)

        // Oyuncunun hangi yöne gitmek istediğini hesaplıyoruz.
        // İleri geri ve sağ sol hareketleri birleştiriyoruz.
        Vector3 inputDirection = _orientationTransform.forward * verticalInput +
                                 _orientationTransform.right * horizontalInput;

        // Eğer oyuncu bir yöne gitmek istiyorsa (yani tuşa basıyorsa)
        if (inputDirection != Vector3.zero)
        {
            // Oyuncunun görsel kısmını (örneğin 3D modelini),
            // gitmek istediği yöne doğru yavaşça döndürüyoruz.
            // Slerp sayesinde dönüş yumuşak oluyor (ani dönmüyor).
            _playerVisualTransform.forward = Vector3.Slerp(
                _playerVisualTransform.forward,
                inputDirection.normalized,
                Time.deltaTime * _rotationSpeed);
        }
    }

}
