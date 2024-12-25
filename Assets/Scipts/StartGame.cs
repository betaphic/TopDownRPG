using UnityEngine;
using UnityEngine.SceneManagement; // Import to manage scenes

public class StartGame : MonoBehaviour
{
    public void OnStartButtonClick()
    {
       
        SceneManager.LoadScene("Game1");
    }
}