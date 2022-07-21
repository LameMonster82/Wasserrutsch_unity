using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RobotAI : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    float timeToWait;
    float _timeKeeper;
    [SerializeField]
    float searchRadius;
    
    void FixedUpdate()
    {
        //print(agent.hasPath);
        if (!agent.hasPath && _timeKeeper <= 0)
        {
            var newPointToGo = transform.position + new Vector3(Random.Range(-searchRadius, searchRadius), 0, Random.Range(-searchRadius, searchRadius));
            agent.SetDestination(newPointToGo);

            _timeKeeper = timeToWait;
        } else if (!agent.hasPath)
            _timeKeeper -= Time.fixedDeltaTime;
        
    }
}
