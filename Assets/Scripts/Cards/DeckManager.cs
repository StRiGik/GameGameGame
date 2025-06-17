using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    [Header("Настройки колоды")]
    [SerializeField] private Deck _loadPlayerDeck;

    [Header("Настройки слотов карт")]
    [SerializeField] private List<CardSlotUI> _cardSlots;
    [SerializeField] private Image _nextCardHindImage;
    [SerializeField] private Text _nextCardHintManaCost;

    private Queue<Card> _cardQueue = new Queue<Card>();
    private List<Card> _currentHand = new List<Card>();
    private int _handSize = 4;

    private void Awake()
    {
        InitializeDeck();
        FillInitialHand();
    }

    private void InitializeDeck()
    {
        // Перемешиваем колоду и заполняем очередь
        Card[] shuffledCards = ShuffleTheCards(_loadPlayerDeck.PlayerCards);
        foreach (var card in shuffledCards)
        {
            _cardQueue.Enqueue(card);
            Debug.Log("в очеердь добавлена карта: " + card.name);
        }
    }

    private Card[] ShuffleTheCards(Card[] deck)
    {
        Card[] shuffled = (Card[])deck.Clone();
        for (int i = 0; i < shuffled.Length; i++)
        {
            int randomIndex = Random.Range(i, shuffled.Length);
            Card temp = shuffled[i];
            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }
        return shuffled;
    }

    private void FillInitialHand()
    {
        // Заполняем первые 4 слота
        for (int i = 0; i < _handSize; i++)
        {
            if (_cardQueue.Count > 0)
            {
                Card card = _cardQueue.Dequeue();
                _currentHand.Add(card);
                _cardSlots[i].SetCard(card);

            }
            UpdateNextCardHint();
        }

    }

    public void OnCardUsed(CardSlotUI usedSlot)
    {
        int slotIndex = _cardSlots.IndexOf(usedSlot);
        if (slotIndex == -1) return;

        // Возвращаем использованную карту в конец очереди
        Card usedCard = _currentHand[slotIndex];
        _cardQueue.Enqueue(usedCard);
        _currentHand.RemoveAt(slotIndex);

        // Берем новую карту из очереди
        if (_cardQueue.Count > 0)
        {
            Card newCard = _cardQueue.Dequeue();
            _currentHand.Insert(slotIndex, newCard);
            usedSlot.SetCard(newCard);

            UpdateNextCardHint();
        }
    }

    private void UpdateNextCardHint()
    {
        Card card = _cardQueue.Peek();
        _nextCardHindImage.sprite = card.Icon;
        _nextCardHintManaCost.text = card.ElexirCost.ToString();
    }
    // Для отладки
    public void PrintQueueStatus()
    {
        Debug.Log($"Карт в руке: {_currentHand.Count}, в очереди: {_cardQueue.Count}");
    }
}