using UnityEngine;

// Bu kod, oyuncunun şu anda ne yaptığını (örneğin duruyor mu, yürüyor mu) kontrol eder.
// Yani karakterin "durumunu" takip eder.
public class StateController : MonoBehaviour
{
    // Oyuncunun şu anki durumunu tutan değişken (örnek: Idle = Boşta duruyor)
    private PlayerState _currentPlayerState = PlayerState.Idle;


    void Start()
    {
        // Oyun başlarken oyuncunun durumu "Idle" yani boşta olarak ayarlanıyor.
        ChangeState(PlayerState.Idle);
    }


    // Bu kısım, oyuncunun durumunu değiştirmek için kullanılır.
    // Mesela koşuyorsa "Running", zıplıyorsa "Jumping" durumuna geçebilir.
    public void ChangeState(PlayerState newPlayerState)
    {
        // Eğer oyuncunun yeni durumu zaten şu ankiyle aynıysa, hiçbir şey yapma.
        // (Yani zaten duruyorsa, tekrar "dur" deme.)
        if (_currentPlayerState == newPlayerState)
        {
            return; // Bu "return" demek: "Geri dön, başka bir şey yapma."
        }

        // Eğer farklı bir durumsa, o zaman yeni durumu kaydet.
        _currentPlayerState = newPlayerState;
    }

    // Bu kısım, şu anda oyuncunun hangi durumda olduğunu söylemek için kullanılır.
    public PlayerState GetCurrentState()
    {
        // Şu anki durumu geri döndür (örneğin "Idle" ya da "Running").
        return _currentPlayerState;
    }
}
