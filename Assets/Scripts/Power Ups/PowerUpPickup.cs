using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    public enum PowerUpType
    {
        SpeedBoost,
        TimeSlow,
        Dash,
        MetalForm
    }

    [Header("Settings")]
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float hoverHeight = 0.5f;
    [SerializeField] private float hoverSpeed = 1f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        SetupVisuals();
    }

    private void SetupVisuals()
    {
        var renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            Material material = new Material(renderer.material);
            material.color = GetPowerUpColor();
            renderer.material = material;
        }
    }

    private Color GetPowerUpColor()
    {
        switch (powerUpType)
        {
            case PowerUpType.SpeedBoost: return Color.yellow;
            case PowerUpType.TimeSlow: return Color.blue;
            case PowerUpType.Dash: return Color.green;
            case PowerUpType.MetalForm: return Color.grey;
            default: return Color.white;
        }
    }

    private void Update()
    {
        // Rotación
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Flotación
        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var inventory = other.GetComponent<PlayerPowerUpInventory>();
            if (inventory != null && !inventory.HasPowerUp())
            {
                inventory.CollectPowerUp(powerUpType);
                Destroy(gameObject);
            }
        }
    }
}
