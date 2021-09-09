using UnityEngine;

namespace MeldingMadness.Utils {
    public class ConstantRotate : MonoBehaviour {
        [SerializeField] float rotationSpeed;

        void Update() {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}