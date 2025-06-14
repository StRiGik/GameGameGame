using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private int _maxMana = 10;
    [SerializeField] private float _manaRegenRate = 1f;
    [SerializeField] private Slider _manaBar;

    private float _currentMana;

    public float CurrentMana => _currentMana;

    private void Start()
    {
        _currentMana = _maxMana;
        UpdateManaBar();
    }

    private void Update()
    {
        if (_currentMana < _maxMana)
        {
            _currentMana = Mathf.Min(_currentMana + _manaRegenRate * Time.deltaTime, _maxMana);
            UpdateManaBar();
        }
    }

    public bool TrySpendMana(int amount)
    {
        if (_currentMana >= amount)
        {
            _currentMana -= amount;
            UpdateManaBar();
            return true;
        }
        return false;
    }

    public void AddMana(int amount)
    {
        _currentMana = Mathf.Min(_currentMana + amount, _maxMana);
        UpdateManaBar();
    }

    private void UpdateManaBar()
    {
        if (_manaBar != null)
            _manaBar.value = _currentMana / _maxMana;
    }
}