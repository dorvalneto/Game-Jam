using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoUtils : MonoBehaviour
{
    public void movieUtils(MOVE move,Rigidbody2D corpoRigido,float speed)
    {
        switch (move)
        {
            case MOVE.Baixo:
                corpoRigido.transform.Translate(Vector3.down * (speed * Time.deltaTime));
                break;
            case MOVE.Cima :
                corpoRigido.transform.Translate(Vector3.up * (Time.deltaTime * speed));
                break;
            case MOVE.Direita:
                corpoRigido.transform.Translate(Vector3.right * (Time.deltaTime * speed));
                break;
            case MOVE.Esquerda:
                corpoRigido.transform.Translate(Vector3.left *(Time.deltaTime * speed));
                break;
            default:
                corpoRigido.transform.Translate(Vector3.zero * (Time.deltaTime * speed));
                break;
        }
    }
}
