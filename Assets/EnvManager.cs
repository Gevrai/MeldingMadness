using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManager : MonoBehaviour {
    public GameObject wall;
    public float radius;
    public int nb = 20;

    void Awake() {
        for (int i = 0; i < nb; i++) {
            var position = new Vector3(radius, 0, 0);
            var quat = Quaternion.Euler(0, (360f / (float)nb) * i, 0);
            position = quat * position;
            GameObject go = Instantiate(wall, position, quat, this.transform);
        }
    }

}
