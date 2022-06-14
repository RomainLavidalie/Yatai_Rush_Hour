using System;
using UnityEngine;
using UnityEngine.AI;

public enum IABehaviours
    {
        IDLE,
        WALKING,
        RUNNING,
        DANCING,
        ORDERING
    }

public class IAStateMachine : MonoBehaviour
{
    #region Exposed

    [SerializeField] private NavMeshAgent _agent;

    #endregion

    private void Start()
    {
        _currentIAState = IABehaviours.IDLE;
        OnStateEnter(_currentIAState);
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
        
    }

    private void OnUpdateIdle()
    {
        
    }

    private void OnExitIdle()
    {
        
    }

    #endregion

    #region RUN State

    private void OnEnterRunning()
    {
        
    }

    private void OnUpdateRunning()
    {
        
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
        
    }

    private void OnExitDancing()
    {
        
    }

    #endregion

    #region WALKING State

    private void OnEnterWalking()
    {
        
    }

    private void OnUpdateWalking()
    {
        
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
        
    }

    private void OnExitOrdering()
    {
        
    }

    #endregion
    
    #region Private

    private IABehaviours _currentIAState;

    #endregion
}
