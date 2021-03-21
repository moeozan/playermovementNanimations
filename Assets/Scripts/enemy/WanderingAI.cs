using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private float patrolRadius;
    [SerializeField] private float attackDistance;
    [SerializeField] private float runDistance;
    [SerializeField] private float patrolTimer;
    [SerializeField] private Transform startTransform;
    [SerializeField] private float damage;

    private PlayerHealth playerHealth;
    private Vector3 newPatrolPosition;
    private GameObject player;
    private NavMeshAgent agent;
    private float timer;
    private Animator anim;
    EnemyHealth enemyHealth;
    private float distance;
    private NavMeshHit navHit;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        timer = 0;
        agent.avoidancePriority = Random.Range(0, 100);
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (playerHealth.CurrentHealth <= 0)
        {
            Patrolling();
        }
        if (enemyHealth.currentHealt <= 0)
        {
            HandlerOfSomething(Vector3.zero,true,"Death",true,0);        
        }
        else
        {
            EnemyHandler();
        }
    }

    private void EnemyHandler()
    {
        if (distance <= attackDistance)
        {
            Attack();
        }
        else if (distance <= runDistance && distance > attackDistance)
        {
            Run();
        }
        else
        {
            Patrolling();
        }
    }

    public void Patrolling()
    {
        timer += Time.deltaTime;
        if (!agent.hasPath || timer > patrolTimer || Vector3.Distance(transform.position, newPatrolPosition) < 1f)
        {
            FindPatrolPoint();
        }

    }
    private void FindPatrolPoint()
    {
        newPatrolPosition = RandomNavSphere(startTransform.position, patrolRadius, -1);
        float distanceForPatrol = Vector3.Distance(newPatrolPosition, transform.position);
        if (distanceForPatrol < patrolRadius / 2)
        {
            Vector3 add = new Vector3(newPatrolPosition.x + patrolRadius / 2, 0 , newPatrolPosition.z + patrolRadius / 2);
            newPatrolPosition += add;
        }
        HandlerOfSomething(newPatrolPosition, false, "Run", true, 3f);
        if (timer > patrolTimer)
        {
            timer = 0;
        }
    }

    private Vector3 RandomNavSphere(Vector3 start, float radius, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += start;
        NavMesh.SamplePosition(randomDirection, out navHit, radius, layermask);
        return navHit.position;
    }

    public void Run()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.ResetTrigger("Attack");
            HandlerOfSomething(player.transform.position, false, "Run", true, 6f);
            
        }
        
    }
    public void Attack()
    {
        playerHealth.TakeDamage(damage);
        transform.LookAt(player.transform.position);
        agent.isStopped = true;
        agent.speed = 0f;
        anim.SetBool("Run", false);
        anim.SetTrigger("Attack");
    }

    private void HandlerOfSomething(Vector3 pos, bool agentStopStatus, string animName, bool animStatus, float agentSpeed)
    {
        agent.SetDestination(pos);
        agent.isStopped = agentStopStatus;
        anim.SetBool(animName, animStatus);
        agent.speed = agentSpeed;
    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(startTransform.position, patrolRadius);
    }

#endif

}
