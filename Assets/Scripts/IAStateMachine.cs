using TMPro;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// States IA might go through
/// </summary>
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
    
    /// <summary>
    /// bool used to enter SERVED state and restart ia loop
    /// </summary>
    public bool _orderArrived;
    
    /// <summary>
    /// the position where we spawn each agent
    /// </summary>
    public Transform _startPosition;
    
    /// <summary>
    /// the position next to the yatai 
    /// </summary>
    public Transform _orderPosition;
    
    /// <summary>
    /// the last position the agent will go after we ended the order
    /// </summary>
    public Transform _endPosition;

    public GameObject _character;

    #endregion
    
    
    #region Private

    //references to ia agent component on this GO
    private IABehaviours _currentIAState;
    private NavMeshAgent _agent;
    private IARandomMovements _iaControler;

    private float changeDestinationTimer;
    
    #endregion

    #region Private exposed properties
    
    /// <summary>
    /// threshold used to interpolate between walk and run animation
    /// </summary>
    [SerializeField] private float _runSpeedThreshold;

    [SerializeField] FoodCustomerUI _foodCustomerUI;
    [SerializeField] private Animator _animControls;
    
 

    #endregion
    
    private void Awake()
    {
        //reference to the ia controler on this game object
        _iaControler = transform.GetComponent<IARandomMovements>();
        
        //store a reference to agent component of navMesh
        _agent = _iaControler.agent;
    }

    private void Start()
    {
        transform.position = _startPosition.position;
        
        //we set start state (in this case ORDERING)
        _currentIAState = IABehaviours.ORDERING;
        OnStateEnter(_currentIAState);
    }

    private void Update()
    {
        //on the main thread we update each state
        OnStateUpdate(_currentIAState);

        if (_currentIAState == IABehaviours.WALKING)
        {
            changeDestinationTimer += Time.deltaTime;
        }
    }

    #region State Machine
    
    
    private void OnStateEnter(IABehaviours state)
    {
        switch (state)
        {
            case IABehaviours.IDLE:
                OnEnterIdle();
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
    
    /// <summary>
    /// core function of the state machine, call it to switch between states whenever you needs to
    /// </summary>
    /// <param name="newIAState">the state we want to enter</param>
    private void TransitionToState(IABehaviours newIAState)
    {
        OnStateExit(_currentIAState);
        _currentIAState = newIAState;
        OnStateEnter(newIAState);
    }

    #endregion

    //each state works the same way
    #region IDLE State

    //works like a Start function for this state
    private void OnEnterIdle()
    {
        _animControls.SetTrigger("IDLE");
    }

    //the main loop of the state where you want to check conditioons to enter other states
    private void OnUpdateIdle()
    {
        //we check if the _iaControler has already a target position, if not we set a random position
        if (!_iaControler._hasTarget)
        {
            _iaControler.SetRandomPos();
        }
        
        //Idle => Running
        
        if (_agent.velocity.magnitude > 0f && _agent.velocity.magnitude >= _runSpeedThreshold)
        {
            TransitionToState(IABehaviours.RUNNING);
        } 
        
        
        //Idle => Walking
        if (_agent.velocity.magnitude > 0f && _agent.velocity.magnitude < _runSpeedThreshold)
        {
            TransitionToState(IABehaviours.WALKING);
        } 
    }
    
    //if you need to do some change just before switching state
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

    // #region DANCING State
    //
    // private void OnEnterDancing()
    // {
    //     
    // }
    //
    // private void OnUpdateDancing()
    // {
    //     //Dancing => Running
    //     
    //     //Dancing => Idle
    //     
    //     //Dancing => Walking
    //     
    //     //Dancing => Ordering
    // }
    //
    // private void OnExitDancing()
    // {
    //     
    // }
    //
    // #endregion

    #region WALKING State

    private void OnEnterWalking()
    {
        _animControls.SetBool("WALK", true);
        
        _animControls.SetTrigger("WALK");
        
    }

    private void OnUpdateWalking()
    {
        //Teleport to respawn when the agent hit _endPosition
        if (transform.position.Compare(_endPosition.position, 1))
        {
            transform.position = _startPosition.position;
            TransitionToState(IABehaviours.ORDERING);
        }

        if (changeDestinationTimer > 10)
        {
            _iaControler._hasTarget = false;
            TransitionToState(IABehaviours.WALKING);
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
        changeDestinationTimer = 0;
        _animControls.SetBool("WALK", false);
    }

    #endregion

    #region ORDERING State

    private void OnEnterOrdering()
    {
        
    }

    private void OnUpdateOrdering()
    {
        //Ordering => Walking
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
                _iaControler._hasTarget = false;
                TransitionToState(IABehaviours.WALKING);
            }
        }
    }

    private void OnExitOrdering()
    {
        _foodCustomerUI.ChangeColor(_character.GetComponent<SkinnedMeshRenderer>().material.color);
        _foodCustomerUI.AddRamen();
    }

    #endregion

    #region SERVED state

    private void OnEnterServed()
    {
        _iaControler.SetIATarget(transform.position);
        _animControls.SetTrigger("WIN");
        _foodCustomerUI.RemoveRamen();
    }

    private void OnUpdateServed()
    {
        //Served => Walking
        //final step of the ia loop, we set the agent target position to _endPosition. _hasTarget is just here to ensure no random pos will change ia target
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
    
    //Gizmos in editor to show where fixed target positions are
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
