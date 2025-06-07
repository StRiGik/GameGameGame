using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/Card Data")]
public class Card : ScriptableObject
{
    [Header("Базовые параметры")]
    [SerializeField] private string _cardName;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _manaCost;
    [SerializeField] private GameObject _spellPrefab;

    public string CardName => _cardName;
    public Sprite Icon => _icon;
    public int ManaCost => _manaCost;

    protected virtual bool CanUseLogic(Transform caster) => true;

    protected virtual void UseLogic(Transform caster, Vector2 targetPos)
    {
        if (_spellPrefab != null)
            Instantiate(_spellPrefab, targetPos, Quaternion.identity);
    }

    public bool CanUse(Transform caster) => CanUseLogic(caster);

    public void Use(Transform caster, Vector2 targetPos)
    {
        if (!CanUse(caster)) return;
        UseLogic(caster, targetPos);
    }
}


