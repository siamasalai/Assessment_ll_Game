using UnityEngine;

public partial class CarDisplayRotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Degrees per second the car will rotate.")]
    public float rotationSpeed = 30.0f;

    [Tooltip("The axis to rotate around. Usually Y for a turntable effect.")]
    public Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        // Rotates the object by rotationSpeed degrees per second
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}