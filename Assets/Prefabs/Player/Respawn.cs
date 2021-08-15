using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    [SerializeField] float yPlane;

    Vector3 initialPosition;
    Quaternion initialRotation;
    Rigidbody rb;

    void Start() {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (transform.position.y < yPlane) {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            rb.velocity = Vector3.zero;
        }
    }
}
