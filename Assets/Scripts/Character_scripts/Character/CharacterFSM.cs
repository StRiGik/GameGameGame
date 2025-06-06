using System;
using UnityEngine;

public class CharacterFSM : MonoBehaviour
{
    public enum PlayerState { Idle, Moving, Attacking }
    public PlayerState CurrentState { get; private set; } = PlayerState.Idle;
    //Флаг принудительной атаки
    private bool _forceAttacking = false;

    public void OnMovementStarted()
    {
        if(!_forceAttacking)
            TryChangeState(PlayerState.Moving);
    }
    public void OnMovementStoped()
    {
        if(!_forceAttacking)
            TryChangeState(PlayerState.Idle);
    }
    public void SetAttackState(bool shouldAttack)
    {
        _forceAttacking = shouldAttack;
        if (shouldAttack)
            TryChangeState(PlayerState.Attacking);
        else if (CurrentState == PlayerState.Attacking)
            TryChangeState(PlayerState.Idle);
    }

    public event Action<PlayerState> OnStateChanged;

    private void TryChangeState(PlayerState newState)
    {
        if (CurrentState == newState && newState != PlayerState.Attacking) return;
        if (_forceAttacking && newState != PlayerState.Attacking) return;
        //Проверяем, можно ли перейти в новое состояние
        if (CanChangeState(newState))
        {
            ExitCurrentState();
            CurrentState = newState;
            OnStateChanged?.Invoke(CurrentState);
            Debug.Log("новое состояние установлено, это : " + CurrentState.ToString());
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
        Debug.Log("состояние начало свою работу:" + CurrentState);
    }

    private void ExitCurrentState()
    {
        Debug.Log("состояние звершило свою работу: " + CurrentState);
    }
}
