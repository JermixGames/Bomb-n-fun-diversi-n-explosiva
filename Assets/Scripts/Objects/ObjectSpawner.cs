using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Points")]
    public List<Transform> spawnPoints;

    [Header("Object Prefabs")]
    public List<GameObject> objectPrefabs;

    [Header("Spawn Settings")]
    public float spawnInterval = 10f;
    public int objectsPerSpawn = 5;

    // Ajustes adicionales para el spawn
    [Header("Physics & Spawn Tweaks")]
    [Tooltip("Distancia aleatoria m�xima alrededor del punto de spawn para evitar superposici�n")]
    public float spawnRadius = 0.5f;
    [Tooltip("Capa en la que se colocar�n los objetos spawneados (opcional)")]
    public LayerMask spawnLayer;
    [Tooltip("Aplicar Constraints u otras propiedades al Rigidbody tras spawn")]
    public bool applyPhysicsConstraints = true;

    private void Start()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        if (objectPrefabs == null || objectPrefabs.Count == 0)
        {
            Debug.LogError("No object prefabs assigned!");
            return;
        }

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnObjects();
        }
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < objectsPerSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject prefab = objectPrefabs[Random.Range(0, objectPrefabs.Count)];

            // Aplicar un offset aleatorio a la posici�n para evitar que todos nazcan en el mismo punto exacto
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                Random.Range(0f, spawnRadius),
                Random.Range(-spawnRadius, spawnRadius)
            );

            Vector3 spawnPosition = spawnPoint.position + randomOffset;

            GameObject spawnedObject = Instantiate(prefab, spawnPosition, spawnPoint.rotation);

            // Opcional: Asignar la capa a la que pertenecen estos objetos para controlar colisiones
            spawnedObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(spawnLayer.value));

            // Si el objeto tiene un Rigidbody, ajustamos algunas propiedades para evitar comportamientos extra�os
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb != null && applyPhysicsConstraints)
            {
                // Por ejemplo, bloqueamos la rotaci�n en Z y X para que no se desequilibre
                rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

                // Ajustar drag y angularDrag para reducir la tendencia a volverse incontrolable
                rb.linearDamping = 0.5f;
                rb.angularDamping = 0.5f;

                // Opcional: Reducir masa si es muy alta
                rb.mass = 1f;
            }
        }
    }
}
