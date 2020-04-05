using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardUtils : MonoBehaviour
{
   private MOVE _moveDefalt = MOVE.Direita;
   public MOVE ActionKeyBoard()
   {
      if (Input.GetKey(KeyCode.UpArrow) && _moveDefalt != MOVE.Baixo)
      {
         _moveDefalt = MOVE.Cima;
      }
      if (Input.GetKey(KeyCode.DownArrow) && _moveDefalt != MOVE.Cima)
      {
         _moveDefalt = MOVE.Baixo;
      } 
      if (Input.GetKey(KeyCode.LeftArrow) && _moveDefalt != MOVE.Direita)
      {
         _moveDefalt = MOVE.Esquerda;
      } 
      if (Input.GetKey(KeyCode.RightArrow)&& _moveDefalt!=MOVE.Esquerda)
      {
         _moveDefalt = MOVE.Direita;
      }
      return _moveDefalt;
   }
}
