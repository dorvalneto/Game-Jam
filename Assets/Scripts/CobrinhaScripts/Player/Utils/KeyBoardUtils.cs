using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardUtils : MonoBehaviour
{
   private MOVIE dafautl;
   public MOVIE actionKeyBoard()
   {
      if (Input.GetKey(KeyCode.UpArrow))
      {
         dafautl = MOVIE.Cima;
      }
      if (dafautl == null ||Input.GetKey(KeyCode.DownArrow))
      {
         dafautl = MOVIE.Baixo;
      } 
      if (Input.GetKey(KeyCode.LeftArrow))
      {
         dafautl = MOVIE.Esquerda;
      } 
      if (Input.GetKey(KeyCode.RightArrow))
      {
         dafautl = MOVIE.Direita;
      }
      return dafautl;
   }
}
