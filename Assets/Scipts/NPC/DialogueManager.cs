using UnityEngine;
using TMPro;  

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;  // Reference to the dialogue box UI
    public TextMeshProUGUI dialogueText;  // Reference to the TextMeshPro component

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the dialogue box is hidden at the start
        dialogueBox.SetActive(false);

        // Get the PlayerController instance
        playerController = FindObjectOfType<PlayerController>();
    }

    // Method to start the dialogue
    public void StartDialogue(string dialogue)
    {
        dialogueBox.SetActive(true);  // Show the dialogue box
        dialogueText.text = dialogue;  // Set the dialogue text

        // Disable player movement during dialogue
        if (playerController != null)
        {
            playerController.StopMovement();
        }
    }

    // Optionally, close the dialogue with the space key or any other input
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CloseDialogue();
        }
    }

    // Method to close the dialogue
    public void CloseDialogue()
    {
        dialogueBox.SetActive(false);  // Hide the dialogue box

        // Enable player movement after dialogue ends
        if (playerController != null)
        {
            playerController.ResumeMovement();
        }
    }
}