using MeldingMadness.Managers;
using MeldingMadness.Utils;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace MeldingMadness.Player {
    public class PlayerMovement : MonoBehaviour {

        internal PlayerStats stats;
        private float movementSpeedBonus = 1f;

        Rigidbody rb;
        Collider coll;

        float lastBoostTimestamp = 0f;
        Vector2 movement;
        bool jumpPressed, boostPressed;

        bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.1f);
        bool CanBoost => Time.time > lastBoostTimestamp + stats.boostDelay;

        void Start() {
            rb = GetComponent<Rigidbody>();
            coll = GetComponent<Collider>();
        }

        public void OnMove(InputValue val) {
            movement = val.Get<Vector2>();
        }

        public void OnJump(InputValue val) {
            jumpPressed = val.isPressed;
        }

        public void OnBoost(InputValue val) {
            boostPressed = val.isPressed;
        }

        void FixedUpdate() {

            // Direction based on camera's forward
            var cam = Camera.main;
            var direction = movement.x * Right(cam) + movement.y * Up(cam);

            // If direction goes against current velocity, give a little help to stop.
            var dot = Vector3.Dot(direction.normalized, rb.velocity.normalized);
            var brakeRatio = dot < 0 ? 1 + -dot * stats.brakeCompensation : 1;

            // Move
            rb.AddForce(movementSpeedBonus * brakeRatio * stats.movementSpeed * direction);

            // Boost!
            if (boostPressed && CanBoost) {
                rb.AddForce(stats.boostImpulse * direction, ForceMode.Impulse);
                lastBoostTimestamp = Time.time;
            }

            // Jump!
            if (jumpPressed && IsGrounded) {
                rb.AddForce(stats.jumpImpulse * Vector3.up, ForceMode.Impulse);
            }

        }
        private Vector3 Right(Camera camera) {
            return VectorUtils.StripY(camera.transform.right).normalized;
        }

        private Vector3 Up(Camera camera) {
            switch (stats.movementSchema) {
                case PlayerStats.MovementScheme.TopDown:
                case PlayerStats.MovementScheme.SideScroller:
                    return VectorUtils.StripY(camera.transform.up).normalized;
                case PlayerStats.MovementScheme.ThirdPerson:
                    return VectorUtils.StripY(camera.transform.forward).normalized;
                default:
                    throw new InvalidOperationException($"unhandled movement schema {stats.movementSchema}");
            }
        }

        public void SetMovementSpeedBonus(float bonus) {
            movementSpeedBonus = bonus;
        }
    }
}