using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health;
    [SerializeField] private float damage;
    [Space]

    [SerializeField] private float attackDelay;
    [SerializeField] private float speed;

    private GameObject currentTile;
    private GameObject target;
    private float timer;

    [HideInInspector] public Transform finishPoint;
    [HideInInspector] public int activeLine;

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    // ANIMATOR STATES
    private bool isDead = false;
    private bool isAttacking = false;
    private bool isMoving = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.speed = speed;
        timer = attackDelay;

        transform.SetParent(GameObject.FindGameObjectWithTag("EnemyParent").transform);
    }

    private void Update()
    {
        KillActor();

        ActorMovement();
    }

    private void ActorMovement()
    {
        timer += Time.deltaTime;
        if (!isDead)
        {
            if (target != null)
            {
                navMeshAgent.destination = target.transform.position;

                float attackDistance = 1f;
                if ((transform.position - navMeshAgent.destination).magnitude <= attackDistance)
                {
                    navMeshAgent.destination = transform.position;
                    transform.LookAt(target.transform.position);

                    isMoving = false;

                    if (timer >= attackDelay)
                    {
                        timer = 0;
                        isAttacking = true;

                        DealDamage(target);
                    }
                }
            }
            else
            {
                isMoving = true;
                navMeshAgent.destination = finishPoint.position;
            }
        }
    }
    private void KillActor()
    {
        int reward = 10;
        if (health <= 0 && isDead)
        {
            timer = 0;
            isMoving = false;

            gameObject.tag = "Untagged";
            navMeshAgent.destination = transform.position;
            navMeshAgent.isStopped = true;

            GameStats.currentMoney += reward;
            Destroy(gameObject, 2f);

            GetComponent<Enemy>().enabled = false;
        }
        // FINISH POINT
        else if ((transform.position - finishPoint.position).magnitude <= .5f)
        {
            GameStats.currentHealth--;
            Destroy(gameObject);
        }
    }

    // UPDATE ANIMATIONS AND SOUNDS
    private void LateUpdate()
    {
        animator.SetBool("isMoving", isMoving);

        if (isAttacking)
        {
            isAttacking = false;

            animator.SetTrigger("isAttacking");
            FindObjectOfType<AudioManager>().PlaySound("SwordHit1");
        }

        if (health <= 0 && !isDead)
        {
            isDead = true;

            animator.Play("die hard");
            FindObjectOfType<AudioManager>().PlaySound("Death1");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Projectile>() || other.GetComponent<CardSpells>())
            return;

        currentTile = other.gameObject;
        if (currentTile.GetComponent<Tile>() && currentTile.GetComponent<Tile>().isOccupied)
            target = other.GetComponent<Tile>().activeDefender;
        else
            target = null;
    }

    private void DealDamage(GameObject _target)
    {
        _target.SendMessage("TakeDamage", damage);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}