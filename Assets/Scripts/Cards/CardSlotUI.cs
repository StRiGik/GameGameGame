using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardSlotUI : MonoBehaviour
{
    [Header("Визуальные элементы")]
    [SerializeField] private Image _cardIcon;
    [SerializeField] private Image _cardFrame;
    [SerializeField] private Text _cardManaCost;

    [Header("Системы")]
    [SerializeField] private ManaSystem _manaSystem;
    [SerializeField] private DeckManager _deckManager;

    private Card _currentCard;
    private int _manaCost;

    public Image CardIcon => _cardIcon;

    public void SetCard(Card card)
    {
        _currentCard = card;
        _cardIcon.sprite = card.Icon;
        _cardFrame.sprite = card.Frame;
        _cardManaCost.text = card.ElexirCost.ToString();
        _manaCost = card.ElexirCost;
        _cardIcon.color = Color.white;
        Debug.Log("добавлена карта:" +  _currentCard.name);
    }


    public void TryUseCard(Cell targetCell)
    {
        if (!_manaSystem.TrySpendMana(_manaCost)) return;

        Debug.Log($"Использована карта: {_currentCard.CardName}");

        // Уведомляем DeckManager
        _deckManager.OnCardUsed(this);

        // Здесь можно добавить эффекты применения карты
        if (targetCell != null && _currentCard.SpellPrefab != null)
        {
            Instantiate(_currentCard.SpellPrefab, targetCell.transform.position, Quaternion.identity);
        }
    }
}