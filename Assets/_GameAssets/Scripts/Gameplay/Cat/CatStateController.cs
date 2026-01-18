using UnityEngine;

// Bu sınıf, kedinin hangi durumda olduğunu saklıyor.
// Yani kedi yürüyorsa, koşuyorsa, duruyorsa veya saldırıyorsa bunu takip ediyoruz.
public class CatStateController : MonoBehaviour
{
    // Kedinin şu anki durumunu tutuyoruz (başlangıçta yürüyüşte)
    [SerializeField] private CatState _currentCatState = CatState.Walking;

    void Start()
    {
        // Oyun başlarken kediyi yürüyüş durumuna al
        ChangeState(CatState.Walking);
    }

    // Kedinin durumunu değiştirmek için kullanılır
    public void ChangeState(CatState newState)
    {
        // Eğer yeni durum eskisiyle aynıysa hiçbir şey yapma
        if (_currentCatState == newState) { return; }

        // Kedinin durumunu güncelle
        _currentCatState = newState;
    }

    // Kedinin şu anki durumunu öğrenmek için kullanılır
    public CatState GetCurrentCatState()
    {
        return _currentCatState;
    }
}
