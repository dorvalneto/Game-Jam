using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resetador : MonoBehaviour
{

    public Rigidbody2D alvo;
    SpringJoint2D mola;

    public float velocidadeParada = 0.025f;
    float velocidadeParadaQuadrada;

    private void Awake()
    {
        mola = alvo.GetComponent<SpringJoint2D>();
    }

    //// Use this for initialization
    void Start()
    {
        velocidadeParadaQuadrada = velocidadeParada * velocidadeParada;
    }

    //// Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
            Resetar();

        if (alvo.velocity.sqrMagnitude < velocidadeParadaQuadrada && mola == null)
            Resetar();

    }

    void Resetar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerExit2D(Collider2D colisao)
    {
        if (colisao.GetComponent<Rigidbody2D>() == alvo)
            Resetar();
    }
}
