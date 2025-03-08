using UnityEngine;

public class enemyai : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;              // Movement speed.
    public float chaseRange = 5f;         // Distance at which enemy starts chasing the player.
    public float attackRange = 1f;        // Range at which the enemy stops and can attack.
    public float attackCooldown = 1.5f;   // Time between attacks.
    public float rotationOffset = 0f;     // Adjust this value if the enemy sprite is rotated (e.g., set to -90 or 90).

    [Header("Health Settings")]
    public int maxHealth = 3;             // Maximum health of the enemy.
    public int currentHealth;
    bool isdeath = false;
    private Transform player;
    public Transform attackPoint;
    private float lastAttackTime = 0f;
    public Animator animator;

    public LayerMask Playerlayer;

    void Start()
    {
        currentHealth = maxHealth;
        // Find the player by tag. Make sure your player GameObject is tagged "Player".
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Please tag your player as 'Player'.");
        }
    }

    void Update()
    {
        if (player == null)
            return;

        // Calculate the distance to the player.
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // If within chase range, either chase or stop to attack.
        if (distanceToPlayer <= chaseRange)
        {
            // Chase if the player is outside attack range.
            if (distanceToPlayer > attackRange)
            {
                ChasePlayer();
            }
            // Stop movement and attack if within attack range.
            else
            {
                StopMovement();
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    void ChasePlayer()
    {
        // Calculate normalized direction toward the player.
        Vector2 direction = (player.position - transform.position).normalized;
        // Move enemy toward the player.
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // Rotate enemy to face the player with an added rotation offset.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);
    }

    void StopMovement()
    {
        // If using a Rigidbody2D, you could set its velocity to zero here.
        // For example:
        // GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void AttackPlayer()
    {
        // Insert your attack logic here.
        animator.SetTrigger("attack");
        Debug.Log("Enemy attacks the player!");
        GameObject other;
        other = Physics2D.OverlapCircle(attackPoint.position, 1, Playerlayer, -Mathf.Infinity, Mathf.Infinity).gameObject;
        // e.g., trigger an animation, reduce player health, etc.
        if (other != null){
            Debug.Log("attack");
            other.gameObject.GetComponent<TopDownCharacterController>().health = other.gameObject.GetComponent<TopDownCharacterController>().health - 10;
        }
    }

    // Function called by the bullet to apply damage.
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        if (!isdeath)
        {
            isdeath = true;
            GameObject.Find("Game manager").GetComponent<Gamemanager>().Updatescore(1);
            Destroy(gameObject);
        }
    }
}
