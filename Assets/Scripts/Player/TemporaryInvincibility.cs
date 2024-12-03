using System.Collections;
using UnityEngine;

public class TemporaryInvincibility : MonoBehaviour
{
    public float invincibilityDuration = 2f;
    public Material flashMaterial;
    private Material originalMaterial;
    private MeshRenderer meshRenderer;

    void Start()
    {
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;

        playerHealth.onPlayerDeath.AddListener(ActivateInvincibility);
    }

    public void ActivateInvincibility()
    {
        StartCoroutine(InvincibilityRoutine());
    }

    IEnumerator InvincibilityRoutine()
    {
        // Cambiar a material de invencibilidad
        meshRenderer.material = flashMaterial;

        // Esperar la duración
        yield return new WaitForSeconds(invincibilityDuration);

        // Volver al material original
        meshRenderer.material = originalMaterial;
    }
}
