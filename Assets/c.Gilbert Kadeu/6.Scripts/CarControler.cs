using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 15f;
    public float laneDistance = 1.5f;
    public float laneChangeSpeed = 10f;

    [Header("Visual Effects")]
    public float tiltAmount = 10f;
    public float tiltSpeed = 5f;

    private bool isOnRightLane = true;

    void Update()
    {
        // 1. FORWARD MOVEMENT
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // 2. INPUT
        if (Input.GetKeyDown(KeyCode.LeftArrow)) isOnRightLane = false;
        if (Input.GetKeyDown(KeyCode.RightArrow)) isOnRightLane = true;

        // 3. HORIZONTAL POSITIONING
        float targetX = isOnRightLane ? laneDistance : -laneDistance;
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);

        // 4. APPLY TILT (LEAN)
        float moveDirection = targetX - transform.position.x;
        float targetZRotation = -moveDirection * tiltAmount;

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetZRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
    }
}