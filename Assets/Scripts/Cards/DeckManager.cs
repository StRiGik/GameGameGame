using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    [Header("настройки колоды")]
    [SerializeField] private Deck _loadPlayerDeck;
    [SerializeField] private Card[] _selectedPlayerCards = new Card[8];

    [Header("настройки слотов карт")]
    [SerializeField] private List<CardSlotUI> _cardSlots;

    private int _lastCard;

    private void Awake()
    {
        _selectedPlayerCards = ShuffleTheCards(_loadPlayerDeck.PlayerCards);
        Debug.Log("карты получены и перетасованы");

        for(int i = 0; i < _cardSlots.Count; i++)
        {
            _cardSlots[i].SetCard(_selectedPlayerCards[i]);
            _lastCard = i;
        }
        Debug.Log("рука заполнена картами");
    }

    private Card[] ShuffleTheCards(Card[] deck)
    {
        for (int i = 0; i < deck.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, deck.Length);
            Card temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
        return deck;
    }

    public void AddCardToSlot(CardSlotUI slot)
    {
        if(_lastCard < _selectedPlayerCards.Length - 1)
        {
            slot.SetCard(_selectedPlayerCards[_lastCard + 1]);
            _lastCard += 1;
        }
        else if(_lastCard >= _selectedPlayerCards.Length - 1)
        {
            _lastCard = -1;
            slot.SetCard(_selectedPlayerCards[_lastCard + 1]);
        }
        
        
    }
}
