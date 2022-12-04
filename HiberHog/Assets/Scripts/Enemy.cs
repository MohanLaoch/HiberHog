using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private List <GameObject> Foods = new List <GameObject> ();
    public NavMeshAgent agent;
    public Animator SquirrelAnim;

    public Transform player;
    public Transform food;

    public LayerMask Ground, Player, Food, CollectedFood;


    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float sightRange;
    public bool PlayerInSightRange;
    public bool FoodInSightRange;

    public string foodTag = "Food";

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    public void UpdateTarget()
    {
        GameObject[] foods = GameObject.FindGameObjectsWithTag(foodTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestFood = null;


        foreach (GameObject food in foods)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, food.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestFood = food;
            }
        }

        if(nearestFood != null && shortestDistance <= sightRange)
        {
            food = nearestFood.transform;
        }
        else
        {
            food = null;
        }
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
        SquirrelAnim.SetBool("IsRunning", false);

        if (!walkPointSet)
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
        SquirrelAnim.SetBool("IsRunning", true);
        agent.SetDestination(player.position);
        transform.LookAt(player);

    }

    public void ChaseFood()
    {

        SquirrelAnim.SetBool("IsRunning", true);
        agent.SetDestination(food.position);
        transform.LookAt(food);
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sightRange);
    }

    /*ok so the main problem is that the ai only has one food transform that it can chase and collect when it needs to be able to
     * chase any of the food in the scene. one thing i can do is make a list of all the food transforms in the scene and manually click and drag.
     * another method is to have the enemy chase the closest food transform after it hits the player 
     */

}
