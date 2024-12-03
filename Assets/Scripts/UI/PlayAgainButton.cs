using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgainButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        // Obtener el componente Button
        button = GetComponent<Button>();
        
        // Añadir el listener para el click
        if (button != null)
            button.onClick.AddListener(RestartGame);
    }

    public void RestartGame()
    {
        // Restaurar el timeScale por si estaba pausado
        Time.timeScale = 1f;
        
        // Recargar la escena actual
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
