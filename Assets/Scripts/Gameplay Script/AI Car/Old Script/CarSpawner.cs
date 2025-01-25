using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab; // Reference to the car prefab
    public Vector3 spawnPosition = new Vector3(0, 0.84f, 0); // Position to spawn the car
    public float spawnDelay = 2f; // Delay before spawning the car

    void Start()
    {
        StartCoroutine(SpawnCarWithDelay());
    }

    IEnumerator SpawnCarWithDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(spawnDelay);

        // Instantiate the car prefab at the specified position with no rotation
        Instantiate(carPrefab, spawnPosition, Quaternion.identity);
    }
}
