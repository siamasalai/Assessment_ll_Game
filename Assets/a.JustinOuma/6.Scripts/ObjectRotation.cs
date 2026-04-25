using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    public void RotateLeft()
    {
        transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
    }

    public void RotateRight()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
