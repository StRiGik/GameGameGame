using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [Header("Основные настройки")]
    [SerializeField] private Transform _target; //Цель(персонаж)
    [SerializeField] private float _smoothSpeed = 5f; //плавность (5-10)

    [Header("Смещение и зона комфорта")]
    [SerializeField] private Vector2 _offset = new Vector2(0f, 1f); //Смещение вверх
    [SerializeField] private Vector2 _deadZoneSize = new Vector2(2f, 1f); //Зона без движения

    [Header("Границы уровня")]
    [SerializeField] private bool _useBounds = false;
    [SerializeField] private Vector2 _minBounds, _maxBounds;

    [Header("Опережающее движение")]
    [SerializeField] private bool _lookAhead = true;
    [SerializeField] private float _lookAheadDistance = 2f;

    private Vector3 _velocity = Vector3.zero;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        CalculateDeadZone();
    }

    private void FixedUpdate()
    {
        if (_target == null) return;

        Vector3 targetPosition = CalculateTargetPosition();
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, 1 / _smoothSpeed);

        if (_useBounds)
        {
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, _minBounds.x, _maxBounds.x);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, _minBounds.y, _maxBounds.y);
        }

        transform.position = smoothedPosition;
    }

    private Vector3 CalculateTargetPosition()
    {

        if (_lookAhead)
        {
            float direction = _target.localScale.x; // Направление взгляда
            _offset.x = Mathf.Lerp(_offset.x, direction * _lookAheadDistance, Time.deltaTime * 2);
        }

        Vector3 targetPos = _target.position + (Vector3)_offset;

        //Если персонаж в "мертвой зоне"--камера не двигается
        float xDelta = Mathf.Abs(transform.position.x - targetPos.x);
        float yDelta = Mathf.Abs(transform.position.y - targetPos.y);

        if (xDelta < _deadZoneSize.x / 2 && yDelta < _deadZoneSize.y / 2)
            return transform.position;

        //Плавное смещение к краю зоны
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if(xDelta >= _deadZoneSize.x / 2)
            targetX = targetPos.x - (_deadZoneSize.x / 2) * Mathf.Sign(transform.position.x - targetPos.x);

        if (yDelta >= _deadZoneSize.y / 2)
            targetY = targetPos.y - (_deadZoneSize.y / 2) * Mathf.Sign(transform.position.x - targetPos.y);
        
        return new Vector3(targetX, targetY, transform.position.z);
    }

    private void CalculateDeadZone()
    {
        // Автоматический расчёт зоны комфорта под размер экрана
        if (_camera != null && _deadZoneSize == Vector2.zero)
        {
            float height = 2f * _camera.orthographicSize;
            float width = height * _camera.aspect;
            _deadZoneSize = new Vector2(width * 0.6f, height * 0.4f);
        }
    }

    // Для визуализации в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + (Vector3)_offset, new Vector3(_deadZoneSize.x, _deadZoneSize.y, 0));
    }

}
