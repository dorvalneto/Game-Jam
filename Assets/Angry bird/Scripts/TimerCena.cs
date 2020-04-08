using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerCena : MonoBehaviour
{

    public string NomeDaCena = "cena";
    public float tempoParaCarregar = 10;

    float cronometro = 0;

    void Update()
    {
        cronometro += Time.deltaTime;
        if (cronometro > tempoParaCarregar)
        {
            SceneManager.LoadScene(NomeDaCena);
        }
    }
}