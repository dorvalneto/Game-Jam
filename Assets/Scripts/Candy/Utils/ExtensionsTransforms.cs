using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionsTransforms
{
    public static IEnumerator Move(this Transform t, Vector3 target, float duration)
    {
        Vector3 diffVecto = (target - t.position);
        float diffLength = diffVecto.magnitude;
        diffVecto.Normalize();
        float conter = 0;
        while (conter < duration)
        {
            float movAmount = (Time.deltaTime * diffLength/duration);
            t.position += diffVecto * movAmount;
            conter += Time.deltaTime;
            yield return null;
        }

        t.position = target;
    }
}