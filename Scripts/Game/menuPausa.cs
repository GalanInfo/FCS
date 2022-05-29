using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausaScript : MonoBehaviour
{
    private GameObject botonPausa;

    private GameObject menuPausa;

    public void Pausa()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanduar()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }
}
