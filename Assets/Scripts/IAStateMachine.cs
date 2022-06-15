using System;
using System.Collections;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public enum IABehaviours
    {
        IDLE,
        WALKING,
        RUNNING,
        DANCING,
        ORDERING,
        SERVED
    }

public class IAStateMachine : MonoBehaviour
{
    private void Awake()
    {
        _iaControler = transform.GetComponent<IARandomMovements>();
        _agent = _iaControler.agent;
    }

    private void Start()
    {
        _currentIAState = IABehaviours.ORDERING;
        OnStateEnter(_currentIAState);
    }

    private void Update()
    {
        OnStateUpdate(_currentIAState);
    }

    #region State Machine

    private void OnStateEnter(IABehaviours state)
    {
        switch (state)
        {
            case IABehaviours.IDLE:
                OnEnterIdle();
                break;
            case IABehaviours.DANCING:
                OnEnterDancing();
                break;
            case IABehaviours.RUNNING:
                OnEnterRunning();
                break;
            case  IABehaviours.WALKING:
                OnEnterWalking();
                break;
            case IABehaviours.ORDERING:
                OnEnterOrdering();
                break;
            case IABehaviours.SERVED:
                OnEnterServed();
                break;
            default: 
                Debug.LogError($"Trying to entering non-existent state : {state.ToString()}");
                break;
        }
    }
    
    private void OnStateUpdate(IABehaviours state)
    {
        switch (state)
        {
            case IABehaviours.IDLE:
                OnUpdateIdle();
                break;
            case IABehaviours.DANCING:
                OnUpdateDancing();
                break;
            case IABehaviours.RUNNING:
                OnUpdateRunning();
                break;
            case  IABehaviours.WALKING:
                OnUpdateWalking();
                break;
            case IABehaviours.ORDERING:
                OnUpdateOrdering();
                break;
            case IABehaviours.SERVED:
                OnUpdateServed();
                break;
            default: 
                Debug.LogError($"Trying to updating non-existent state : {state.ToString()}");
                break;
        }
    }

    private void OnStateExit(IABehaviours state)
    {
        switch (state)
        {
            case IABehaviours.IDLE:
                OnExitIdle();
                break;
            case IABehaviours.DANCING:
                OnExitDancing();
                break;
            case IABehaviours.RUNNING:
                OnExitRunning();
                break;
            case  IABehaviours.WALKING:
                OnExitWalking();
                break;
            case IABehaviours.ORDERING:
                OnExitOrdering();
                break;
            case IABehaviours.SERVED:
                OnExitServed();
                break;
            default: 
                Debug.LogError($"Trying to exit non-existent state : {state.ToString()}");
                break;
        }
    }
    
    private void TransitionToState(IABehaviours newIAState)
    {
        OnStateExit(_currentIAState);
        _currentIAState = newIAState;
        OnStateEnter(newIAState);
    }

    #endregion

    #region IDLE State

    private void OnEnterIdle()
    {
        _animControls.SetTrigger("IDLE");
    }

    private void OnUpdateIdle()
    {
        //Idle => Running
        
        if (_agent.velocity.magnitude > 0f && _agent.velocity.magnitude >= _runSpeedThreshold)
        {
            TransitionToState(IABehaviours.RUNNING);
        } 
        
        //Idle => Dancing
        
        //Idle => Walking
        if (_agent.velocity.magnitude > 0f && _agent.velocity.magnitude < _runSpeedThreshold)
        {
            TransitionToState(IABehaviours.WALKING);
        } 
        
        //Idle => Ordering
    }

    private void OnExitIdle()
    {
        
    }

    #endregion

    #region RUN State

    private void OnEnterRunning()
    {
        _animControls.SetTrigger("RUN");
    }

    private void OnUpdateRunning()
    {
        //Running => Idle
        
        if (_agent.velocity.magnitude > 0f && _agent.velocity.magnitude < _runSpeedThreshold)
        {
            TransitionToState(IABehaviours.WALKING);
        } 
        
        //Running => Dancing

        //Running => Idle
        if (_agent.velocity.magnitude == 0f && _iaControler._hasTarget == false)
        {
            TransitionToState(IABehaviours.IDLE);
        } 
        //Running => Ordering
    }

    private void OnExitRunning()
    {
        
    }

    #endregion

    #region DANCING State

    private void OnEnterDancing()
    {
        
    }

    private void OnUpdateDancing()
    {
        //Dancing => Running
        
        //Dancing => Idle
        
        //Dancing => Walking
        
        //Dancing => Ordering
    }

    private void OnExitDancing()
    {
        
    }

    #endregion

    #region WALKING State

    private void OnEnterWalking()
    {
        _animControls.SetTrigger("WALK");
    }

    private void OnUpdateWalking()
    {
        //Walking => Running
        
        if (_agent.velocity.magnitude > 0f && _agent.velocity.magnitude >= _runSpeedThreshold)
        {
            TransitionToState(IABehaviours.RUNNING);
        } 
        
        //Walking => Dancing
        
        //Walking => Idle
        if (_agent.velocity.magnitude == 0f && _iaControler._hasTarget == false)
        {
            TransitionToState(IABehaviours.IDLE);
        } 
        
        //Walking => Ordering
    }

    private void OnExitWalking()
    {
        
    }

    #endregion

    #region ORDERING State

    private void OnEnterOrdering()
    {
        
    }

    private void OnUpdateOrdering()
    {
        //Ordering => Running
        
        //Ordering => Dancing
        
        //Ordering => Walking
        
        //Ordering => Idle
        
        _iaControler.SetIATarget(_startPosition.position);

        if (!transform.position.Compare(_startPosition.position, 1))
        {
            _animControls.Play("Walking");
        }
        
        else
        {
            _animControls.SetBool("WALK", false);
            _animControls.SetBool("ORDER", true);
            
            if(_animControls.GetCurrentAnimatorStateInfo(0).IsName("Talking") && _animControls.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                _animControls.SetBool("ORDER", false);
                TransitionToState(IABehaviours.IDLE);
                
            }
        }
        
        
    }

    private void OnExitOrdering()
    {
        
    }

    #endregion

    #region SERVED state

    private void OnEnterServed()
    {
        
    }

    private void OnUpdateServed()
    {
        
    }

    private void OnExitServed()
    {
        
    }

    #endregion
    
    #region Private

    private IABehaviours _currentIAState;
    private NavMeshAgent _agent;
    private IARandomMovements _iaControler;

    [SerializeField] private float _runSpeedThreshold;
    [SerializeField] private Animator _animControls;
    [SerializeField] private Transform _startPosition;

    #endregion


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(_startPosition.position, new Vector3(.2f,2,.2f));
    }
}
