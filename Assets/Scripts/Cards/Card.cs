using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card Data")]
public class Card : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _cardName;         // �������� ����� ("�������� ���")
    [SerializeField] private CardRarity _rarity;       // �������� (�������, ������, ���������...)
    [SerializeField] private int _elixirCost;      // ��������� �������� (��� � CR)
    [SerializeField] private Sprite _icon;            // ������ (������������ � �����)
    [SerializeField] private Sprite _frame;           // ����� (������� �� ��������)
    [SerializeField] private GameObject _spellPrefab; // ������ ����������� (���� ����� - ����������)

    [Header("Description")]
    [TextArea] public string description; // �������� ����� (��� UI)

    // ����� �������� ������ ���������: ����, ������, ������������ � �. �.

    //��������� �������� ��� ������� � ������
    public string CardName => _cardName;
    public CardRarity Rarity => _rarity;
    public int ElexirCost => _elixirCost;
    public Sprite Icon => _icon;
    public Sprite Frame => _frame;
    public GameObject SpellPrefab => _spellPrefab;
}

public enum CardRarity
{
    Common,      // ������� (�����)
    Rare,        // ������ (���������)
    Epic,        // ��������� (����������)
    Legendary    // ����������� (�������)
}

public enum CartType
{
    Area, // �����, ���������� �� ������� (������, �������� ���)
    Summoners, // �����, ����������� ������ (�����, ����� ��������)
    DirectedAction // �����, ���������� �� ���� (������ ��������, ����� �����������)
}