using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardSlotUI : MonoBehaviour
{
    [Header("Визуальные элементы")]
    [SerializeField] private Image _cardIcon;
    [SerializeField] private Image _cardFrame;
    [SerializeField] private Text _cardManaCost;

    private Card _currentCard;
    private int _manaCost;

    public Image CardIcon => _cardIcon;
    public Card Card => _currentCard;
    public void SetCard(Card card)
    {
        _currentCard = card;
        _cardIcon.sprite = card.Icon;
        _cardFrame.sprite = card.Frame;
        _cardManaCost.text = card.ElexirCost.ToString();
        _cardIcon.color = Color.white;
        Debug.Log("добавлена карта:" +  _currentCard.name);
    }
}