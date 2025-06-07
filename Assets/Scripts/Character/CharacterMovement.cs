using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterParameters _parametrs;
    [SerializeField] private CharacterFSM _fsm;

    private Rigidbody2D _rb;
    private float _startSpeed;
    private float _currentSpeed;
    private Vector2 _moveInput;
    private bool _isRight = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startSpeed = _parametrs.GetCharMoveSpeed();
        _currentSpeed = _startSpeed;
    }

    private void Update()
    {
        Moving();
    }

    private void Moving()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
        if (_moveInput.x != 0 || _moveInput.y != 0)
        {
            _fsm.OnMovementStarted();
        }
        else
            _fsm.OnMovementStoped();

        if (_moveInput.x < 0 && _isRight)
            Flip();
        else if (_moveInput.x > 0 && !_isRight)
            Flip();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _moveInput.normalized * _currentSpeed * Time.deltaTime;
    }

    void Flip()
    {
        _isRight = !_isRight;
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }


}
