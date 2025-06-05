using System;
using UnityEngine;

public class CharacterFSM : MonoBehaviour
{
    public enum PlayerState { Idle, Moving, Attacking }
    public PlayerState CurrentState { get; private set; }

    public void OnMovementStarted() => TryChangeState(PlayerState.Moving);
    public void OnMovementStoped() => TryChangeState(PlayerState.Idle);
    public void OnAttackStarted() => TryChangeState(PlayerState.Attacking);
    public void OnAttackStoped() => TryChangeState(PlayerState.Idle);

    public event Action<PlayerState> OnStateChanged;

    private void TryChangeState(PlayerState newState)
    {
        //���������, ����� �� ������� � ����� ���������
        if (CanChangeState(newState))
        {
            ExitCurrentState();
            CurrentState = newState;
            OnStateChanged?.Invoke(CurrentState);
            Debug.Log("����� ��������� �����������, ��� : " + CurrentState.ToString());
            EnterNewState(newState);
        }
    }

    private bool CanChangeState(PlayerState newState)
    {
        if(newState == PlayerState.Idle && CurrentState == PlayerState.Attacking) { return false; }
        return true;
    }

    private void EnterNewState(PlayerState state)
    {
        Debug.Log("��������� ������ ���� ������:" + CurrentState);
    }

    private void ExitCurrentState()
    {
        Debug.Log("��������� �������� ���� ������: " + CurrentState);
    }
}
