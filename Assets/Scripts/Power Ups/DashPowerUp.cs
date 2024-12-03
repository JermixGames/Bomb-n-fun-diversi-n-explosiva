using UnityEngine;

public class DashPowerUp : BasePowerUp
{
    [SerializeField] private float dashForce = 20f;
    private Rigidbody rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        duration = 0.2f;  // Dash corto
    }

    protected override void OnPowerUpStart()
    {
        Vector3 dashDirection = playerController.lastMoveDirection;
        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
    }

    protected override void OnPowerUpUpdate() { }
    
    protected override void OnPowerUpEnd() { }
}