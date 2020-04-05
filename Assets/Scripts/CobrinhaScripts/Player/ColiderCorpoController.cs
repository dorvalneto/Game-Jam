using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ColiderCorpoController : MonoBehaviour
{
    public CircleCollider2D direita;
    public CircleCollider2D esquerda;
    public CircleCollider2D cima;
    public CircleCollider2D baixo;
    public void controleCasoCollider(MOVE move)
    {
        switch (move)
        {
            case MOVE.Cima:
                direita.enabled = false;
                esquerda.enabled = false;
                cima.enabled = true;
                baixo.enabled = false;
                break;
            case MOVE.Baixo:
                direita.enabled = false;
                esquerda.enabled = false;
                cima.enabled = false;
                baixo.enabled = true;
                break;
            case MOVE.Direita:
                direita.enabled = true;
                esquerda.enabled = false;
                cima.enabled = false;
                baixo.enabled = false;
                break;
            case MOVE.Esquerda:
                direita.enabled = false;
                esquerda.enabled = true;
                cima.enabled = false;
                baixo.enabled = false;
                break;
            default:
                direita.enabled = false;
                esquerda.enabled = false;
                cima.enabled = false;
                baixo.enabled = false;
                break;
        }
    }
    
}
