using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapExplosion : MonoBehaviour
{
    public GameObject explosionVFX;       // Explosion effect to play on cube destruction

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player (tagged "MainCamera") collides with the trap
        if (other.CompareTag("MainCamera"))
        {
            Destroy(gameObject);
            // Find all objects with the tag "NewEnemy"
            // GameObject[] enemies = GameObject.FindGameObjectsWithTag("NewEnemy");
            
            // // Check if there are at least two enemies to destroy
            // if (enemies.Length >= 2)
            // {
            //     Instantiate(explosionVFX, enemies[1].transform.position, Quaternion.identity);
            //     Destroy(enemies[1]); // Destroy the second enemy (index 1)
            //     Debug.Log("Destroyed: " + enemies[1].name);
            // }
            // else
            // {
            //     Debug.Log("Not enough enemies to destroy.");
            // }

                        // Destroy all objects with the tag "NewEnemy"
            // GameObject[] enemiesToDestroy = GameObject.FindGameObjectsWithTag("NewEnemy");
            // foreach (GameObject enemy in enemiesToDestroy)
            // {
            //     Destroy(enemy);
            //     Debug.Log("Destroyed: " + enemy.name);
            // }
        }
    }
}
