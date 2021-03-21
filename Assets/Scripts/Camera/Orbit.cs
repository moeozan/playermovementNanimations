using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public SphericalVector sphericalVector = new SphericalVector(0,0,1);

    protected virtual void FixedUpdate()
    {
        transform.position = sphericalVector.Position;
    }
}
