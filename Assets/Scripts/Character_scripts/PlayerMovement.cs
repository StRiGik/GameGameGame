using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float _moveSpeed = 150f;   

    private Animator _anim;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private bool _isRight = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Moving();
        if (_moveInput.x < 0 && _isRight)
            Flip();
        else if (_moveInput.x > 0 && !_isRight)
            Flip();
    }

    private void Moving()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
        _anim.SetFloat("moveX", _moveInput.x);
        _anim.SetFloat("moveY", _moveInput.y);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _moveInput.normalized * _moveSpeed * Time.deltaTime;
    }

    void Flip()
    {
        _isRight = !_isRight;
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
