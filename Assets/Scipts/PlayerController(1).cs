using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; // Singleton instance

    public float speed;
    private SpriteRenderer sr;
    public bool hasKey = false; // Track if the player has the key
    public int foodCount = 0; // Track collected food
    public int totalFoods = 5; // Total foods needed to unlock the border

    // sprite variables
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite frontSprite;

    // audio variables
    public AudioSource soundEffects;
    public AudioClip[] sounds; // Add sound clips here

    public Rigidbody2D rb; // Reference to the Rigidbody2D
    public GameObject worldBorder; // Reference to the border GameObject

    private bool canTeleport = false; // Flag to track if player can teleport
    private bool canMove = true; // Flag to track if player can move

    // Start is called before the first frame update
    void Start()
    {
        // Initialize singleton instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent destruction when loading new scenes
        }
        else
        {
            Destroy(gameObject); // Ensure there's only one instance
        }

        soundEffects = GetComponent<AudioSource>(); // Get the audio source
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // If movement is disabled, prevent movement
        if (!canMove) return;

        Vector2 movement = Vector2.zero;

        // Player movement controls
        if (Input.GetKey("w"))
        {
            movement.y += 1;
            sr.sprite = upSprite;
        }

        if (Input.GetKey("a"))
        {
            movement.x -= 1;
            sr.sprite = leftSprite;
        }

        if (Input.GetKey("s"))
        {
            movement.y -= 1;
            sr.sprite = frontSprite;
        }

        if (Input.GetKey("d"))
        {
            movement.x += 1;
            sr.sprite = rightSprite;
        }

        // Ensure player can move after collecting food
        rb.linearVelocity = movement.normalized * speed;

        // Check if all food is collected
        if (foodCount >= totalFoods)
        {
            UnlockBorder(); // Enable teleport when all food is collected
        }
    }

    // Method to stop the player's movement
    public void StopMovement()
    {
        canMove = false;  // Disable player movement
        rb.linearVelocity = Vector2.zero;  // Stop the player's velocity
    }

    // Method to resume the player's movement
    public void ResumeMovement()
    {
        canMove = true;  // Enable player movement
    }

    private void UnlockBorder()
    {
        canTeleport = true; // Enable teleport when all food is collected
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Logic for obtaining the key
        if (collision.gameObject.CompareTag("Key"))
        {
            hasKey = true; // Player picks up the key
            soundEffects.PlayOneShot(sounds[0], 1f); // Play sound when key is collected
            Destroy(collision.gameObject); // Remove the key from the scene
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            foodCount++;
            soundEffects.PlayOneShot(sounds[0], 0.7f); // Play sound when food is collected
            Destroy(other.gameObject); // Remove the food object
        }

        // Check if the player interacts with the worldBorder when they have collected all food
        if (other.CompareTag("worldBorder") && canTeleport)
        {
            // Debug log to check teleportation trigger
            Debug.Log("Teleporting to Game2 scene");

            // Teleport to the "Game2" scene
            SceneManager.LoadScene("Game2"); 

            // Optionally, you could disable the world border to prevent multiple triggers
            // worldBorder.SetActive(false);
        }

        // If the player touches the door and has the key, load the "EndGame" scene
        if (other.CompareTag("Door") && hasKey)
        {
            SceneManager.LoadScene("EndGame"); // Load the "EndGame" scene
        }
    }
}
