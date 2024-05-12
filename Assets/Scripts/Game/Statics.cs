using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics
{
    public static Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //https://www.youtube.com/watch?v=03GHtGyEHas
        //define the distance x and y first
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        //create a float the represent our distance
        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;
        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;
    }
    public static Vector3 CalculatePositionWithVelocity(Vector3 currentPosition, Vector3 velocity, float time)
    {
        // s= ut + att/2
        Vector3 atPosition = currentPosition + velocity * time + (Vector3.up * Physics.gravity.y * time * time) * 0.5f;
        return atPosition;
    }
}
