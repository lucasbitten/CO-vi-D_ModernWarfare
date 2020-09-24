using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovementAI : MonoBehaviour
{
    public Transform destination;
    public List<Transform> destinations;
    public int destinationIndex;
    public int counter = 0;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        destinationIndex = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = destinations[destinationIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, destination.position) <= 1)
        {
            Debug.Log("Aproximate");
            counter++;
            destinationIndex = counter % destinations.Count;
            destination = destinations[destinationIndex];
            SetNewDestination();
        }
        //Debug.Log($"Agent = {this.transform.position}\n" +
        //          $"Destination = {destination.position}");
    }

    void SetNewDestination()
    {
        agent.destination = destination.position;
    }
}
