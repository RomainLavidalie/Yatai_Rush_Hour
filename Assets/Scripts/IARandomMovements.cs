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
    private Vector3 _minPosition;
    private Vector3 _maxPosition;
    
    /// <summary>
    /// store remaining distance to set target
    /// </summary>
    private float _currentDistanceToTarget;

    #endregion

    #region Exposed private properties
    
    //radius of the area where we'll generate random position
    [SerializeField] private float _randomPositionRadius = 1;
    [SerializeField] private Transform _minArea;
    [SerializeField] private Transform _maxArea;

    #endregion


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _minPosition = _minArea.position;
        _maxPosition = _maxArea.position;
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
        _agentTarget = new Vector3(
            Random.Range(_minPosition.x, _maxPosition.x),
            transform.position.y,
            Random.Range(_minPosition.z, _maxPosition.z)
        );

        if (_agentTarget == transform.position)
        {
            RandomTargetAI();
        }

        agent.destination = _agentTarget;
        _hasTarget = true;
        
        //Debug.Log(agent.destination);
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
        /*Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _randomPositionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, _agentTarget);
        Gizmos.DrawWireCube(_agentTarget, new Vector3(0.1f,6,0.1f));
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(_minPosition, new Vector3(0.01f,3,0.01f));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_maxPosition, new Vector3(0.01f,3,0.01f));*/
    }
}
    