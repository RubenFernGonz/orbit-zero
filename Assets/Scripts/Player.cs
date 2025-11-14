using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Player : MonoBehaviour
{
    [Header("Configuración del tanque")]
    public float moveSpeed = 8f;          // Velocidad de avance
    public float turnSpeed = 60f;         // Velocidad de giro (grados/segundo)

    [Header("Ajustes visuales (opcional)")]
    public Transform leftTrack;
    public Transform rightTrack;

    private Rigidbody rb;

    private float leftInput;
    private float rightInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ; 
        // Esto evita que el box incline el tanque
    }

    void Update()
    {
        leftInput = 0f;
        rightInput = 0f;

        if (Input.GetKey(KeyCode.W)) leftInput = 1f;
        else if (Input.GetKey(KeyCode.S)) leftInput = -1f;

        if (Input.GetKey(KeyCode.UpArrow)) rightInput = 1f;
        else if (Input.GetKey(KeyCode.DownArrow)) rightInput = -1f;

        AnimateTracks();
    }

    void FixedUpdate()
    {
        // Movimiento (ambas orugas)
        float forward = (leftInput + rightInput) * 0.5f;
        Vector3 move = transform.forward * forward * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + move);

        // Giro diferencial
        float turn = (leftInput - rightInput) * turnSpeed * Time.fixedDeltaTime;

        Quaternion rot = Quaternion.Euler(0, turn, 0);
        rb.MoveRotation(rb.rotation * rot);

        // Mantener el tanque totalmente paralelo al plano
        Vector3 e = rb.rotation.eulerAngles;
        rb.MoveRotation(Quaternion.Euler(0, e.y, 0));
    }

    void AnimateTracks()
    {
        if (leftTrack)
            leftTrack.localRotation = Quaternion.Euler(leftInput * -30f, 0, 0);

        if (rightTrack)
            rightTrack.localRotation = Quaternion.Euler(rightInput * -30f, 0, 0);
    }
}
