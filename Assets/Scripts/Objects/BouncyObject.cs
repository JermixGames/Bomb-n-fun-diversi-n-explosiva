// BouncyObject.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BouncyObject : MonoBehaviour
{
    private Rigidbody rb;

    public float bounceForce = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Asegurarse de que el material tiene el bounciness adecuado
        PhysicsMaterial2D mat = new PhysicsMaterial2D();
        mat.bounciness = 1f;
        mat.friction = 0f;
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.material = new PhysicsMaterial();
            collider.material.bounciness = 1f;
            collider.material.frictionCombine = PhysicsMaterialCombine.Minimum;
            collider.material.bounceCombine = PhysicsMaterialCombine.Maximum;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Aplicar una fuerza de rebote
        Vector3 bounceDirection = Vector3.Reflect(rb.linearVelocity, collision.contacts[0].normal);
        rb.linearVelocity = bounceDirection * bounceForce;
    }
}
