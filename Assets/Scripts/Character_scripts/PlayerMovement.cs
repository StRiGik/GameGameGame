using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float _moveSpeed = 150f;


    [Header("Настройки Атаки")]
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _attackDistance = 5f;
    [SerializeField] private float _attackCooldown = 0.5f;
    [SerializeField] private LayerMask _enemyLayer;
    private float _lastAttackTime;


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

        if(Time.time -  _lastAttackTime > _attackCooldown)
        {
            TryAutoAttack();
        }
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


    void TryAutoAttack()
    {
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, _attackDistance, _enemyLayer);
        if (enemys.Length == 0) return;

        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D enemyCollider in enemys)
        {
            float distance = Vector2.Distance(transform.position, enemyCollider.transform.position);

            if(distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemyCollider.transform;
            }
        }

        if(closestEnemy != null)
        {
            _anim.SetTrigger("isAttack");
            closestEnemy.GetComponent<Health>().SetHealth(-_damage);
            _lastAttackTime = Time.time;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
    }
}
