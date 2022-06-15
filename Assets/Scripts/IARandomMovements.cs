using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class IARandomMovements : MonoBehaviour
{
    [SerializeField] private float _randomPositionRadius = 1;

    private Vector3 _aiTarget;
    
    private float _currentDistanceToTarget;

    public bool _hasTarget;
    
    public NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _currentDistanceToTarget = agent.remainingDistance;

        if (_aiTarget != Vector3.zero)
        {
            _hasTarget = true;
        }
    }

    public void RandomTargetAI()
    {
        _aiTarget = Random.insideUnitSphere * _randomPositionRadius;
        _aiTarget.y = transform.position.y;    
        agent.destination = _aiTarget;
    }

    public void SetRandomPos()
    {
        if (_currentDistanceToTarget <= 0)
        {
            RandomTargetAI();
        }
    }

    public void SetIATarget(Vector3 posToTarget)
    {
        _aiTarget = posToTarget;
        _aiTarget.y = transform.position.y;
        agent.destination = _aiTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _randomPositionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _aiTarget);
        Gizmos.DrawWireCube(_aiTarget, new Vector3(0.1f,2,0.1f));
    }
}
    