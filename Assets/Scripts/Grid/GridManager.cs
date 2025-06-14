using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [Header("Tilemap Settings")]
    [SerializeField] private Tilemap _cellsTilemap;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private LayerMask _cellsLayerMask;

    private void Awake()
    {
        GenerateCells();
    }

    private void GenerateCells()
    {
        foreach (var pos in _cellsTilemap.cellBounds.allPositionsWithin)
        {
            if (_cellsTilemap.HasTile(pos))
            {
                Vector3 worldPos = _cellsTilemap.GetCellCenterWorld(pos);
                Instantiate(_cellPrefab, worldPos, Quaternion.identity, transform);
            }
        }
    }

    public Cell GetNearestCell(Vector2 worldPos)
    {
        Collider2D hit = Physics2D.OverlapPoint(worldPos, _cellsLayerMask);
        return hit?.GetComponent<Cell>();
    }
}