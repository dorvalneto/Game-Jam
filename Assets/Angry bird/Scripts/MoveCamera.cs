using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public Transform projetil;

    public Transform limiteEsquerdo;
    public Transform limiteDireito;

    Vector3 novaPosicao;

    void Update()
    {
        novaPosicao = transform.position;
        novaPosicao.x = projetil.position.x;
        novaPosicao.x = Mathf.Clamp(novaPosicao.x, limiteEsquerdo.position.x, limiteDireito.position.x);
        transform.position = novaPosicao;
    }
}
