using UnityEngine;

public class EnvironmentLoop : MonoBehaviour
{
    [Header("References")]
    public Transform carTransform;

    [Header("Loop Settings")]
    // The Z-distance behind the car where the block teleports
    public float thresholdZ = 20f;
    // This MUST be exactly (Number of Blocks * Length of one Block)
    public float totalLoopLength = 100f;

    void Update()
    {
        if (carTransform == null) return;

        // If the block is more than 'thresholdZ' units behind the car
        if (transform.position.z < carTransform.position.z - thresholdZ)
        {
            // SNAP logic: Move the block forward by the total length of the loop
            Vector3 newPos = transform.position;
            newPos.z += totalLoopLength;
            transform.position = newPos;
        }
    }
}