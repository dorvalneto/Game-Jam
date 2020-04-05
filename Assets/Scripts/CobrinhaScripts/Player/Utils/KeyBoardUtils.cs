using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardUtils : MonoBehaviour
{

   private MOVE _moveDefalt = MOVE.Direita;
   Vector3 init = new Vector3();
   Vector3 fim = new Vector3();
   private bool touchEvent;
   public float angle;
   
   public MOVE ActionKeyBoard()
   {
      if (Input.GetKey(KeyCode.UpArrow) || (angle > 70 && angle < 110) && _moveDefalt != MOVE.Baixo)
      {
         _moveDefalt = MOVE.Cima;
      }
      if (Input.GetKey(KeyCode.DownArrow) || (angle > 250 && angle < 290) && _moveDefalt != MOVE.Cima)
      {
         _moveDefalt = MOVE.Baixo;
      } 
      if (Input.GetKey(KeyCode.LeftArrow) || (angle < 200 && angle > 160) && _moveDefalt != MOVE.Direita)
      {
         _moveDefalt = MOVE.Esquerda;
      } 
      if (Input.GetKey(KeyCode.RightArrow) || (angle < 20 || angle > 340) && _moveDefalt!=MOVE.Esquerda)
      {
         _moveDefalt = MOVE.Direita;
      }
      return _moveDefalt;
   }
   
   public void getEventTouch()
   {
      if (Input.touchCount > 0)
      {
         Debug.Log("Touch");
         Touch touch = Input.GetTouch(0);
         if (touch.phase == TouchPhase.Began)
         {
            Debug.Log("Inicio");
            init = touch.position;
         }

         if (touch.phase == TouchPhase.Ended)
         {
            fim = touch.position;
            angle = calculateAngle(init, fim);
         }
      }
   }

   private float calculateAngle(Vector3 inicio, Vector3 fim)
   {
      float x = inicio.x - fim.x;
      float y = inicio.y - fim.y;
      float angulo = Mathf.Atan2(y, x);
      return ((angulo * Mathf.Rad2Deg) + ((2 * Mathf.PI * Mathf.Rad2Deg)) - 180f);
   }
}
