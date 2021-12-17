using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class IARandomMovements : MonoBehaviour
{
    [SerializeField] private float _randomPositionRadius = 1;

    private Vector3 _aiTarget;
    
    private float _currentDistanceToTarget;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        RandomTargetAI();
        _currentDistanceToTarget = _agent.remainingDistance;
    }

    private void Update()
    {
        SetRandomPos();
        Debug.Log($"IA Agent destination : {_aiTarget}");
    }

    private void RandomTargetAI()
    {
        _aiTarget = Random.insideUnitSphere * _randomPositionRadius;
        _agent.destination = _aiTarget;
    }

    private void SetRandomPos()
    {
        
        if (_currentDistanceToTarget <= 0)
        {
            RandomTargetAI();
        }
    }
}
    