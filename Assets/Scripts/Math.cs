using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static Vector2 fromPolar(float magnitude, float angle)
    {
        float flippedAngle = flipAngle(angle);
        return new Vector2(magnitude * Mathf.Cos(flippedAngle), magnitude * Mathf.Sin(flippedAngle));
    }

    public static float flipAngle(float angle)
    {
        return (Mathf.PI / 2.0f) - angle;
    }

    public static float stepAngle(float angularVelocity, float angle, float deltaTime)
    {
        return angle + (angularVelocity / deltaTime);
    }
}
