using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    [SerializeField] private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeHeal(float health)
    {
        _currentHealth += health;

        if(_currentHealth > _maxHealth)
        {
            Debug.Log("абонент умер!");
            _currentHealth = _maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if(_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
