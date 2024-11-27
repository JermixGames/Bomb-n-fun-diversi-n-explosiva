// ExplosiveObject.cs
using UnityEngine;

public class ExplosiveObject : MonoBehaviour
{
    public float timer = 5f;
    public float explosionForce = 500f;
    public float explosionRadius = 5f;

    private void Start()
    {
        Invoke("Explode", timer);
    }

    private void Explode()
    {
        // Explosión física
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        // Puedes agregar efectos visuales o de sonido aquí

        // Destruir el objeto después de la explosión
        Destroy(gameObject);
    }
}

