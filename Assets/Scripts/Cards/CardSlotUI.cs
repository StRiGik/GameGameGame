using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlotUI : MonoBehaviour
{
    [SerializeField] private Image _cardIcon;
    [SerializeField] private Image _cardFrame;
    [SerializeField] private Text _cardManaCost;
    [SerializeField] private ManaSystem _mana;

    private int _manaCost;

    public Image CardIcon => _cardIcon;
    public int Mana => _manaCost;
    public ManaSystem ManaSystem => _mana;
    public void SetCard(Card _card)
    {
        _cardIcon.sprite = _card.Icon;
        _cardFrame.sprite = _card.Frame;
        _cardManaCost.text = _card.ElexirCost.ToString();
        _manaCost = _card.ElexirCost;
    }

    public void UseCard(Cell targetCell)
    {
        Debug.Log($"����� ������������ �� ������: {targetCell.name}");

        // ����� �����:
        // 1. ������� ������/������ ���������� �� ������
        // 2. ��������� �����������
        // 3. ��������� DeckManager

    }
}
