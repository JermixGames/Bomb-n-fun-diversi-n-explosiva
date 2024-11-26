using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickupController : MonoBehaviour
{
    public Transform holdPoint;  // Punto donde se sostendrán los objetos
    public float pickupRange = 2f;
    public float throwForceMultiplier = 2f;
    
    private PickupableObject currentObject;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void OnPickup(InputAction.CallbackContext context)
    {
        if (context.started)  // Cuando se presiona el botón
        {
            if (currentObject == null)
            {
                // Intenta recoger un objeto
                TryPickupObject();
            }
            else
            {
                // Lanza el objeto que tiene
                ThrowObject();
            }
        }
    }

    void TryPickupObject()
    {
        // Realiza un raycast para detectar objetos cercanos
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            0.5f,
            transform.forward,
            pickupRange
        );

        foreach (RaycastHit hit in hits)
        {
            PickupableObject pickupable = hit.collider.GetComponent<PickupableObject>();
            if (pickupable != null)
            {
                currentObject = pickupable;
                currentObject.PickUp(holdPoint);
                break;
            }
        }
    }

    void ThrowObject()
    {
        if (currentObject != null)
        {
            // Calcula la dirección del lanzamiento
            Vector3 throwDirection = transform.forward + Vector3.up * 0.5f;
            currentObject.Throw(throwDirection.normalized * throwForceMultiplier);
            currentObject = null;
        }
    }
}
