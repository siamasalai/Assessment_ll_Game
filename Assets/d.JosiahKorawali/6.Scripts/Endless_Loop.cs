using UnityEngine;
public class EnvironmentLoop : MonoBehaviour
{
    public float loopOffset = 100f; // The length of your city block
    public Transform carTransform;

    void Update()
    {
        // If the car passes the city block, move the block forward
        if (carTransform.position.z > transform.position.z + loopOffset)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (loopOffset * 2));
        }
    }
}