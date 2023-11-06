using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    NavMeshAgent enemy;
    Rigidbody rb;

    //assign layers for the ground and player
    [SerializeField] LayerMask groundLayer, playerLayer;

    //set new destination for patrol
    Vector3 newDestination;

    //
    bool enableWalk;
    [SerializeField] float range;

    //state change
    [SerializeField] float sightRange, attackRange;
    bool playerInSight, playerInAttackRange;

    void Start()
    {
        //get enemy and player object
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSight && !playerInAttackRange) Patrol();
        if (playerInSight && !playerInAttackRange) Chase();
        if (playerInSight && playerInAttackRange) Attack();
    }


    //enemy chase player function
    void Chase()
    {
        enemy.SetDestination(player.transform.position);
        
    }

    //enemy attack player function
    void Attack()
    {
    }

    
    //enemy on patrol function
    void Patrol()
    {
        if (!enableWalk) SetNewDest();
        if (enableWalk) enemy.SetDestination(newDestination);

        //set enemy walk to false if new destination is below range
        if (Vector3.Distance(transform.position, newDestination) < 10) enableWalk = false;
    }

    //set new destination for enemy to walk to
    void SetNewDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        newDestination = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(newDestination, Vector3.down, groundLayer))
        {
            enableWalk = true;
        }
    }
}
