using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverPanel;           // Panel que contiene la UI de Game Over
    public TextMeshProUGUI winnerText;        // Texto que muestra el ganador
    public Button playAgainButton;            // Botón de Play Again

    [Header("Player References")]
    public PlayerHealth player1Health;        // Referencia al PlayerHealth del Player 1
    public PlayerHealth player2Health;        // Referencia al PlayerHealth del Player 2

    void Start()
    {
        // Asegurarnos que el panel de Game Over está oculto al inicio
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Subscribirse a los eventos de Game Over de ambos jugadores
        if (player1Health != null)
            player1Health.onGameOver.AddListener(() => ShowGameOver("Player 2"));

        if (player2Health != null)
            player2Health.onGameOver.AddListener(() => ShowGameOver("Player 1"));
    }

    public void ShowGameOver(string winner)
    {
        // Activar el panel de Game Over
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Mostrar el mensaje del ganador
        if (winnerText != null)
        {
            winnerText.text = $"{winner} Wins!";
            
            // Opcional: Añadir efectos de animación al texto
            StartCoroutine(AnimateWinnerText());
        }

        // Opcional: Pausar el juego
        Time.timeScale = 0f;
    }

    private System.Collections.IEnumerator AnimateWinnerText()
    {
        float time = 0;
        Vector3 originalScale = winnerText.transform.localScale;

        while (true)
        {
            time += Time.unscaledDeltaTime;
            float scale = 1 + Mathf.Sin(time * 2) * 0.1f; // Efecto de pulsación
            winnerText.transform.localScale = originalScale * scale;
            yield return null;
        }
    }
}
