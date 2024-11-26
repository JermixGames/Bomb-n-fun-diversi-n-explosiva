// StickyObject.cs
using UnityEngine;

public class StickyObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Al colisionar, fija el objeto al otro objeto
        transform.parent = collision.transform;

        // Desactivar el Rigidbody para que no se mueva más
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}