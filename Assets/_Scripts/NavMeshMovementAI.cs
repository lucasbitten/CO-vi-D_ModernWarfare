using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovementAI : MonoBehaviour
{
    public Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = destination.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
