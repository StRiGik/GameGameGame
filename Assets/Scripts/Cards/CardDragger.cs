using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CardSlotUI))]
public class CardDragger : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Settings")]
    [SerializeField] private LayerMask _playableLayerMask; // ��������� � ����������
    [SerializeField] private float _dragScale = 1.2f; // ������ ���������� ��� ��������������
    [SerializeField] private DeckManager _deck;
    private CardSlotUI _slot;
    private RectTransform _draggingObject;
    private Canvas _canvas;
    private bool _isDragging;
    private GridManager _gridManager;
    

    private void Start()
    {
        _slot = GetComponent<CardSlotUI>();
        _gridManager = FindObjectOfType<GridManager>();
        _canvas = GetComponentInParent<Canvas>();

        CreateDragObject();
    }

    private void CreateDragObject()
    {
        // ������� ������-����� �����
        GameObject dragObj = new GameObject("DraggedCard", typeof(Image), typeof(CanvasGroup));
        dragObj.transform.SetParent(_canvas.transform);
        dragObj.SetActive(false);

        // ����������� �����������
        Image dragImage = dragObj.GetComponent<Image>();
        dragImage.raycastTarget = false;
        dragImage.sprite = _slot.CardIcon.sprite;
        dragImage.preserveAspect = true;

        // �������� ������� ������������ �����
        _draggingObject = dragObj.GetComponent<RectTransform>();
        _draggingObject.sizeDelta = _slot.CardIcon.rectTransform.sizeDelta;
        _draggingObject.localScale = Vector3.one * _dragScale;

        // ����������� ������������
        CanvasGroup cg = dragObj.GetComponent<CanvasGroup>();
        cg.alpha = 0.8f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        _draggingObject.gameObject.SetActive(true);
        _draggingObject.position = _slot.CardIcon.transform.position;
        _slot.CardIcon.color = new Color(1, 1, 1, 0.3f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _draggingObject.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint);

        _draggingObject.localPosition = localPoint;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isDragging) return;
        _isDragging = false;

        _slot.CardIcon.color = Color.white;
        _draggingObject.gameObject.SetActive(false);

        // ��������� ����� �� ������ ����� GridManager
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        Cell targetCell = _gridManager.GetNearestCell(worldPos);

        if (targetCell != null)
        {
            _slot.TryUseCard(targetCell); // �������� ������, ���� ��������
            _draggingObject.GetComponent<Image>().sprite = _slot.CardIcon.sprite; //��������� ��������
        }
    }

    private void OnDestroy()
    {
        if (_draggingObject != null)
            Destroy(_draggingObject.gameObject);
    }
}