using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private CharacterParameters _parametrs;
    [SerializeField] private CharacterFSM _fsm; 

    private float _damage;
    private float _attackDistance = 1f;
    private float _attackCooldown;
    [SerializeField] private LayerMask _enemyLayer;
    private float _lastAttackTime;

    private void Start()
    {
        _damage = _parametrs.GetCharDamage();
        _attackDistance = _parametrs.GetCharAtkDistance();
        _attackCooldown = _parametrs.GetCharAtkCooldown();
    }

    private void Update()
    {
        if (Time.time - _lastAttackTime > _attackCooldown)
            TryAutoAttack();
    }

    private void TryAutoAttack()
    {
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, _attackDistance, _enemyLayer);
        if (enemys.Length == 0)
        {
            _fsm.SetAttackState(enemys.Length > 0);
            return;
        }

        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D enemyCollider in enemys)
        {
            float distance = Vector2.Distance(transform.position, enemyCollider.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemyCollider.transform;
            }
        }

        if (closestEnemy != null)
        {
            _fsm.SetAttackState(enemys.Length > 0);
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

