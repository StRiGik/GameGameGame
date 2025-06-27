using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private ManaSystem _manaSystem;
    [SerializeField] private GridManager _gridManager;

    // Попытка использовать карту типа "summoner"
    public void TryUseCard(Card card, CardSlotUI cardSlot, Vector2 worldPos)
    {
        if (!_manaSystem.TrySpendMana(card.ElexirCost)) return;

        Cell targetCell = _gridManager.GetNearestCell(worldPos);

        if (targetCell != null && card.SpellPrefab != null)
            Instantiate(card.SpellPrefab, targetCell.transform.position, Quaternion.identity);

        _deckManager.OnCardUsed(cardSlot);
    }

    // Попытка использовать карту типа "DirectedAction"
    public void TryUseCard(Card card, CardSlotUI cardSlot, GameObject targetObj)
    {
        if (!_manaSystem.TrySpendMana(card.ElexirCost)) return;

        if(targetObj != null)
            Debug.Log("Карта применена на объект: " + targetObj.name);

        _deckManager.OnCardUsed(cardSlot);
    }

    // Попытка использовать карту типа "Area"
    public void TryUseCard(Card card, CardSlotUI cardSlot, Vector3 targetArea)
    {
        if (!_manaSystem.TrySpendMana(card.ElexirCost)) return;
        Vector3 spellPos = targetArea;
        spellPos.z = 0;
        Instantiate(card.SpellPrefab, spellPos, Quaternion.identity);
        Debug.Log("карта применена на область" + spellPos);

        _deckManager.OnCardUsed(cardSlot);
    }
}
