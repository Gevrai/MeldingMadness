using System;
using UnityEngine;

namespace MeldingMadness.Player {

    [RequireComponent(typeof(BasePlayer))]
    public class PlayerCollision : MonoBehaviour {

        public Action<BasePlayer, BasePlayer> OnPlayersCollided;

        private BasePlayer thisPlayer;

        private void Start() {
            thisPlayer = GetComponent<BasePlayer>();
        }

        private void OnCollisionEnter(Collision collision) {
            if (OnPlayersCollided == null) {
                return; // no callback registered
            }
            var otherPlayer = collision.gameObject.GetComponent<BasePlayer>();
            if (otherPlayer != null) {
                OnPlayersCollided(thisPlayer, otherPlayer);
            }
        }
    }
}
