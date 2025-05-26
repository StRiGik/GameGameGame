using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public enum PlayerState { Idle, Moving }

    private PlayerState currentState = PlayerState.Idle;

    [Header("—сылки")]
    private Animator _anim;
    private Rigidbody2D _rb;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void ChangeState(PlayerState value)
    {
        if (value != currentState)
        {
            currentState = value;
            
        }
    }
}
