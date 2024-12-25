using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcDialogue = "You're nowhere but everywhere at once. You're not a snake, but a person controlling a snake in a pretty rushed Unity game. Here, take this key and go somewhere else.";  // NPC Dialogue
    public GameObject key;  // Reference to the key GameObject
    public DialogueManager dialogueManager;  // Reference to the DialogueManager to control the dialogue

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is near the NPC (using the "Player" tag)
        if (other.CompareTag("Player"))
        {
            // Start the dialogue
            dialogueManager.StartDialogue(npcDialogue);

            // Give the key to the player and make it visible
            PlayerController.instance.hasKey = true;
            key.SetActive(true);

            // Optionally, disable the NPC after interaction
            gameObject.SetActive(false);
        }
    }
}
