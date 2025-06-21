using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card Data")]
public class Card : ScriptableObject
{
    [Header("����� ����������")]
    [SerializeField] private string _cardName;         // �������� ����� ("�������� ���")
    [SerializeField] private CardRarity _rarity;       // �������� (�������, ������, ���������...)
    [SerializeField] private Sprite _icon;            // ������ (������������ � �����)
    [SerializeField] private Sprite _frame;           // ����� (������� �� ��������)

    [Header("������ ���������")]
    [SerializeField] private CardType _cardType;    // ��� �����
    [SerializeField] private GameObject _spellPrefab; // ������ �����������
    [SerializeField] private float _radius;         // ������ �������� ��� ���� �� �������
    [SerializeField] private LayerMask _targetLayerMask; // ��������, �� ������� ���������������� �������� �����
    [SerializeField] private GameObject _units;     // ������� ������ ��� ���� �������
    
    [SerializeField] private int _elixirCost;      // ��������� �������� (��� � CR)
    [SerializeField] private float _damage;         // ����    


    [Header("�������� �����")]
    [TextArea] public string description; // �������� �����


    //��������� �������� ��� ������� � ������
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
    Common,      // ������� (�����)
    Rare,        // ������ (���������)
    Epic,        // ��������� (����������)
    Legendary    // ����������� (�������)
}

public enum CardType
{
    Area, // �����, ���������� �� ������� (������, �������� ���)
    Summoners, // �����, ����������� ������ (�����, ����� ��������)
    DirectedAction // �����, ���������� �� ���� (������ ��������, ����� �����������)
}