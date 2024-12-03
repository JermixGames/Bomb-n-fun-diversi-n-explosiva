using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerPowerUpInventory : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text powerUpText;

    private BasePowerUp currentPowerUp;

    public void OnPowerUpAction(InputAction.CallbackContext context)
    {
        if (currentPowerUp != null)
        {
            currentPowerUp.OnPowerUp(context);
        }
    }

    public bool HasPowerUp()
    {
        return currentPowerUp != null;
    }

    public void CollectPowerUp(PowerUpPickup.PowerUpType type)
    {
        if (HasPowerUp()) return;

        // Añadir el power-up correspondiente
        switch (type)
        {
            case PowerUpPickup.PowerUpType.SpeedBoost:
                currentPowerUp = gameObject.AddComponent<SpeedBoostPowerUp>();
                break;
            case PowerUpPickup.PowerUpType.TimeSlow:
                currentPowerUp = gameObject.AddComponent<TimeSlowPowerUp>();
                break;
            case PowerUpPickup.PowerUpType.Dash:
                currentPowerUp = gameObject.AddComponent<DashPowerUp>();
                break;
            case PowerUpPickup.PowerUpType.MetalForm:
                currentPowerUp = gameObject.AddComponent<MetalFormPowerUp>();
                break;
        }

        UpdateUI();
    }

    public void PowerUpFinished()
    {
        currentPowerUp = null;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (powerUpText != null)
        {
            powerUpText.text = HasPowerUp() ?
                $"Power-up: {currentPowerUp.GetType().Name.Replace("PowerUp", "")}" :
                "No Power-up";
        }
    }
}
