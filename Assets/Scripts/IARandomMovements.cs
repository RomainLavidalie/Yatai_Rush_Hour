using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IARandomMovements : MonoBehaviour
{
    #region Public

    /// <summary>
    /// check if the nav mesh agent has been assigned a target position
    /// </summary>
    public bool _hasTarget;
    
    public NavMeshAgent agent;

    #endregion

    #region Private
    
    private Vector3 _agentTarget;
    
    /// <summary>
    /// store remaining distance to set target
    /// </summary>
    private float _currentDistanceToTarget;

    #endregion

    #region Exposed private properties
    
    //radius of the area where we'll generate random position
    [SerializeField] private float _randomPositionRadius = 1;

    #endregion


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _currentDistanceToTarget = agent.remainingDistance;

        if (_agentTarget.Compare(transform.position, 1))
        {
            _hasTarget = false;
        }
    }

    //methods called by the state machine
    #region Custom public methods

    /// <summary>
    /// will randomly generate a random position
    /// </summary>
    public void RandomTargetAI()
    {
        _agentTarget = Random.insideUnitSphere * _randomPositionRadius;
        _agentTarget.y = transform.position.y;    
        
        agent.destination = _agentTarget;
        _hasTarget = true;
    }

    /// <summary>
    /// keep track of agent distance to target and generate another position 
    /// </summary>
    public void SetRandomPos()
    {
        if (_currentDistanceToTarget <= 0)
        {
            RandomTargetAI();
        }
    }

    /// <summary>
    /// enforce a defined position as target
    /// </summary>
    /// <param name="posToTarget">the transform we want to target</param>
    public void SetIATarget(Vector3 posToTarget)
    {
        _agentTarget = posToTarget;
        _agentTarget.y = transform.position.y;
        agent.destination = _agentTarget;
    }

    #endregion

    //Gizmos in editor to show radius of generation area and ray to targets
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _randomPositionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _agentTarget);
        Gizmos.DrawWireCube(_agentTarget, new Vector3(0.1f,2,0.1f));
    }
}
    