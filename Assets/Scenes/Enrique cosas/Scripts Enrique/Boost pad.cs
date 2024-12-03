using UnityEngine;

public class Boostpad : MonoBehaviour
{
    public Vector3 ForceDirection = Vector3.up;
    public float ForcePower = 100;
    public ForceMode ForceType;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered by " + other.gameObject.name);
        Rigidbody RBReference = other.gameObject.GetComponent<Rigidbody>();
        RBReference.AddForce(ForceDirection * ForcePower, ForceType);
    }
}

