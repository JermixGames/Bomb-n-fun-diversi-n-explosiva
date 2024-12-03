using UnityEngine;

public class SpeedBoostPowerUp : BasePowerUp
{
    [SerializeField] private float speedMultiplier = 2f;
    private float originalSpeed;

    protected override void OnPowerUpStart()
    {
        originalSpeed = playerController.moveSpeed;
        playerController.moveSpeed *= speedMultiplier;
    }

    protected override void OnPowerUpUpdate() { }

    protected override void OnPowerUpEnd()
    {
        playerController.moveSpeed = originalSpeed;
    }
}
