using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // M�todo que ser� llamado cuando se presione el bot�n
    public void StartGame()
    {
        // Cargar la escena del juego (�ndice 1)
        SceneManager.LoadScene(1);
    }
}
