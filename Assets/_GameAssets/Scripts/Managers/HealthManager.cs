using System;
using UnityEngine;

// Bu sınıf, oyuncunun canını (health) yönetiyor.
// Yani oyuncu hasar aldığında canı azalıyor, iyileştiğinde artıyor ve canı biterse oyun bitiyor.
public class HealthManager : MonoBehaviour
{
    // Her yerden bu sınıfa ulaşabilmek için tek bir örnek (singleton)
    public static HealthManager Instance { get; private set; }

    // Oyuncunun canı bittiğinde diğer sınıflara haber vermek için
    public event Action OnPlayerDeath;

    [Header("References")]
    [SerializeField] private PlayerHealthUI _playerHealtUI; // Can göstergesini kontrol eder

    [SerializeField] private int _maxHealt = 3; // Oyuncunun başlangıç canı
    private int _currentHealth; // Şu anki can

    void Awake()
    {
        Instance = this; // Her yerden erişim için
    }

    void Start()
    {
        _currentHealth = _maxHealt; // Oyun başlarken canı maksimum yap
    }

    // Oyuncu hasar aldığında çağrılır
    public void Damage(int damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount; // Canı azalt
            _playerHealtUI.AnimateDamage(); // Hasar animasyonu oynat

            // Eğer can 0 olduysa
            if (_currentHealth <= 0)
            {
                OnPlayerDeath?.Invoke(); // Oyuncu öldü, diğer sınıflara haber ver
            }
        }
    }

    // Oyuncu iyileştiğinde çağrılır
    public void Heal(int healAmount)
    {
        if (_currentHealth < _maxHealt)
        {
            // Canı maksimum değeri geçmeyecek şekilde artır
            _currentHealth = Mathf.Min(_currentHealth + healAmount, _maxHealt);
        }
    }
}
