using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;      // Damage value of the bullet.



    // Called when the bullet enters a trigger collider.
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has an Enemy component.
        enemyai enemy = collision.GetComponent<enemyai>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        // Destroy the bullet after collision.
    }
}
