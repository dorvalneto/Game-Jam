using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnComida : MonoBehaviour
{
    public GameObject comida;
    public Transform barreiraDireita;
    public Transform barreiraEsquerda;
    public Transform barreiraSuperior;
    public Transform barreiraInferior;
    private float dis;
    void Start()
    {
        InvokeRepeating("spawnComida", 0.3f, 2f);
        dis = barreiraDireita.GetComponent<BoxCollider2D>().size.y*2f;
    }

    // private void Update()
    // {
    //     Debug.DrawLine(new Vector3(barreiraDireita.position.x-dis,barreiraSuperior.position.y-dis,10f), new Vector3(barreiraEsquerda.position.x+dis,barreiraInferior.position.y+dis,10f));
    // }

    private void spawnComida()
    {
        Instantiate(comida, new Vector3(Random.Range(barreiraEsquerda.position.x + dis,barreiraDireita.position.x-dis),Random.Range(barreiraInferior.position.y+dis,barreiraSuperior.position.y-dis),10f),Quaternion.identity);
    }
}