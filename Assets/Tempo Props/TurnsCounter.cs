using UnityEngine;
using UnityEngine.UI;

public class TurnsCounter : MonoBehaviour
{
    public GameObject[] targetObjects; // Array of target objects to detect collisions with
    public GameObject messageCanvas;   // Reference to the canvas to show when 12 objects are hit
    private int collisionCount = 0;    // Counter for collisions

    private void Start()
    {
        if (messageCanvas != null)
        {
            messageCanvas.SetActive(false); // Hide the canvas at the start
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Loop through each target object in the array
        for (int i = 0; i < targetObjects.Length; i++)
        {
            // Check if the collision is with one of the target objects
            if (collision.gameObject == targetObjects[i])
            {
                collisionCount++; // Increase the collision counter
                Debug.Log("Collision Count: " + collisionCount);

                // Destroy the object that was collided with
                Destroy(targetObjects[i]);

                // Remove the destroyed object from the array
                targetObjects[i] = null;

                // Check if the player has hit 12 objects
                if (collisionCount == 2 && messageCanvas != null)
                {
                    messageCanvas.SetActive(true); // Show the canvas
                }

                break; // Exit the loop since we found the object that collided
            }
        }
    }
}
