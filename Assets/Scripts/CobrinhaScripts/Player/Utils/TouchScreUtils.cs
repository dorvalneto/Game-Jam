using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreUtils : MonoBehaviour
{
    Vector3 init = new Vector3();
    Vector3 fim = new Vector3();
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
                Debug.Log("Fim");
                fim = touch.position;
                Debug.Log("inici"+init);
                Debug.Log("fim"+fim);
                Debug.Log("angle: "+calculateAngle(init, fim)); 
                Debug.Log("angle Unity: "+Vector3.Angle(init,fim)); 
            }
        }
    }

    private float calculateAngle(Vector3 inicio, Vector3 fim)
    {
        float dot = inicio.x * fim.x + inicio.y * fim.y;
        float angle = Mathf.Acos(dot / (inicio.magnitude * fim.magnitude));
        return angle * Mathf.Rad2Deg +((2*Mathf.PI*Mathf.Rad2Deg)-180f);
    }
  }