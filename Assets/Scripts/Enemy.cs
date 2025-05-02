using UnityEngine;

public class Enemy : MonoBehaviour
{
    float health, maxHealth = 3f;
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public bool showDetectionGizmo = true;

    public HealthBar healthBar;

    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    bool playerDetected = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (target)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if (!playerDetected && distanceToPlayer <= detectionRange)
            {
                playerDetected = true;
            }

            if (playerDetected)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                moveDirection = direction;
                //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //rb.rotation = angle;
            }
        }
    }

    private void FixedUpdate()
    {
        if (target && playerDetected)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (showDetectionGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
    }
}