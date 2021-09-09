using UnityEngine;

namespace MeldingMadness.Player {
    [CreateAssetMenu(menuName = "Player Stats")]
    public class PlayerStats : ScriptableObject {
        public enum MovementScheme {
            TopDown, SideScroller, ThirdPerson
        }
        public MovementScheme movementSchema;
        public RigidbodyConstraints constraints;
        public float movementSpeed;
        public float brakeCompensation;
        public float jumpImpulse;
        public float boostImpulse;
        public float boostDelay;
    }
}
