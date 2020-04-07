using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{

    public static IEnumerator Move(this Transform t, Vector3 pos, float duration)
    {
        Vector3 direction = pos - t.position;
        float distance = direction.magnitude;
        direction.Normalize();

        float startTime = 0;

        while(startTime < duration)
        {
            float remainingDistance = (distance * Time.deltaTime) / duration;
            t.position += direction * remainingDistance;
            startTime += Time.deltaTime;
            yield return null;
        }
        t.position = pos;
    }

    public static IEnumerator Scale(this Transform t, Vector3 scale, float duration)
    {
        Vector3 direction = scale - t.localScale;
        float size = direction.magnitude;
        direction.Normalize();

        float startTime = 0;

        while (startTime < duration)
        {
            float remainingDistance = (size * Time.deltaTime) / duration;
            t.localScale += direction * remainingDistance;
            startTime += Time.deltaTime;
            yield return null;
        }
        t.localScale = scale;
    }

}
