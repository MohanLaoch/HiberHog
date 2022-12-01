using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private List <GameObject> Foods = new List <GameObject> ();
    public NavMeshAgent agent;

    public Transform player;
    public Transform food;

    public LayerMask Ground, Player, Food, CollectedFood;


    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float sightRange;
    public bool PlayerInSightRange;
    public bool FoodInSightRange;

    private void Awake()
    {
        player = GameObject.Find("TestPlayer").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        PlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        FoodInSightRange = Physics.CheckSphere(transform.position, sightRange, CollectedFood);

        if (!PlayerInSightRange)
        {
            Patrolling();
        }

        if (PlayerInSightRange)
        {
            ChasePlayer();
        }

        if(FoodInSightRange && PlayerInSightRange)
        {
            ChaseFood();
        }
        


    }

    private void Patrolling()
    {
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }
        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude<1f)
        {
            walkPointSet = false;   
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
        {
            walkPointSet = true;
        }

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);

    }

    public void ChaseFood()
    {
      

        agent.SetDestination(food.position);
        transform.LookAt(food);
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sightRange);
    }

}
