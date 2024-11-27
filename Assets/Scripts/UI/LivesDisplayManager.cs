using UnityEngine;
using TMPro; // Asegúrate de tener TextMeshPro importado

public class LivesDisplayManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI player1LivesText;    // Referencia al TMP del Player 1
    public TextMeshProUGUI player2LivesText;    // Referencia al TMP del Player 2

    [Header("Player References")]
    public PlayerHealth player1Health;          // Referencia al PlayerHealth del Player 1
    public PlayerHealth player2Health;          // Referencia al PlayerHealth del Player 2

    void Start()
    {
        // Nos subscribimos a los eventos de muerte de ambos jugadores
        if (player1Health != null)
        {
            player1Health.onPlayerDeath.AddListener(UpdatePlayer1Lives);
            UpdatePlayer1Lives(); // Actualizamos el display inicial
        }

        if (player2Health != null)
        {
            player2Health.onPlayerDeath.AddListener(UpdatePlayer2Lives);
            UpdatePlayer2Lives(); // Actualizamos el display inicial
        }
    }

    // Actualiza el texto de vidas del Player 1
    void UpdatePlayer1Lives()
    {
        if (player1LivesText != null && player1Health != null)
        {
            player1LivesText.text = $"Player 1: {player1Health.GetCurrentLives()}";
        }
    }

    // Actualiza el texto de vidas del Player 2
    void UpdatePlayer2Lives()
    {
        if (player2LivesText != null && player2Health != null)
        {
            player2LivesText.text = $"Player 2: {player2Health.GetCurrentLives()}";
        }
    }
}
