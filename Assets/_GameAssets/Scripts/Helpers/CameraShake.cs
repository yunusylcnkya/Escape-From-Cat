using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

// Bu sınıf, oyun sırasında kamerayı sallamak için kullanılır.
// Mesela bir patlama, çarpma veya düşme olduğunda ekran sarsılıyor.
public class CameraShake : MonoBehaviour
{
    // Bu sınıfın tek bir örneği var, her yerden kolayca erişebiliriz
    public static CameraShake Instance { get; private set; }

    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin; // Kamerayı sallamak için
    private float _shakeTimer;       // Kameranın sallanacağı süreyi tutar
    private float _shakeTimerTotal;  // Sallanma süresinin toplamı
    private float _startingIntensity; // Sallanmanın başlangıç gücü

    void Awake()
    {
        Instance = this; // Her yerden bu sınıfa ulaşmak için
        _cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        // Eğer kamera sallanıyorsa
        if (_shakeTimer > 0f)
        {
            _shakeTimer -= Time.deltaTime; // Zamanı azalt

            // Sallanma süresi bittiğinde
            if (_shakeTimer <= 0f)
            {
                // Kamerayı yavaşça eski haline getir
                _cinemachineBasicMultiChannelPerlin.AmplitudeGain = Mathf.Lerp(_startingIntensity, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
            }
        }
    }

    // Kamerayı sallama işlemi coroutine ile yapılır
    private IEnumerator CameraShakeCoroutine(float intensity, float time, float delay)
    {
        yield return new WaitForSeconds(delay); // Eğer gecikme varsa bekle
        _cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity; // Sallanmayı başlat
        _shakeTimer = time;       // Sallanma süresini ayarla
        _shakeTimerTotal = time;  // Toplam süreyi kaydet
        _startingIntensity = intensity; // Başlangıç gücünü kaydet
    }

    // Bu fonksiyon başka yerlerden kamerayı sallamak için çağrılır
    public void ShakeCamera(float intensity, float time, float delay = 0f)
    {
        StartCoroutine(CameraShakeCoroutine(intensity, time, delay));
    }
}
