using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardUtils : MonoBehaviour
{
   public MOVIE actionKeyBoard()
   {
      if (Input.GetKey(KeyCode.UpArrow))
      {
         return MOVIE.Cima;
      }

      if (Input.GetKey(KeyCode.DownArrow))
      {
         return MOVIE.Baixo;
      }

      if (Input.GetKey(KeyCode.LeftArrow))
      {
         return MOVIE.Esquerda;
      }

      if (Input.GetKey(KeyCode.RightArrow))
      {
         return MOVIE.Direita;
      }
      return MOVIE.Parado;
   }
}
