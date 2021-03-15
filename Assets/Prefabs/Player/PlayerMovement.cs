using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] float movementSpeed;
    [SerializeField] float jumpImpulse;

    [SerializeField] float boostImpulse;
    [SerializeField] float boostDelay;

    new Camera camera;
    Rigidbody rb;
    Collider coll;

    float lastBoostTimestamp = 0f;

    bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.1f);

    bool CanBoost => Time.time > (lastBoostTimestamp + boostDelay);

    void Start() {
        camera = Camera.main;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    void Update() {

        // Direction based on camera's forward
        var horizontal = Input.GetAxis("Horizontal") * VectorUtils.StripY(camera.transform.right).normalized;
        var vertical = Input.GetAxis("Vertical") * VectorUtils.StripY(camera.transform.forward).normalized;
        var direction = horizontal + vertical;

        // Move
        rb.AddForce(movementSpeed * direction);

        // Boost!
        if (Input.GetButtonDown("Fire1") && CanBoost) {
            Debug.Log("boosting");
            rb.AddForce(boostImpulse * direction, ForceMode.Impulse);
            lastBoostTimestamp = Time.time;
        }

        // Jump!
        if (Input.GetButtonDown("Jump") && IsGrounded) {
            rb.AddForce(jumpImpulse * Vector3.up, ForceMode.Impulse);
        }

    }
}
