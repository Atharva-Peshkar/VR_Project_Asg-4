using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class RayCastTeleport : MonoBehaviour
{
    [SerializeField]
    private GameObject playerOrigin;
    [SerializeField]
    private LayerMask teleportMask;
    [SerializeField]
    private InputActionReference teleportButtonPress;

    public GameObject explosionVFX; // Assign your explosion effect prefab here


    void Start()
    {
        teleportButtonPress.action.performed += DoRayCast;
    }

    void DoRayCast(InputAction.CallbackContext _)
    {
        RaycastHit hit;

        bool didHit = Physics.Raycast(
        transform.position,
        transform.forward,
        out hit,
        Mathf.Infinity,
        teleportMask);
        
    if (didHit) {
        // The object we hit will be in the collider property of our hit object.
        // We can get the name of that object by accessing it's Game Object then the name property
        Debug.Log(hit.collider.gameObject.name);
        
				// Don't forget to attach the player origin in the editor!
        playerOrigin.transform.position = hit.collider.gameObject.transform.position;

            // Start the coroutine to wait and destroy the enemy
            StartCoroutine(DestroyFirstEnemyAfterDelay(0.7f));
    }
        
    }

    private IEnumerator DestroyFirstEnemyAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);
        

        // Find all objects with the tag "NewEnemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("NewEnemy");
        
        // Check if there are at least two enemies to destroy
        if (enemies.Length >= 2)
        {
            Instantiate(explosionVFX, enemies[1].transform.position, Quaternion.identity);
            Destroy(enemies[1]); // Destroy the second enemy (index 1)
            Debug.Log("Destroyed: " + enemies[1].name);
        }
        else
        {
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
                Debug.Log("Destroyed: " + enemy.name);
            }
        }

    }
}
