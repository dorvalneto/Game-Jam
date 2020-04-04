using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrastaProjetil : MonoBehaviour
{

    bool clicou;

    public float esticadaMaxima = 3.0f;
    float esticadaMaximaQuadrada;

    SpringJoint2D mola;
    Rigidbody2D meuRigidbody;

    Transform estilingue;
    Ray raioParaMouse;
    Ray raioEstilingueFrente;

    CircleCollider2D colisor;
    float medidaCirculo;

    Vector2 velocidadeAnterior;

    public LineRenderer linhaFrente;
    public LineRenderer linhaTras;

    private void Awake()
    {
        colisor = GetComponent<CircleCollider2D>();
        meuRigidbody = GetComponent<Rigidbody2D>();
        mola = GetComponent<SpringJoint2D>();
    }

    void Start()
    {


        estilingue = mola.connectedBody.transform;
        esticadaMaximaQuadrada = esticadaMaxima * esticadaMaxima;
        raioParaMouse = new Ray(estilingue.position, Vector3.zero);
        raioEstilingueFrente = new Ray(linhaFrente.transform.position, Vector3.zero);

        medidaCirculo = colisor.radius;

        ConfiguraLinha();
    }

    void ConfiguraLinha()
    {
        linhaFrente.SetPosition(0, linhaFrente.transform.position + Vector3.forward * -0.03f);
        linhaTras.SetPosition(0, linhaTras.transform.position + Vector3.forward * -0.01f);

        linhaFrente.sortingLayerName = "Frente;";
        linhaTras.sortingLayerName = "Frente;";

        linhaFrente.sortingOrder = 3;
        linhaTras.sortingOrder = 1;
    }

    void AtualizaLinha()
    {
        Vector2 estilingueParaProgetil = transform.position - linhaFrente.transform.position;
        raioEstilingueFrente.direction = estilingueParaProgetil;
        Vector3 pontoDeAmarra = raioEstilingueFrente.GetPoint(estilingueParaProgetil.magnitude + medidaCirculo);

        pontoDeAmarra.z = -0.03f;
        linhaFrente.SetPosition(1, pontoDeAmarra);
        pontoDeAmarra.z = -0.01f;
        linhaTras.SetPosition(1, pontoDeAmarra);
    }

    void Update()
    {
        if (clicou == true)
            Arrastar();

        if (mola != null)
        {
            if (!meuRigidbody.isKinematic && velocidadeAnterior.sqrMagnitude > meuRigidbody.velocity.sqrMagnitude)
            {
                Destroy(mola);
                meuRigidbody.velocity = velocidadeAnterior;
            }

            if (!clicou)
                velocidadeAnterior = meuRigidbody.velocity;

            AtualizaLinha();
        }
        else
        {

        }
    }

    private void OnMouseDown()
    {
        clicou = true;
        mola.enabled = false;
    }

    private void OnMouseUp()
    {
        clicou = false;
        mola.enabled = true;
        meuRigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    void Arrastar()
    {
        Vector3 posicaoMouseMundo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 estilingueParaMouse = posicaoMouseMundo - estilingue.position;


        if (estilingueParaMouse.sqrMagnitude > esticadaMaximaQuadrada)
        {
            raioParaMouse.direction = estilingueParaMouse;
            posicaoMouseMundo = raioParaMouse.GetPoint(esticadaMaxima);
        }

        posicaoMouseMundo.z = 0;
        transform.position = posicaoMouseMundo;
    }
}
