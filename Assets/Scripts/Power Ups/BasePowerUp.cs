using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BasePowerUp : MonoBehaviour
{
    [Header("Power-up Base Settings")]
    [SerializeField] protected float duration = 5f;

    protected bool isActive = false;
    protected PlayerController playerController;
    protected float timeRemaining;
    protected bool hasBeenUsed = false;

    protected virtual void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError($"Error: {this.GetType().Name} requiere un PlayerController!");
        }
    }

    public void OnPowerUp(InputAction.CallbackContext context)
    {
        if (context.started && !hasBeenUsed && !isActive)
        {
            ActivatePowerUp();
            hasBeenUsed = true;
        }
    }

    protected virtual void ActivatePowerUp()
    {
        isActive = true;
        timeRemaining = duration;
        StartCoroutine(PowerUpRoutine());
    }

    protected virtual System.Collections.IEnumerator PowerUpRoutine()
    {
        OnPowerUpStart();

        while (isActive && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            OnPowerUpUpdate();
            yield return null;
        }

        EndPowerUp();
    }

    protected virtual void EndPowerUp()
    {
        isActive = false;
        OnPowerUpEnd();

        // Notificar al inventario que el power-up ha terminado
        var inventory = GetComponent<PlayerPowerUpInventory>();
        if (inventory != null)
        {
            inventory.PowerUpFinished();
        }

        Destroy(this);
    }

    protected abstract void OnPowerUpStart();
    protected abstract void OnPowerUpUpdate();
    protected abstract void OnPowerUpEnd();
}
