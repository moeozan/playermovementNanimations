using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SphericalVector
{
    public float Lenght;
    public float Zenith;
    public float Azimuth;

    public SphericalVector(float azimuth, float zenith, float lenght)
    {
        Lenght = lenght;
        Zenith = zenith;
        Azimuth = azimuth;
    }

    public Vector3 Direction
    {
        get { 
            Vector3 dir;
            float verticalAngle = Zenith * Mathf.PI / 2f;
            dir.y = Mathf.Sin(verticalAngle);
            float h = Mathf.Cos(verticalAngle);

            float horizontalAngle = Azimuth * Mathf.PI;
            dir.x = h * Mathf.Sin(horizontalAngle);
            dir.z = h * Mathf.Cos(horizontalAngle);
            return dir;        
        }
    }

    public Vector3 Position
    {
        get
        {
            return Lenght * Direction;
        }

    }

}