using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public float destroyMagnitude = 1;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(Tags.PLAYER)) {
            if (collision.relativeVelocity.magnitude >= destroyMagnitude) {
                Destroy(this.gameObject);
            }
        }
    }
}
