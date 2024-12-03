using UnityEngine;

public class TimeSlowPowerUp : BasePowerUp
{
    [SerializeField] private float timeScale = 0.5f;
    private float originalFixedDeltaTime;

    protected override void OnPowerUpStart()
    {
        originalFixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = originalFixedDeltaTime * timeScale;
    }

    protected override void OnPowerUpUpdate() { }

    protected override void OnPowerUpEnd()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
}