using UnityEngine;

// Bu sınıf, kameranın karakterin arkasında durmasını ve karakterin gitmek istediği yöne dönmesini sağlar.
// Yani kamera karakteri takip eder ve karakterin yönü kamera ile uyumlu olur.
public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")] // Unity editöründe düzen için başlık

    // Karakterin konumunu tutuyoruz (nerede olduğunu biliyoruz)
    [SerializeField] private Transform _playerTransform;

    // Karakterin yönünü belirlemeye yardımcı oluyoruz
    [SerializeField] private Transform _orientationTransform;

    // Karakterin görünür kısmı (3D modeli) 
    [SerializeField] private Transform _playerVisualTransform;

    [Header("Settings")] // Ayarlar başlığı

    // Karakter ne kadar hızlı dönecek, yani yön değiştirme hızı
    [SerializeField] private float _rotationSpeed;

    void Update()
    {
        // Oyun oynanıyor değilse hiçbir şey yapma
        if ((GameManager.Instance.GetCurrentGameState() != GameState.Play) &&
            (GameManager.Instance.GetCurrentGameState() != GameState.Resume))
        {
            return;
        }

        // Kamera ile oyuncu arasındaki farkı buluyoruz
        // Böylece hangi yöne baktığını bilebiliriz
        Vector3 viewDirection = _playerTransform.position - new Vector3(
            transform.position.x,
            _playerTransform.position.y,
            transform.position.z);

        // Orientation nesnesini bu yöne çeviriyoruz
        _orientationTransform.forward = viewDirection.normalized;

        // Klavyeden yön tuşlarını alıyoruz
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // Sol-sağ
        float verticalInput = Input.GetAxisRaw("Vertical");     // İleri-geri

        // Oyuncunun gitmek istediği yönü hesaplıyoruz
        Vector3 inputDirection = _orientationTransform.forward * verticalInput +
                                 _orientationTransform.right * horizontalInput;

        // Eğer oyuncu bir tuşa basıyorsa
        if (inputDirection != Vector3.zero)
        {
            // Karakter modelini gitmek istediği yöne doğru yavaşça döndürüyoruz
            // Böylece ani dönme olmaz, dönüşler yumuşak olur
            _playerVisualTransform.forward = Vector3.Slerp(
                _playerVisualTransform.forward,
                inputDirection.normalized,
                Time.deltaTime * _rotationSpeed);
        }
    }
}
