using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;  // Cube prefab to instantiate
    [SerializeField] private Vector3 spawnPosition = new Vector3(0, 1, 0); // Fixed spawn position
    [SerializeField] private float respawnDelay = 3f; // Delay before respawning after destruction

    private GameObject currentCube; // Reference to the spawned cube

    void Start()
    {
        StartCoroutine(SpawnAndRespawnCube());
    }

    private IEnumerator SpawnAndRespawnCube()
    {
        while (true)
        {
            // Check if the cube exists; if not, instantiate it
            if (currentCube == null)
            {
                currentCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
            }

            // Wait for the cube to be destroyed, then respawn after a delay
            yield return new WaitUntil(() => currentCube == null);
            yield return new WaitForSeconds(respawnDelay);
        }
    }
}



