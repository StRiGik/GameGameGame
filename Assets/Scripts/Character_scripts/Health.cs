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
        //Debug.Log("��������� �������� ���������!");
        if(_currentHealth <= 0)
        {
            Debug.Log("������� ����!");
            _currentHealth = _maxHealth;
        }
    }
}
