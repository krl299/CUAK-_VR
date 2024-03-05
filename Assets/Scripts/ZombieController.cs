using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private StateMachine brain;
    private Animator animator;
    private NavMeshAgent agent;
    private PlayerController player;
    public Transform playerT;


    [SerializeField] private bool playerIsNear;
    [SerializeField] private bool withinAttackRange;

    private float changeMind;
    private float attackTimer;
    public float health;
    public int enemyScorePoint;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    [SerializeField] bool alreadyAttacked;
    public GameObject projectileGraphic;
    public GameObject projectile;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        brain = GetComponent<StateMachine>();
        playerT = GameObject.Find("Player").transform;

        playerIsNear = false;
        withinAttackRange = false;

        //Create the first Enemy State = Idle
        brain.PushState(Idle, OnIdleEnter, OnIdleExit);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //HUDController.instance.ScoreIncrement(enemyScorePoint);
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        //5m start chasing the player
        playerIsNear = Vector3.Distance(transform.position, player.transform.position) < 10;

        //1m start attacking the player
        withinAttackRange = Vector3.Distance(transform.position, player.transform.position) < 5;
    }

    //----------------------------------------------------------------
    #region IDLE_STATE
    private void OnIdleEnter()
    {
        //reset the NavMesh paths to follow the player
        agent.ResetPath();
    }

    private void Idle()
    {
        Debug.Log("Idle");
        changeMind -= Time.deltaTime;
        if (playerIsNear)
        {
            //Go to next state Chase
            brain.PushState(Chase, OnChaseEnter, OnChaseExit);
        }
        else if (changeMind <= 0)
        {
            //Go to next state Wander
            brain.PushState(Wander, OnWanderEnter, OnWanderExit);
            changeMind = Random.Range(2, 5);
        }
    }

    private void OnIdleExit()
    {

    }
    #endregion

    //----------------------------------------------------------------
    #region CHASE_STATE
    private void OnChaseEnter()
    {
        //change the animation state
        animator.SetBool("Chase", true);
    }

    private void Chase()
    {
        Debug.Log("Chase");
        //Chase the Player
        agent.SetDestination(player.transform.position);
        //If not near eliminate current state
        if (!playerIsNear)
        {
            brain.PopState();
        }
        //if within attack range create new state Attack
        if (withinAttackRange)
        {
            brain.PushState(Attack, OnAttackEnter, OnAttackExit);
        }
    }

    private void OnChaseExit()
    {
        //change the animation state
        animator.SetBool("Chase", false);
    }

    #endregion

    //----------------------------------------------------------------
    #region ATTACK_STATE
    private void OnAttackEnter()
    {
        //Stop moving the Enemy towards the Player
        agent.ResetPath();
    }

    private void Attack()
    {
        Debug.Log("Attack");
        attackTimer -= Time.deltaTime;
        if (!withinAttackRange)
        {
            brain.PopState();
        }
        else if (attackTimer <= 0)
        {
            //Activate the Attack animation each 2 seconds
            animator.SetBool("Chase", true);
            attackTimer = 2f;
            transform.LookAt(playerT);

            if (!alreadyAttacked)
            {
                //bulletSound.PlayOneShot(bulletSound.clip);

                projectile = (GameObject)Instantiate(projectileGraphic, transform.position, Quaternion.identity);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
                Destroy(projectile, 3f);
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnAttackExit()
    {

    }
    #endregion


    //----------------------------------------------------------------
    #region WANDER_STATE
    private void OnWanderEnter()
    {
        animator.SetBool("Chase", true);
        //Search a random direction in a 4m radius sphere from my position
        Vector3 wanderDirection = (Random.insideUnitSphere * 4) + transform.position;

        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(wanderDirection, out navMeshHit, 3f, NavMesh.AllAreas);
        Vector3 destination = navMeshHit.position;

        agent.SetDestination(destination);
    }

    private void Wander()
    {
        //when we arrive to the destination
        if (agent.remainingDistance <= .25f)
        {
            agent.ResetPath();//Stop the walking
            brain.PopState();//eliminate this state
        }
        if (playerIsNear)
        {
            brain.PushState(Chase, OnChaseEnter, OnChaseExit);
        }

    }

    private void OnWanderExit()
    {
        animator.SetBool("Chase", false);
    }
    #endregion



}
