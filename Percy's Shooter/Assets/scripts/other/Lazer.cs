using System.Collections;
using UnityEngine;

public class SpawnAndSpin : MonoBehaviour
{
    public GameObject prefab;          // The prefab to spawn
    public float spawnInterval = 10f;  // Time between spawns (in seconds)
    public float disappearTime = 5f;   // Time before the prefab disappears (in seconds)

    private GameObject spawnedObject;  // The currently spawned object
    private Transform playerTransform; // The player's transform

    void Start()
    {
        // Find the player transform (assuming the player has the "Player" tag)
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Start the spawning process
        StartCoroutine(SpawnPrefab());
    }

    // Coroutine to handle the spawning and behavior of the prefab
    IEnumerator SpawnPrefab()
    {
        while (true)
        {
            // Wait for the spawn interval
            yield return new WaitForSeconds(spawnInterval);

            // Spawn the prefab at the current object's position
            spawnedObject = Instantiate(prefab, transform.position, Quaternion.identity);

            // Make the prefab spin towards the player
            StartCoroutine(RotateTowardsPlayer(spawnedObject));

            // Wait for the disappear time
            yield return new WaitForSeconds(disappearTime);

            // Destroy the prefab after it disappears
            Destroy(spawnedObject);
        }
    }

    // Coroutine to rotate the prefab towards the player
    IEnumerator RotateTowardsPlayer(GameObject obj)
    {
        Vector3 targetDirection = playerTransform.position - obj.transform.position;

        // Rotate the object smoothly over time to face the player
        float rotationSpeed = 90f;  // Degrees per second (adjust as necessary)

        while (obj != null && targetDirection != Vector3.zero)
        {
            // Rotate the object towards the player
            Vector3 direction = Vector3.RotateTowards(obj.transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0f);
            obj.transform.rotation = Quaternion.LookRotation(direction);

            yield return null;
        }
    }
}