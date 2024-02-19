using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.audio;

public class enemigo : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("player"))
        {
           
            Debug.Log("detecto player " + other.name);

            //aqui iria el  codigo que le sigue
            audioSource.play();
        }

    }


    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("player"))
        {

            Debug.Log("detecto salida player " + other.name);

            //aqui iria el  codigo que le sigue
            audioSource.canceled();

        }

    }
}

