using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int _maxHealt = 3;
    private int _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealt;
    }


    public void Damage(int damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
            //todo: ui animate damage
            if (_currentHealth <= 0)
            {
                //todo :Playerdead
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (_currentHealth < _maxHealt)
        {
            _currentHealth = Mathf.Min(_currentHealth + healAmount, _maxHealt);
        }
    }
}
