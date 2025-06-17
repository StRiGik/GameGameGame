using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card Data")]
public class Card : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _cardName;         // Название карты ("Огненный шар")
    [SerializeField] private CardRarity _rarity;       // Редкость (обычная, редкая, эпическая...)
    [SerializeField] private int _elixirCost;      // Стоимость эльксира (как в CR)
    [SerializeField] private Sprite _icon;            // Иконка (отображается в слоте)
    [SerializeField] private Sprite _frame;           // Рамка (зависит от редкости)
    [SerializeField] private GameObject _spellPrefab; // Префаб способности (если карта - заклинание)

    [Header("Description")]
    [TextArea] public string description; // Описание карты (для UI)

    // Можно добавить другие параметры: урон, радиус, длительность и т. д.

    //Публичные свойства для доступа к данным
    public string CardName => _cardName;
    public CardRarity Rarity => _rarity;
    public int ElexirCost => _elixirCost;
    public Sprite Icon => _icon;
    public Sprite Frame => _frame;
    public GameObject SpellPrefab => _spellPrefab;
}

public enum CardRarity
{
    Common,      // Обычная (синяя)
    Rare,        // Редкая (оранжевая)
    Epic,        // Эпическая (фиолетовая)
    Legendary    // Легендарная (золотая)
}

public enum CartType
{
    Area, // Карты, работающие по области (стрелы, огненный шар)
    Summoners, // Карты, призывающие юнитов (пушка, армия скелетов)
    DirectedAction // Карты, работающие по цели (Свиток усиления, зелье невидимости)
}