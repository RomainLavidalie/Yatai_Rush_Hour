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
    #region Public
    
    public bool _orderArrived;

    #endregion
    
    private void Awake()
    {
        _iaControler = transform.GetComponent<IARandomMovements>();
        _agent = _iaControler.agent;
    }

    private void Start()
    {
        transform.position = _startPosition.position;
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
        if (!_iaControler._hasTarget)
        {
            _iaControler.SetRandomPos();
        }
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
        if (!_iaControler._hasTarget)
        {
            _iaControler.RandomTargetAI();
        }
        
        //Running => Walking
        
        if (_agent.velocity.magnitude > 0f && _agent.velocity.magnitude < _runSpeedThreshold)
        {
            TransitionToState(IABehaviours.WALKING);
        }
        
        //Running => Served
        if (_orderArrived)
        {
            TransitionToState(IABehaviours.SERVED);
        }
    }

    private void OnExitRunning()
    {
        _animControls.ResetTrigger("RUN");
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
        _animControls.SetBool("WALK", true);
        
        _animControls.SetTrigger("WALK");
        
    }

    private void OnUpdateWalking()
    {
        //Teleport to respawn
        if (transform.position.Compare(_endPosition.position, 1))
        {
            transform.position = _startPosition.position;
            TransitionToState(IABehaviours.ORDERING);
        }
        
        if (!_iaControler._hasTarget)
        {
            _iaControler.RandomTargetAI();
        }
        
        //Walking => Running
        
        if (_agent.velocity.magnitude > 0f && _agent.velocity.magnitude >= _runSpeedThreshold)
        {
            TransitionToState(IABehaviours.RUNNING);
        } 
        
        //Walking => Served
        if (_orderArrived)
        {
            TransitionToState(IABehaviours.SERVED);
        }
    }

    private void OnExitWalking()
    {
        _animControls.SetBool("WALK", false);
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
        
        _iaControler.SetIATarget(_orderPosition.position);

        if (!transform.position.Compare(_orderPosition.position, 1))
        {
            _animControls.Play("Walking");
        }
        
        else
        {
            _animControls.SetBool("WALK", false);
            _animControls.SetBool("ORDER", true);
            
            if(_animControls.GetCurrentAnimatorStateInfo(0).IsName("Talking") && _animControls.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f)
            {
                _animControls.SetBool("ORDER", false);
                TransitionToState(IABehaviours.WALKING);
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
        _iaControler.SetIATarget(transform.position);
        _animControls.SetTrigger("WIN");
    }

    private void OnUpdateServed()
    {
        if(_animControls.GetCurrentAnimatorStateInfo(0).IsName("Win") && _animControls.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f)
        {
            _orderArrived = false;
            _iaControler.SetIATarget(_endPosition.position);
            _iaControler._hasTarget = true;
            TransitionToState(IABehaviours.WALKING);
        }
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
    [SerializeField] private Transform _orderPosition;
    [SerializeField] private Transform _endPosition;

    #endregion


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_orderPosition.position, new Vector3(.2f,2,.2f));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_endPosition.position, new Vector3(.2f,2,.2f));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_startPosition.position, new Vector3(.2f,2,.2f));
    }
}
