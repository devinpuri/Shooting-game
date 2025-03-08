using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;         // Movement speed of the character.

    private Rigidbody2D rb;              // Reference to the Rigidbody2D component.
    private Vector2 movement;            // Stores input movement direction.

    public float health;
    public float maxhealth;

    public HealthBar healthbar;
    void Start()
    {
        health = maxhealth;
        healthbar.SetHealth(health); 
        healthbar.SetMaxHealth(maxhealth);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        healthbar.SetHealth(health);
        // Get input from the player.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize to ensure consistent movement speed in all directions.
        movement = movement.normalized;

        // Rotate the player to face the mouse position.
        RotateTowardsMouse();
        if (health<= 0)
        {
            GameObject.Find("Game manager").GetComponent<Gamemanager>().gameover();
        }
    }

    void FixedUpdate()
    {
        // Move the character based on input.
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); 
    }

    // Rotates the player so that its forward direction aligns with the mouse pointer.
    void RotateTowardsMouse()
    {
        // Get the mouse position in world space.
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Ensure the z-coordinate is the same as the player's.
        mousePos.z = transform.position.z;

        // Calculate the direction vector from the player to the mouse.
        Vector3 direction = mousePos - transform.position;

        // Calculate the angle in degrees from the positive x-axis.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust the angle by subtracting 90 degrees if the player's sprite is oriented upward.
        angle -= 0f; // Adjust this value if your sprite's forward direction is different.

        // Rotate the player to face the mouse position.
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
