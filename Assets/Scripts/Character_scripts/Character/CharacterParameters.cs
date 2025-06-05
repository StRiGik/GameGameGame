using UnityEngine;

public class CharacterParameters : MonoBehaviour
{
    //�������� �������� ���������
    [SerializeField] private float _moveSpeed;
    public float GetCharMoveSpeed() { return _moveSpeed; }
    public void SetCharMoveSpeed(float newMoveSpeed) { _moveSpeed = newMoveSpeed; }

    //�������� ����� ���������
    [SerializeField] private float _damage;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackCooldown;
    public float GetCharDamage() { return _damage; }
    public float GetCharAtkDistance() { return _attackDistance; }
    public float GetCharAtkCooldown() { return _attackCooldown; }


}
