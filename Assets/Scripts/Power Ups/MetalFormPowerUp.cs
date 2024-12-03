using UnityEngine;

public class MetalFormPowerUp : BasePowerUp
{
    private Rigidbody rb;
    private Vector3 originalScale;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
    }

    protected override void OnPowerUpStart()
    {
        rb.isKinematic = true;
        transform.localScale *= 1.2f;
    }

    protected override void OnPowerUpUpdate()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    protected override void OnPowerUpEnd()
    {
        rb.isKinematic = false;
        transform.localScale = originalScale;
    }
}