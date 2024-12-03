using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataformajugador : MonoBehaviour
{
    public GameObject Player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Parenteado");
                Player.transform.parent = transform;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.transform.parent = null;
        }


    }

}
