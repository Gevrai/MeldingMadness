using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlaneInstantiator : MonoBehaviour {
    public GameObject groundBallPrefab;
    public int amount = 1000;

    void Start() {
        var bounds = gameObject.GetComponent<Collider>().bounds;

        for (int i = 0; i < amount; i++) {
            var x = Random.Range(bounds.min.x, bounds.max.x);
            var z = Random.Range(bounds.min.z, bounds.max.z);

            GameObject go = Instantiate(groundBallPrefab);

            var scale = Random.Range(2f, 20f);
            go.transform.position = new Vector3(x, 0, z);
            go.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

}
