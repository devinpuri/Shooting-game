//using UnityEngine;
//using TMPro;
//public class Player : Character
//{
//    public TextMeshProUGUI textBox;
//    public float MaxHealth = 100;
//    public float health = 100;
//    public int score = 0;
//    public Rigidbody rb;
//    public float jumpForce = 7f;
//    public float rotationSpeed = 10f;
//    public Camera playerCam;
//    public Gun equippedGun; // Will be assigned when player picks up gun
//    public Texture2D cursorTexture;

//    public GameObject damageEffect; // Optional: Add a visual effect for taking damage

//    public HealthBar healthbar;
//    private Vector3 moveDirection;
//    protected override void Start()
//    {
//        base.Start();
//        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
//        Cursor.visible = true;
//        health = MaxHealth;
//        healthbar.SetMaxHealth(MaxHealth);
//    }

//    void Update()
//    {
//        textBox.text = "Score: " + score;
//        float xInput = Input.GetAxis("Horizontal");
//        float zInput = Input.GetAxis("Vertical");

//        moveDirection = transform.TransformDirection(new Vector3(xInput, 0, zInput).normalized);

//        RotateCharacter();

//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            Jump();
//        }

//        if (Input.GetKeyDown(KeyCode.R) && equippedGun != null)
//        {
//            equippedGun.Reload();
//        }
//    }
//    void FixedUpdate()
//    {
//        MoveCharacter();
//    }

//    void MoveCharacter()
//    {
//        // Apply movement using Rigidbody velocity (better collision handling)
//        Vector3 velocity = moveDirection * moveSpeed;
//        velocity.y = rb.velocity.y;  // Keep existing Y velocity for gravity

//        rb.velocity = velocity;
//    }

//    void HorizontalMovement(float xInput, float zInput)
//    {
//        Vector3 move = new Vector3(xInput, 0, zInput) * moveSpeed * Time.deltaTime;
//        move = transform.TransformDirection(move);
//        rb.MovePosition(rb.position + move);
//    }

//    void RotateCharacter()
//    {
//        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
//        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime));
//    }

//    void Jump()
//    {
//        if (IsGrounded())
//        {
//            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//        }
//    }

//    bool IsGrounded()
//    {
//        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
//    }

//    public void TakeDamage(float damage)
//    {
//        health -= damage;
//        healthbar.Sethealth(health);
//        if (health <= 0)
//        {
//            Die();
//        }
//    }

//    private void Die()
//    {
//        Debug.Log("Player has died.");
//        Destroy(this.gameObject);
//        // Add death logic (e.g., respawn, game over screen)
//    }
//    public void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.tag == "kill")
//        {
//            Die();
//        }
//    }
//}
