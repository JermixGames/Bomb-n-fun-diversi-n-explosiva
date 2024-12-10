using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public GameObject prefab;
    public int initialSize;
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance; // Patrón singleton para fácil acceso

    [Header("Pools")]
    public List<Pool> pools;

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        // Crear el diccionario de pools
        poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

        // Inicializar cada pool
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.initialSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.prefab, objectPool);
        }
    }

    /// <summary>
    /// Obtiene un objeto del pool correspondiente al prefab solicitado.
    /// Si no hay objetos disponibles, se creará uno nuevo.
    /// </summary>
    public GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogWarning("No existe un pool para el prefab: " + prefab.name);
            return null;
        }

        Queue<GameObject> objectPool = poolDictionary[prefab];

        GameObject objectToSpawn;

        if (objectPool.Count > 0)
        {
            objectToSpawn = objectPool.Dequeue();
        }
        else
        {
            // Si no hay objetos disponibles, instanciamos uno nuevo
            objectToSpawn = Instantiate(prefab);
        }

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    /// <summary>
    /// Devuelve un objeto al pool, poniéndolo inactivo.
    /// </summary>
    public void ReturnToPool(GameObject prefab, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogWarning("No existe un pool para el prefab al que quieres devolver el objeto.");
            Destroy(obj); // Destruir si no pertenece a ningún pool
            return;
        }

        obj.SetActive(false);
        poolDictionary[prefab].Enqueue(obj);
    }
}
