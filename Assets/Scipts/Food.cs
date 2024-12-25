using UnityEngine;

public class Food : MonoBehaviour
{
    public delegate void FoodCollectedHandler(GameObject collectedFood);
    public event FoodCollectedHandler onFoodCollected;

    // This method will be called when the food object is collected
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onFoodCollected?.Invoke(gameObject);  // Notify the FoodSpawner that this food was collected
            Destroy(gameObject);  // Destroy the food object after collection
        }
    }
}
