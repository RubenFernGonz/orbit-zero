using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Configuración del tanque")]
    public float motorForce = 1500f;     // Fuerza de avance
    public float turnTorque = 300f;      // Fuerza de torque para girar
    public float maxSpeed = 10f;         // Velocidad máxima lineal
    public float maxAngularVelocity = 1.5f; // Velocidad máxima de rotación (radianes/segundo)

    [Header("Ajustes visuales (opcional)")]
    public Transform leftTrack;
    public Transform rightTrack;

    private Rigidbody rb;
    private float leftInput;
    private float rightInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.down * 0.5f;
        rb.maxAngularVelocity = maxAngularVelocity;
    }

    void Update()
    {
        // Reset inputs
        leftInput = 0f;
        rightInput = 0f;

        // Controles orugas
        if (Input.GetKey(KeyCode.W)) leftInput = 1f;
        else if (Input.GetKey(KeyCode.S)) leftInput = -1f;

        if (Input.GetKey(KeyCode.UpArrow)) rightInput = 1f;
        else if (Input.GetKey(KeyCode.DownArrow)) rightInput = -1f;
    }

    void FixedUpdate()
    {
        // Fuerza de avance promedio
        float forwardInput = (leftInput + rightInput) * 0.5f;
        Vector3 forwardForce = transform.forward * forwardInput * motorForce * Time.fixedDeltaTime;

        if (rb.velocity.magnitude < maxSpeed)
            rb.AddForce(forwardForce, ForceMode.Force);

        // Torque de giro diferencial (más física)
        float turnInput = (leftInput - rightInput);
        Vector3 turnTorqueVector = Vector3.up * turnInput * turnTorque * Time.fixedDeltaTime;

        rb.AddTorque(turnTorqueVector, ForceMode.Force);

        // Animación opcional
        AnimateTracks();
    }

    void AnimateTracks()
    {
        if (leftTrack)
            leftTrack.localRotation = Quaternion.Euler(leftInput * -30f, 0, 0);
        if (rightTrack)
            rightTrack.localRotation = Quaternion.Euler(rightInput * -30f, 0, 0);
    }
}
