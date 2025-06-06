using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    [Header("—сылки")]
    [SerializeField] private CharacterFSM _charFSM;
    private Animator _anim;

    [Header("ѕараметры анимации")]
    [SerializeField] private string _idleParam = "Idle";
    [SerializeField] private string _moveParam = "Moving";
    [SerializeField] private string _attackParam = "Attack";

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();

        if (_charFSM == null)
        {
            Debug.LogError("—сылка на FSM неактивна. проверьте FSM игрока.");
            return;
        }
        
        _charFSM.OnStateChanged += HandleStateChange;
    }

    private void HandleStateChange(CharacterFSM.PlayerState newState)
    {
            ResetAllParametrs();

        switch (newState)
        {
            case CharacterFSM.PlayerState.Idle:
                _anim.SetBool(_idleParam, true); 
                break;
            case CharacterFSM.PlayerState.Moving:
                _anim.SetBool(_moveParam, true);
                Debug.Log("запуска анимации движени€");
                break;
            case CharacterFSM.PlayerState.Attacking:
                _anim.SetTrigger(_attackParam);
                break;


        }
    }
    
    private void ResetAllParametrs()
    {
        _anim.SetBool(_idleParam, false);
        _anim.SetBool(_moveParam, false);
    }

    private void OnDestroy()
    {
        if(_charFSM != null)
            _charFSM.OnStateChanged -= HandleStateChange;
    }
}
