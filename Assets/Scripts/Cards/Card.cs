using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card Data")]
public class Card : ScriptableObject
{
    [Header("Общая информация")]
    [SerializeField] private string _cardName;         // Название карты ("Огненный шар")
    [SerializeField] private CardRarity _rarity;       // Редкость (обычная, редкая, эпическая...)
    [SerializeField] private Sprite _icon;            // Иконка (отображается в слоте)
    [SerializeField] private Sprite _frame;           // Рамка (зависит от редкости)

    [Header("Особые настройки")]
    [SerializeField] private CardType _cardType;    // Тип карты
    [SerializeField] private GameObject _spellPrefab; // Префаб способности
    [SerializeField] private float _radius;         // Радиус действия для карт по области
    [SerializeField] private LayerMask _targetLayerMask; // Сущности, на которые распространяется действие карты
    [SerializeField] private GameObject _units;     // Префабы юнитов для карт призыва
    
    [SerializeField] private int _elixirCost;      // Стоимость элексира (как в CR)
    [SerializeField] private float _damage;         // Урон    


    [Header("Описание карты")]
    [TextArea] public string description; // Описание карты


    //Публичные свойства для доступа к данным
    public string CardName => _cardName;
    public CardRarity Rarity => _rarity;
    public CardType CardType => _cardType;
    public int ElexirCost => _elixirCost;
    public Sprite Icon => _icon;
    public Sprite Frame => _frame;
    public float Damage => _damage; 
    public GameObject SpellPrefab => _spellPrefab;
    public float Radius => _radius;
    public LayerMask TargetLayers => _targetLayerMask;
    public GameObject Unit => _units;
}

public enum CardRarity
{
    Common,      // Обычная (синяя)
    Rare,        // Редкая (оранжевая)
    Epic,        // Эпическая (фиолетовая)
    Legendary    // Легендарная (золотая)
}

public enum CardType
{
    Area, // Карты, работающие по области (стрелы, огненный шар)
    Summoners, // Карты, призывающие юнитов (пушка, армия скелетов)
    DirectedAction // Карты, работающие по цели (Свиток усиления, зелье невидимости)
}