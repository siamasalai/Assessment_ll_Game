using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Targeting")]
    public Transform target; // Drag your Hilux here

    [Header("Settings")]
    public Vector3 offset = new Vector3(0, 5, -10); // Adjust for height and distance
    public float smoothSpeed = 0.125f; // Lower = smoother (0.1 to 0.5 is best)
    public float rotationSmoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Calculate the position the camera WANTS to be in
        // TransformDirection makes the offset relative to where the car is facing
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // 2. Smoothly move the camera to that position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 3. Smoothly look at the Hilux (slightly above the center)
        Vector3 lookAtPos = target.position + Vector3.up * 1.5f;
        Quaternion targetRotation = Quaternion.LookRotation(lookAtPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}