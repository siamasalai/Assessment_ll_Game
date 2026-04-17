using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 20f;
    public float laneSpeed = 15f;
    public float turnSpeed = 100f; // Added for smooth camera rotation

    [Header("Effects & Audio")]
    public ParticleSystem afterBurner;
    public AudioSource driveSound;

    void Update()
    {
        // 1. Get Input
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down

        // 2. Forward/Backward Movement
        // We removed the '-' because the Car_Player is now oriented correctly.
        transform.Translate(Vector3.forward * vertical * forwardSpeed * Time.deltaTime);

        // 3. Rotation (Turning)
        // This is key for Cinemachine! Rotating the parent makes the camera swing.
        float rotation = horizontal * turnSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        // 4. Handle After-burners & Sound
        HandleEffects(vertical);
    }

    void HandleEffects(float verticalInput)
    {
        if (afterBurner == null || driveSound == null) return;

        // If verticalInput > 0 (Moving Forward), play effects
        if (verticalInput > 0)
        {
            if (!afterBurner.isPlaying) afterBurner.Play();
            driveSound.pitch = Mathf.Lerp(driveSound.pitch, 1.5f, Time.deltaTime * 2);
        }
        else
        {
            if (afterBurner.isPlaying) afterBurner.Stop();
            driveSound.pitch = Mathf.Lerp(driveSound.pitch, 1f, Time.deltaTime * 2);
        }
    }
}