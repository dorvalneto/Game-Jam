using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComida : MonoBehaviour
{
    public GameObject comida;
    public Camera camera;
    public GameObject borda;
    private Vector2 zonaBarra;

    void Start()
    {
        // Invoke("spawnComida",0.3f);
        InvokeRepeating("spawnComida", 0.3f, 2f);
        zonaBarra = borda.GetComponent<BoxCollider2D>().size;
    }

    private void spawnComida()
    {
        Vector3 pos = new Vector3(Random.value, Random.value, 10.0f);
            pos = camera.ViewportToWorldPoint(pos);
            // Vector3 posDiff = new Vector3( Random.Range(((pos.x/2)*-1)+(zonaBarra.y/2),(pos.x/2)-(zonaBarra.y/2)),Random.Range(((pos.y/2)*-1)+(zonaBarra.y/2),(pos.y/2)-(zonaBarra.y/2)),pos.z);
            Instantiate(comida, pos,Quaternion.identity);
    }
}