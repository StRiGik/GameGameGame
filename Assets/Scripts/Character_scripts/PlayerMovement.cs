using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _rb.velocity = _moveInput.normalized * _moveSpeed;
    }
}
