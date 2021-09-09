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

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(Tags.DEATH_ZONE)) {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            rb.velocity = Vector3.zero;
        }
    }
}
