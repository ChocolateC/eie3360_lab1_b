using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimiFollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Transform[] enemies; // Array of references to enemy transforms
    public float followSpeed = 5f; // Speed at which Mimi follows the player
    public float followThreshold = 1f; // Distance threshold for following
    private Animator mimiA;
    private bool isFollowing = true;
    private bool isDisappeared = false;
    private bool isEnemyNearby = false;

    void Start()
    {
        mimiA = GetComponent<Animator>();
    }

    void Update()
{
    if (player != null && enemies != null)
    {
        // Calculate the direction from Mimi to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Calculate the distance between Mimi and the player
        float distanceToPlayer = directionToPlayer.magnitude;

        // Check if any enemy is within the radius
        bool isEnemyNearby = false;
        foreach (Transform enemyTransform in enemies)
        {
            // Get the position of the current enemy
            Vector3 enemyPosition = enemyTransform.position;

            float distanceToEnemy = Vector3.Distance(transform.position, enemyPosition);

            if (distanceToEnemy < 3f)
            {
                // Set Mimi to idle and disappear
                isFollowing = false;
                isDisappeared = true;
                isEnemyNearby = true;
                DisappearCharacter();

                Debug.Log("Mimi is within 3 units of an enemy.");
                break; // Exit the loop after one enemy is found within range
            }
        }

        // If Mimi is not disappeared, move towards the player
        if (!isDisappeared)
    {
        if (distanceToPlayer > followThreshold)
        {
            // Move Mimi towards the player
            transform.Translate(directionToPlayer.normalized * followSpeed * Time.deltaTime);
            mimiA.SetBool("Walk", true);
            transform.LookAt(player);
        }
        else
        {
            // Player is within threshold, stop Mimi's walking animation
            mimiA.SetBool("Walk", false); // Mimi is in an idle state
        }
    }
    else
    {
        // Mimi is disappeared, reset the idle animation
        mimiA.SetBool("Walk", false); // Mimi is in an idle state
    }


        // Debug log to indicate when Mimi is outside the enemy's range
        if (!isEnemyNearby)
        {
            Debug.Log("Mimi is outside the enemy's range.");
        }
    }
}


    private void DisappearCharacter()
    {
        foreach (var renderer in mimiA.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            renderer.enabled = false;
        }
        Invoke("ReappearCharacter", 5); // Reappear after 5 seconds
    }

    private void ReappearCharacter()
    {
        Debug.Log("Mimi reappeared!"); // Debug log to check if the method is called

        foreach (var renderer in mimiA.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            renderer.enabled = true;
        }
        mimiA.SetBool("Idle", false); // Reset the "Idle" parameter
         mimiA.SetBool("Walk", true);
        isDisappeared = false; // Reset the disappeared flag
    }
}
