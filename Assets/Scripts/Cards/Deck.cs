using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Card[] _cardDeck = new Card[4];

    public Card[] PlayerCards => _cardDeck;
}
