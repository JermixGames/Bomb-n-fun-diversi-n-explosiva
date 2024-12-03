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
            // Seleccionar un punto de spawn aleatorio
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // Seleccionar un prefab de objeto aleatorio
            GameObject objectToSpawn = objectPrefabs[Random.Range(0, objectPrefabs.Count)];

            // Instanciar el objeto en la posición y rotación del punto de spawn
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
