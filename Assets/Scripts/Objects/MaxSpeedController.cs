using UnityEngine;

public class MaxSpeedController : MonoBehaviour
{
    public float maxSpeed = 10f; // Velocidad m�xima permitida
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            // Si la magnitud de la velocidad supera maxSpeed, la limitamos
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }
}
