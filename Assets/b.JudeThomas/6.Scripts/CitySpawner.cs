using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    [Header("Setup")]
    public Transform carTransform;
    public GameObject[] cityBlocks;

    [Header("Precise Dimensions")]
    public float cityLength = 320f;

   
    private float fixedX = 88f;
    private float fixedY = 0f;
    private float startZ = -100f;

    void Start()
    {
        transform.position = Vector3.zero;

        if (carTransform == null)
            carTransform = GameObject.FindWithTag("Player").transform;

        // 1. FREEZE THE CAR (Stops it from falling into the void)
        Rigidbody rb = carTransform.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        // 2. POSITION THE ROAD
        if (cityBlocks != null && cityBlocks.Length >= 2)
        {
            cityBlocks[0].transform.position = new Vector3(fixedX, fixedY, startZ);
            cityBlocks[1].transform.position = new Vector3(fixedX, fixedY, startZ + cityLength);
        }

        // 3. UNFREEZE THE CAR (Wait a tiny bit then let physics work)
        Invoke("EnablePhysics", 0.1f);
    }

    void EnablePhysics()
    {
        Rigidbody rb = carTransform.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;
    }

    void Update()
    {
        if (carTransform == null || cityBlocks == null) return;

        foreach (GameObject city in cityBlocks)
        {
            if (city == null) continue;

            float offsetZ = carTransform.position.z - city.transform.position.z;

            if (offsetZ > cityLength)
            {
                city.transform.position += new Vector3(0, 0, cityLength * 2);
            }
            else if (offsetZ < -cityLength)
            {
                city.transform.position -= new Vector3(0, 0, cityLength * 2);
            }
        }
    }
}