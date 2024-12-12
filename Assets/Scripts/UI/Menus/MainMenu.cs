using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Método que será llamado cuando se presione el botón
    public void StartGame()
    {
        // Cargar la escena del juego (índice 1)
        SceneManager.LoadScene(1);
    }
}
