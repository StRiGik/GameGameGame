using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardSlotUI : MonoBehaviour
{
    [Header("Визуальные элементы")]
    [SerializeField] private Image _cardIcon;
    [SerializeField] private Image _cardFrame;
    [SerializeField] private Text _cardManaCost;
    [SerializeField] private CardType _cardType;    

    [Header("Системы")]
    [SerializeField] private ManaSystem _manaSystem;
    [SerializeField] private DeckManager _deckManager;

    private Card _currentCard;
    private int _manaCost;

    public Image CardIcon => _cardIcon;
    public CardType CardType => _cardType;
    public Card Card => _currentCard;
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


    public void TryUseCard(Cell targetCell = null, GameObject targetObj = null, Transform targetArea = null)
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
        else if(targetObj != null)
        {
            Debug.Log("Карта применена на объект: " + targetObj.name);
        }
        else if(targetArea != null)
        {
            Vector3 spellPos = targetArea.position;
            spellPos.z = 0;
            Instantiate(_currentCard.SpellPrefab, spellPos, Quaternion.identity);
            Debug.Log("карта применена на область" + spellPos);
        }

    }
}