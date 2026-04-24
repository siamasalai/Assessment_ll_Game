using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    [Header("Main Settings")]
    public Transform carTransform;
    public GameObject[] cityBlocks;

    [Header("Road Geometry")]
    public float cityLength = 320f;

    // Fixed coordinates for the road alignment
    private float fixedX = 88f;
    private float fixedY = 0f;
    private float startZ = -100f;

    void Start()
    {
        // Set spawner to origin
        transform.position = Vector3.zero;

        // Auto-find player if not assigned
        if (carTransform == null)
        {
            carTransform = GameObject.FindWithTag("Player").transform;
        }

        // --- Prevent Falling ---
        // Freeze car physics while the city loads in
        Rigidbody rb = carTransform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // --- Initial Road Placement ---
        if (cityBlocks != null && cityBlocks.Length >= 2)
        {
            cityBlocks[0].transform.position = new Vector3(fixedX, fixedY, startZ);
            cityBlocks[1].transform.position = new Vector3(fixedX, fixedY, startZ + cityLength);
        }

        // Wait a split second for everything to settle then enable physics
        Invoke("EnablePhysics", 0.1f);
    }

    void EnablePhysics()
    {
        Rigidbody rb = carTransform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    void Update()
    {
        if (carTransform == null || cityBlocks == null) return;

        // Check each block to see if it needs to loop around
        foreach (GameObject city in cityBlocks)
        {
            if (city == null) continue;

            float offsetZ = carTransform.position.z - city.transform.position.z;

            // Move block forward if the car passed it
            if (offsetZ > cityLength)
            {
                city.transform.position += new Vector3(0, 0, cityLength * 2);
            }
            // Move block backward if car is reversing
            else if (offsetZ < -cityLength)
            {
                city.transform.position -= new Vector3(0, 0, cityLength * 2);
            }
        }
    }
}