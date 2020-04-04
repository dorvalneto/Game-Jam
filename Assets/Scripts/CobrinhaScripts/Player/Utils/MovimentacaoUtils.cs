using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoUtils : MonoBehaviour
{
    public void movieUtils(MOVIE movie,Rigidbody2D corpoRigido,float speed)
    {
        switch (movie)
        {
            case MOVIE.Baixo:
                corpoRigido.transform.Translate(Vector3.down * (speed * Time.deltaTime));
                break;
            case MOVIE.Cima :
                corpoRigido.transform.Translate(Vector3.up * (Time.deltaTime * speed));
                break;
            case MOVIE.Direita:
                corpoRigido.transform.Translate(Vector3.right * (Time.deltaTime * speed));
                break;
            case MOVIE.Esquerda:
                corpoRigido.transform.Translate(Vector3.left *(Time.deltaTime * speed));
                break;
            default:
                corpoRigido.transform.Translate(Vector3.zero * (Time.deltaTime * speed));
                break;
        }
    }
}
