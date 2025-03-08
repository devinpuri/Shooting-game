//using UnityEngine;
//using UnityEngine.AI;

//public class Enemy : MonoBehaviour
//{
//    public NavMeshAgent agent;
//    public Transform player;

//    public float health = 100f;
//    public float sightRange = 15f, attackRange = 2f; // Updated attack range
//    public float damage = 10f; // Damage dealt to the player
//    public float attackCooldown = 1.5f; // Time between attacks
//    private bool isDead = false;
//    private float nextAttackTime = 0f; // Cooldown for attacks

//    private void Awake()
//    {
//        player = GameObject.Find("Player").transform;
//        agent = GetComponent<NavMeshAgent>();
//    }

//    private void Update()
//    {
//        if (isDead) return;

//        if (player != null && Vector3.Distance(transform.position, player.position) <= sightRange)
//        {
//            agent.SetDestination(player.position);
//        }
//    }


//    public void TakeDamage(float damageAmount)
//    {
//        health -= damageAmount;

//        if (health <= 0 && !isDead)
//        {
//            Die();
//        }
//    }

//    private void Die()
//    {
//        isDead = true;
//        Destroy(gameObject, 1f);
//        GameObject.Find("Player").GetComponent<Player>().score += 1;
//    }


//}
