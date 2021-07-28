using UnityEngine;

public class Defender : MonoBehaviour
{
    [Header("Defender Stats")]
    public float health;
    [Space]
    public CardStats defenderStats;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject smokePrefab;

    [HideInInspector] public DefenderType defenderType;
    [HideInInspector] public GameObject activeTile;
    [HideInInspector] public int activeLine;

    // ANIMATOR STATES
    private bool isDead;
    private bool isAttacking;

    private GameObject target;
    private float timer;

    private void Start()
    {
        defenderType = defenderStats.defenderType;
        timer = defenderStats.attackDelay;
        health = defenderStats.maxHealth;

        transform.SetParent(GameObject.FindGameObjectWithTag("DefenderParent").transform);
    }

    private void Update()
    {
        Attack();

        KillCharacter();

        UpdateAnimatorValues();
    }

    private void Attack()
    {
        timer += Time.deltaTime;

        if (defenderType == DefenderType.Melee)
        {
            MeleeAttack();
        }

        else if (defenderType == DefenderType.Archer)
        {
            target = FindTarget();

            float attackDistance = 15f;
            if (target != null && (transform.position - target.transform.position).magnitude <= attackDistance)
            {
                if (timer >= defenderStats.attackDelay)
                {
                    transform.LookAt(target.transform.position);
                    timer = 0;

                    isAttacking = true;
                    GameObject newProjectile = Instantiate(projectilePrefab, transform.position + new Vector3(0, .6f, 0), transform.rotation);
                    newProjectile.GetComponent<Projectile>().damage = defenderStats.damage;
                    newProjectile.GetComponent<Projectile>().target = target;
                }
            }
        }

        else if (defenderType == DefenderType.Miner)
        {
            target = FindTarget();

            float attackDistance = 2f;
            if (target != null && (transform.position - target.transform.position).magnitude <= attackDistance)
            {
                transform.LookAt(target.transform.position);
                if (timer >= defenderStats.attackDelay)
                {
                    timer = 0;

                    isAttacking = true;
                    DealDamage(target);
                }
            }
            else if (activeTile.GetComponent<Tile>().activeOre != null)
            {
                target = activeTile.GetComponent<Tile>().activeOre;
                transform.LookAt(activeTile.GetComponent<Tile>().activeOre.transform.position);

                if (timer >= defenderStats.attackDelay)
                {
                    timer = 0;

                    isAttacking = true;
                    DealDamage(target);
                }
            }
        }
    }
    private void KillCharacter()
    {
        if (health <= 0)
        {
            if (defenderType == DefenderType.Obstacle)
            {
                var smoke = Instantiate(smokePrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                Destroy(smoke, .5f);
            }
            activeTile.GetComponent<Tile>().isOccupied = false;
            activeTile.GetComponent<Tile>().activeDefender = null;
            Destroy(this.gameObject, 1f);
        }
    }

    // UPDATE ANIMATIONS AND SOUNDS
    private void UpdateAnimatorValues()
    {
        Animator animator = GetComponent<Animator>();

        if (isAttacking)
        {
            isAttacking = false;
            animator.SetTrigger("isAttacking");

            if (defenderType == DefenderType.Archer)
                FindObjectOfType<AudioManager>().PlaySound("GunShot");
            else
                FindObjectOfType<AudioManager>().PlaySound("SwordHit2");
        }

        if (health <= 0 && !isDead)
        {
            isDead = true;
            animator.SetTrigger("isDead");

            FindObjectOfType<AudioManager>().PlaySound("Death2");
        }
    }

    private GameObject FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>().activeLine == activeLine)
            {
                float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
                if (distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }
        return closestEnemy;
    }

    private void MeleeAttack()
    {
        target = FindTarget();

        float attackDistance = 2f;
        if (target != null && (transform.position - target.transform.position).magnitude <= attackDistance)
        {
            transform.LookAt(target.transform.position);
            if (timer >= defenderStats.attackDelay)
            {
                timer = 0;
                isAttacking = true;

                DealDamage(target);
            }
        }
    }
    private void DealDamage(GameObject _target)
    {
        _target.SendMessage("TakeDamage", defenderStats.damage);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}