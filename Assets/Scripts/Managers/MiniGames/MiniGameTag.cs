using MeldingMadness.Player;
using MeldingMadness.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MeldingMadness.Managers {
    public class MiniGameTag : BaseMiniGame {

        [SerializeField] private List<Transform> spawners;
        [SerializeField] private float touchDelay = 0.1f;
        [SerializeField] private float cameraDescentSpeed = 1f;

        private BasePlayer currentTarget;
        private float lastTouch;

        protected override void Start() {
            base.Start();
            SetCurrentTargetEndOfFrame(players[Random.Range(0, players.Count)]);
            lastTouch = Time.time;
            camera = Camera.main;

            foreach (BasePlayer p in players) {
                var playerCollision = GetComponent<PlayerCollision>();
                if (playerCollision == null) {
                    playerCollision = gameObject.AddComponent<PlayerCollision>();
                }
                playerCollision.OnPlayersCollided += OnPlayersCollision;
            }
        }

        void Update() {
            Debug.Log(players.Count);
            if (players.Count < 2) {
                return;
            }
            FollowTargetPlayer();
            CanSeePlayers();
            ZoomToPlayers();
        }

        private void ZoomToPlayers() {
            var pos = camera.transform.position;
            pos.y -= Time.deltaTime * cameraDescentSpeed;
            camera.transform.position = pos;
        }

        private void FollowTargetPlayer() {
            var pos = camera.transform.position;
            pos.x = currentTarget.transform.position.x;
            pos.z = currentTarget.transform.position.z;

            // If swithing target, don't make the player barf
            var lerpTime = 5f;
            var t = Mathf.InverseLerp(lastTouch, lastTouch + lerpTime, Time.time);
            camera.transform.position = Vector3.Lerp(camera.transform.position, pos, t);
        }

        private void CanSeePlayers() {
            var frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
            foreach (var p in GameManager.Instance.players) {
                var visible = GeometryUtility.TestPlanesAABB(frustumPlanes, p.gameObject.GetComponent<Collider>().bounds);
                if (!visible) {
                    Reset();
                }
            }
        }


        public void OnPlayersCollision(BasePlayer b1, BasePlayer b2) {
            if (lastTouch + touchDelay > Time.time) {
                return;
            }
            lastTouch = Time.time;

            if (currentTarget == b1) {
                SetCurrentTargetEndOfFrame(b2);
            }
            if (currentTarget == b2) {
                SetCurrentTargetEndOfFrame(b1);
            }
        }

        private void SetCurrentTargetEndOfFrame(BasePlayer newTarget) {
            if (newTarget == currentTarget) {
                return;
            }
            Async.RunEndOfFrame(this, () => {
                // Chasing players are faster
                currentTarget?.playerMovement.SetMovementSpeedBonus(1f);
                newTarget?.playerMovement.SetMovementSpeedBonus(2f);
                currentTarget = newTarget;
            });
        }

        private int i = 0;
        protected override void OnSetupPlayer(BasePlayer player) {
            player.gameObject.transform.position = spawners[i % spawners.Count].transform.position;
            if (i == 0) {
                currentTarget = player;
            }
            i++;
        }
    }
}
