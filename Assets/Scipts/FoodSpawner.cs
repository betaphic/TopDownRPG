using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;    // Reference to the red pixel prefab
    public float spawnRangeX = 8f;   // X range for spawning food
    public float spawnRangeY = 4.5f; // Y range for spawning food
    public int maxFoodCount = 5;     // Maximum number of food objects to spawn

    private List<GameObject> spawnedFood = new List<GameObject>();
    private int foodCollected = 0;   // Number of food collected

    void Start()
    {
        SpawnFood();
    }

    // Spawns the specified number of food objects (up to maxFoodCount)
    void SpawnFood()
    {
        // Spawn food objects until the maximum is reached
        while (spawnedFood.Count < maxFoodCount)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(-spawnRangeX, spawnRangeX),
                Random.Range(-spawnRangeY, spawnRangeY)
            );

            GameObject food = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
            food.GetComponent<SpriteRenderer>().color = Color.red; // Make sure food is red

            // Attach the food's "OnCollected" method to the food object
            Food foodScript = food.AddComponent<Food>();
            foodScript.onFoodCollected += OnFoodCollected;  // Ensure no duplicate methods

            spawnedFood.Add(food);
        }
    }

    // Call this function when food is collected
    void OnFoodCollected(GameObject collectedFood)
    {
        foodCollected++;
        Destroy(collectedFood);

        // If all food has been collected, trigger the transition to the next area
        if (foodCollected >= maxFoodCount)
        {
            TransitionToNextArea();
        }
    }

    // Handle transition to the next area
    void TransitionToNextArea()
    {
        // Add your logic for transitioning to the next area here
        Debug.Log("All food collected! Moving to the next area...");
        // For example, you can load a new scene:
        // SceneManager.LoadScene("NextAreaScene");
    }
}
