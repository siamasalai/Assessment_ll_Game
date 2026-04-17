using UnityEngine;

public class EnvironmentLoop : MonoBehaviour
{
    [Header("References")]
    public Transform carTransform; // Drag your 'Car' object here in the Inspector

    [Header("Loop Settings")]
    public float loopDistance = 50f; // The length of one city section
    public float respawnOffset = 100f; // How far ahead the section jumps

    void Update()
    {
        // 1. Safety Check: If the car isn't assigned, stop the script to prevent errors
        if (carTransform == null)
        {
            return;
        }

        // 2. Checking Position:
        // We check if the car has passed the center of this city piece
        if (carTransform.position.z > transform.position.z + loopDistance)
        {
            // 3. The Move:
            // transform.position.x: Keeps it on its current side (Left or Right)
            // transform.position.y: Keeps it at the current height
            // transform.position.z + respawnOffset: Teleports it far ahead of the car
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + respawnOffset);
        }
    }
}