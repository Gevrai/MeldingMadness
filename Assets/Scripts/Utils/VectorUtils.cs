using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorUtils 
{
    public static Vector3 StripY(Vector3 vec) {
        return new Vector3(vec.x, 0, vec.z);
    }
}
