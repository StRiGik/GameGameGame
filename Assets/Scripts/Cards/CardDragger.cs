using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CardSlotUI))]
public class CardDragger : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Settings")]
    [SerializeField] private LayerMask _playableLayerMask;
    [SerializeField] private LayerMask _essensLayerMask;
    [SerializeField] private RectTransform _deckArea;
    [SerializeField] private float _dragScale = 1.2f; // Легкое увеличение при перетаскивании
    [SerializeField] private DeckManager _deck;
    [SerializeField] private Material _trueMaterial;
    [SerializeField] private Material _falseMaterial;
    [SerializeField] private Material _defoultMaterial;
    private CardSlotUI _slot;
    private RectTransform _draggingObject;
    private Canvas _canvas;
    private bool _isDragging;
    private GridManager _gridManager;
    private GameObject _areaIndicator;

    private List<GameObject> _selectedObjects = new List<GameObject>();




    private void Start()
    {
        _slot = GetComponent<CardSlotUI>();
        _gridManager = FindObjectOfType<GridManager>();
        _canvas = GetComponentInParent<Canvas>();
        _selectedObjects = new List<GameObject>();
        CreateDragObject();
    }

    private void CreateDragObject()
    {
        if(_slot.Card.CardType == CardType.DirectedAction)
            CareateDirectedActionTypeCardObject();
        else if(_slot.Card.CardType == CardType.Area)
            CreateAreaTypeCardObject();
        else if(_slot.Card.CardType == CardType.Summoners)
            CareateDirectedActionTypeCardObject();
    }

    private void CareateDirectedActionTypeCardObject()
    {
        // Создаем объект-копию карты
        GameObject dragObj = new GameObject("DraggedCard", typeof(Image), typeof(CanvasGroup));
        dragObj.transform.SetParent(_canvas.transform);
        dragObj.SetActive(false);

        // Настраиваем изображение
        Image dragImage = dragObj.GetComponent<Image>();
        dragImage.raycastTarget = false;
        dragImage.sprite = _slot.CardIcon.sprite;
        dragImage.preserveAspect = true;

        // Копируем размеры оригинальной карты
        _draggingObject = dragObj.GetComponent<RectTransform>();
        _draggingObject.sizeDelta = _slot.CardIcon.rectTransform.sizeDelta;
        _draggingObject.localScale = Vector3.one * _dragScale;

        // Настраиваем прозрачность
        CanvasGroup cg = dragObj.GetComponent<CanvasGroup>();
        cg.alpha = 0.8f;
    }

    private void CreateAreaTypeCardObject()
    {
        // Создаем объект-индикатор области
        _areaIndicator = new GameObject("AreaIndicator");
        _areaIndicator.transform.SetParent(_canvas.transform);
        _areaIndicator.SetActive(false);

        // Добавляем компоненты
        var image = _areaIndicator.AddComponent<Image>();
        image.raycastTarget = false;

        // Настраиваем цвет и прозрачность
        Color semiTransparentGray = new Color(0.5f, 0.5f, 0.5f, 0.4f); // Серый с прозрачностью 40%
        image.color = semiTransparentGray;

        // Создаем спрайт круга с нужным радиусом
        int pixelRadius = Mathf.RoundToInt(_slot.Card.Radius * 100f); // Переводим радиус в пиксели
        image.sprite = CreateCircleSprite(pixelRadius);

        // Настраиваем RectTransform
        _draggingObject = _areaIndicator.GetComponent<RectTransform>();
        float diameterInUnits = _slot.Card.Radius * 2f; // Диаметр в игровых единицах
        _draggingObject.sizeDelta = new Vector2(diameterInUnits, diameterInUnits);

        // Добавляем CanvasGroup для управления прозрачностью
        var canvasGroup = _areaIndicator.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0.7f; // Дополнительная прозрачность
    }

    private Sprite CreateCircleSprite(int diameter)
    {
    Texture2D texture = new Texture2D(diameter, diameter, TextureFormat.RGBA32, false);
    Color[] colors = new Color[diameter * diameter];
    
    Vector2 center = new Vector2(diameter / 2f, diameter / 2f);
    float radius = diameter / 2f;
    float feather = 5f; // Размер размытия краев

    for (int y = 0; y < diameter; y++)
    {
        for (int x = 0; x < diameter; x++)
        {
            float distance = Vector2.Distance(new Vector2(x, y), center);
            float alpha = Mathf.Clamp01((radius - distance) / feather);
            colors[y * diameter + x] = new Color(1, 1, 1, alpha);
        }
    }

    texture.SetPixels(colors);
    texture.Apply();
    return Sprite.Create(texture, new Rect(0, 0, diameter, diameter), Vector2.one * 0.5f, 100f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;

        switch (_slot.Card.CardType)
        {
            case CardType.Area:
                _areaIndicator.SetActive(true);
                _draggingObject.position = _slot.CardIcon.transform.position;
                break;

            case CardType.DirectedAction:
                _draggingObject.gameObject.SetActive(true);
                _draggingObject.position = _slot.CardIcon.transform.position;
                break;

            default:
                _draggingObject.gameObject.SetActive(true);
                _draggingObject.position = _slot.CardIcon.transform.position;
                break;
        }

        _slot.CardIcon.color = new Color(1, 1, 1, 0.3f);

    }

    public void OnDrag(PointerEventData eventData)
    {
       Vector2 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _draggingObject.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint);

        _draggingObject.localPosition = localPoint;

        // Проверка объектов под курсором (только для DirectedAction)
        if (_slot.Card.CardType == CardType.DirectedAction)
        {
            CheckObjectUnderCursor(worldPos);
        }
    }

    private void CheckObjectUnderCursor(Vector2 worldPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            worldPosition,
            Vector2.zero,
            Mathf.Infinity,
            _essensLayerMask);

        // 2. Если попали в объект
        if (hit.collider != null)
        {
            if(!_selectedObjects.Contains(hit.collider.gameObject))
                _selectedObjects.Add(hit.collider.gameObject);

            GameObject target = hit.collider.gameObject;
            Debug.Log(target);

            // 3. Проверяем, валидная ли это цель
            int targetGameObjLayer = target.layer;
            if((_slot.Card.TargetLayers & (1 << targetGameObjLayer)) != 0)
            {
                Debug.Log("цель найдена и покрашена");
                SetOutlineMaterial(target, _trueMaterial);
            }
            else
            {
                Debug.Log("Карту нельзя использовать на эту цель");
                SetOutlineMaterial(target, _falseMaterial);
            }
        }
        else
        {
            ResetOutlineMaterial();
        }

    }

    private void SetOutlineMaterial(GameObject target, Material material)
    {
        SpriteRenderer spriteRender = target.GetComponentInChildren<SpriteRenderer>();
        if (spriteRender != null)
        {
            spriteRender.material = material;
        }
    }

    private void ResetOutlineMaterial()
    {
        foreach (GameObject target in _selectedObjects)
        {
            if (target != null)
            {
                SpriteRenderer spriteRender = target.GetComponentInChildren<SpriteRenderer>();
                if (spriteRender != null)
                {
                    spriteRender.material = _defoultMaterial;
                }
            }
        }
        _selectedObjects.Clear();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetOutlineMaterial();
        if (!_isDragging) return;
        _isDragging = false;

        
        _slot.CardIcon.color = Color.white;
        _draggingObject.gameObject.SetActive(false);

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        if (IsOverDeckArea(eventData.position)) return;
        // Проверяем сброс на клетку через GridManager
        if (_slot.Card.CardType == CardType.Summoners)
        {
            Cell targetCell = _gridManager.GetNearestCell(worldPos);
            if (targetCell != null)
            {
                _slot.TryUseCard(targetCell); // Передаем клетку, куда сбросили
                Destroy(_draggingObject.gameObject);
                CreateDragObject();
            }
        }
        else if (_slot.Card.CardType == CardType.DirectedAction)
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, _essensLayerMask);
            if (hit.collider != null)
            {
                int targetLayer = hit.collider.gameObject.layer;
                if ((_slot.Card.TargetLayers & (1 << targetLayer)) != 0)
                {
                    _slot.TryUseCard(null, hit.collider.gameObject);
                    Destroy(_draggingObject.gameObject);
                    CreateDragObject();
                }
            }
        }
        else if(_slot.Card.CardType == CardType.Area)
        {
            _slot.TryUseCard(null, null, transform);
            Destroy(_draggingObject.gameObject);
            CreateDragObject();
        }
    }

    private bool IsOverDeckArea(Vector2 screenPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            _deckArea,
            screenPosition,
            _canvas.worldCamera);
    }

    private void OnDestroy()
    {
        if (_draggingObject != null)
            Destroy(_draggingObject.gameObject);
    }
}