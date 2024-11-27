using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxLives = 3;           // Número máximo de vidas
    private int currentLives = 3;          // Vidas actuales

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;    // Array de puntos de spawn
    private Vector3 lastCheckpoint;    // Último checkpoint alcanzado

    [Header("Events")]
    public UnityEvent onPlayerDeath;   // Evento cuando el jugador muere
    public UnityEvent onGameOver;      // Evento cuando se acaban todas las vidas

    void Start()
    {
        // Inicializamos las vidas
        currentLives = maxLives;
        
        // Si hay spawn points, establecemos uno aleatorio como punto inicial
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            lastCheckpoint = GetRandomSpawnPoint();
            transform.position = lastCheckpoint;
        }
    }

    // Método para obtener un punto de spawn aleatorio
    private Vector3 GetRandomSpawnPoint()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
            return transform.position;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex].position;
    }

    // Método llamado cuando el jugador muere
    public void OnPlayerDeath()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            // Game Over
            onGameOver?.Invoke();
            // Aquí puedes añadir lógica adicional para el game over
        }
        else
        {
            // Invocamos el evento de muerte
            onPlayerDeath?.Invoke();
            
            // Respawneamos al jugador
            Respawn();
        }
    }

    // Método para respawnear al jugador
    private void Respawn()
    {
        // Obtenemos un punto de spawn aleatorio
        Vector3 spawnPosition = GetRandomSpawnPoint();
        
        // Teletransportamos al jugador
        transform.position = spawnPosition;
        
        // Resetear velocidad del Rigidbody si existe
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    // Método para cuando el jugador toca un trigger de muerte
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathTrigger"))
        {
            OnPlayerDeath();
        }
    }

    // Método para obtener las vidas actuales (útil para UI)
    public int GetCurrentLives()
    {
        return currentLives;
    }
}
