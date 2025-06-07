using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void SetHealth(float health)
    {
        _currentHealth += health;
        //Debug.Log("изменение здоровья произошло!");
        if(_currentHealth <= 0)
        {
            Debug.Log("абонент умер!");
            _currentHealth = _maxHealth;
        }
    }
}
