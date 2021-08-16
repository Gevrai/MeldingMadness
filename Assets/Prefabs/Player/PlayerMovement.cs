using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] int playerNumber;
    [SerializeField] Color playerColor;
    [SerializeField] float movementSpeed;
    [SerializeField] float compensateRatio;
    [SerializeField] float jumpImpulse;

    [SerializeField] float boostImpulse;
    [SerializeField] float boostDelay;

    new Camera camera;
    Rigidbody rb;
    Collider coll;

    float lastBoostTimestamp = 0f;

    bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.1f);

    bool CanBoost => Time.time > (lastBoostTimestamp + boostDelay);

    private void Awake() { InitPlayer(); }

    void InitPlayer() {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", playerColor);
        DynamicGI.UpdateEnvironment();
    }
    void Start() {
        camera = Camera.main;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    void Update() {

        float horizontal = 0;
        float vertical = 0;
        bool jump = false;
        bool boost = false;

        if (playerNumber == 1) {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            boost = Input.GetButtonDown("Fire1");
            jump = Input.GetButtonDown("Jump");
        }
        if (playerNumber == 2) {
            horizontal = Input.GetAxis("Horizontal2");
            vertical = Input.GetAxis("Vertical2");
            boost = Input.GetButtonDown("Boost2");
            jump = Input.GetButtonDown("Jump2");
        }

        // Direction based on camera's forward
        var camH = VectorUtils.StripY(camera.transform.right).normalized;
        var camV = VectorUtils.StripY(camera.transform.forward).normalized;
        var direction = horizontal * camH + vertical * camV;

        var dot = Vector3.Dot(direction.normalized, rb.velocity.normalized);
        var compensate = dot < 0 ? 1 + (-dot * compensateRatio) : 1;

        // Move
        rb.AddForce(compensate * movementSpeed * direction);

        // Boost!
        if (boost && CanBoost) {
            rb.AddForce(boostImpulse * direction, ForceMode.Impulse);
            lastBoostTimestamp = Time.time;
        }

        // Jump!
        if (jump && IsGrounded) {
            rb.AddForce(jumpImpulse * Vector3.up, ForceMode.Impulse);
        }

    }
}
