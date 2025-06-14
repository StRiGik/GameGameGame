using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool IsOccupied;
    private SpriteRenderer _highlight;

    private void Awake()
    {
        _highlight = GetComponent<SpriteRenderer>();
    }

    public void SetHighlight(bool active)
    {
        if (_highlight != null)
            _highlight.enabled = active;
    }

    public bool TryPlayCard()
    {
        if (IsOccupied) return false;
        IsOccupied = true;
        Invoke(nameof(ResetOccupation), 2f); // Через 2 секунды клетка освобождается
        return true;
    }

    private void ResetOccupation() => IsOccupied = false;
}