using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bellhead : MonoBehaviour
{
    public static bool _isBellheadMove = false;

    private NavMeshAgent _agent;
    private GameObject _player;
    private Vector3 _targetPosition;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Move();
    }  

    private void Move()
    {
        _targetPosition = _player.transform.position;
        _agent.SetDestination(_targetPosition);
    }
}
