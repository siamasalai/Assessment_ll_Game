using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheels - Physics")]
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    [Header("Visual Effects")]
    public TrailRenderer[] tireMarks;
    public ParticleSystem[] smokeParticles;

    [Header("Settings")]
    public float motorForce = 1500f;
    public float brakeForce = 6000f;
    public float maxSpeed = 60f;
    public float autoBrakeForce = 5000f;

    [Header("Snappy Turning")]
    public float turnSpeed = 70f;

    [Header("Drift & Boost Settings")]
    [Range(0, 1)]
    public float driftStiffness = 0.5f;
    public float driftTurnBoost = 15f;
    private float normalStiffness = 1.0f;

    [Header("Audio")]
    public AudioSource engineSource;
    public AudioSource screechSource;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1.2f, 0);

        if (engineSource != null)
        {
            engineSource.loop = true;
            if (!engineSource.isPlaying) engineSource.Play();
        }

        ToggleEffects(false);
    }

    void FixedUpdate()
    {
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");
        float speed = rb.velocity.magnitude * 3.6f;

        // 1. FORWARD MOVEMENT (Using -vInput for MY specific orientation)
        if (speed < maxSpeed)
        {
            rearLeft.motorTorque = -vInput * motorForce;
            rearRight.motorTorque = -vInput * motorForce;
        }

        // 2. SNAPPY TURNING
        float rotation = hInput * turnSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0);

        // 3. BRAKING & DRIFT LOGIC
        bool isBraking = Input.GetKey(KeyCode.Space);
        ApplyDriftPhysics(isBraking);


        // Changed to -transform.forward to match  motor orientation
        if (isBraking && Mathf.Abs(hInput) > 0.1f)
        {
            rb.AddForce(-transform.forward * driftTurnBoost, ForceMode.Acceleration);
        }

        if (isBraking)
        {
            ApplyBrakes(brakeForce);
        }
        else if (vInput == 0)
        {
            ApplyBrakes(autoBrakeForce);
        }
        else
        {
            ApplyBrakes(0);
        }

        // 4. TRIGGER VISUALS & SOUND
        bool effectsActive = (isBraking && speed > 5f) || (Mathf.Abs(hInput) > 0.8f && speed > 20f);
        ToggleEffects(effectsActive);

        if (effectsActive)
        {
            if (screechSource != null && !screechSource.isPlaying) screechSource.Play();
        }
        else
        {
            if (screechSource != null && screechSource.isPlaying) screechSource.Stop();
        }

        HandleEngine(speed);
    }

    void ApplyDriftPhysics(bool isDrifting)
    {
        WheelFrictionCurve friction = rearLeft.sidewaysFriction;
        friction.stiffness = isDrifting ? driftStiffness : normalStiffness;
        rearLeft.sidewaysFriction = friction;
        rearRight.sidewaysFriction = friction;
    }

    void ToggleEffects(bool toggle)
    {
        if (tireMarks != null)
            foreach (TrailRenderer TR in tireMarks) if (TR != null) TR.emitting = toggle;

        if (smokeParticles != null)
        {
            foreach (ParticleSystem PS in smokeParticles)
            {
                if (PS != null)
                {
                    var em = PS.emission;
                    em.enabled = toggle;
                }
            }
        }
    }

    void ApplyBrakes(float force)
    {
        frontLeft.brakeTorque = force; frontRight.brakeTorque = force;
        rearLeft.brakeTorque = force; rearRight.brakeTorque = force;
    }

    void HandleEngine(float speed)
    {
        if (engineSource != null)
        {
            engineSource.pitch = Mathf.Lerp(1f, 2.5f, speed / maxSpeed);
            engineSource.volume = Mathf.Lerp(0.4f, 1.0f, speed / maxSpeed);
        }
    }
}