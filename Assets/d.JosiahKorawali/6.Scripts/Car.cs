using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 20f;
    public float laneSpeed = 15f;

    [Header("Effects & Audio")]
    public ParticleSystem afterBurner; // Assign in Inspector
    public AudioSource driveSound;

    void Update()
    {
        // 1. Horizontal Movement (Left/Right)
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontal * laneSpeed * Time.deltaTime);

        // 2. Vertical Movement (Forward/Reverse)
        float vertical = Input.GetAxis("Vertical");

        // We add a '-' before 'vertical' to flip the direction 
        // if your car model was moving backwards.
        transform.Translate(Vector3.forward * -vertical * forwardSpeed * Time.deltaTime);

        // 3. Handle After-burners & Sound
        HandleEffects(vertical);
    }

    void HandleEffects(float verticalInput)
    {
        // Safety check: Exit if references are missing to avoid errors
        if (afterBurner == null || driveSound == null) return;

        // Since we flipped movement, check if verticalInput is negative 
        // to see if we are pressing 'forward' (W/Up)
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