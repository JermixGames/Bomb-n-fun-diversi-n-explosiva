using UnityEngine;

public class PickupableObject : MonoBehaviour
{
    // Enumeración para los tipos de objetos
    public enum ObjectWeight
    {
        Light,
        Medium,
        Heavy
    }

    // Variables configurables
    public ObjectWeight weight = ObjectWeight.Light;
    public float throwForce = 10f;
    private Rigidbody rb;
    private bool isPickedUp = false;
    private Transform holder;

    // Configuración de fuerzas según peso
    private readonly float[] impactForces = { 2f, 4f, 6f };  // Fuerzas para Light, Medium, Heavy
    private readonly float[] objectMasses = { 1f, 3f, 5f };  // Masas para Light, Medium, Heavy

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Configura la masa según el tipo de objeto
        rb.mass = objectMasses[(int)weight];
    }

    public void PickUp(Transform newHolder)
    {
        isPickedUp = true;
        holder = newHolder;
        rb.isKinematic = true;
        transform.SetParent(holder);
        transform.localPosition = Vector3.zero;
    }

    public void Throw(Vector3 direction)
    {
        isPickedUp = false;
        holder = null;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si golpea a otro jugador
        if (collision.gameObject.CompareTag("Player") && !isPickedUp)
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            
            if (playerRb != null)
            {
                // Aplica fuerza según el peso del objeto
                playerRb.AddForce(playerRb.transform.forward * impactForces[(int)weight], ForceMode.Impulse);
            }
        }
    }
}
