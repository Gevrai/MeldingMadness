using MeldingMadness.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MeldingMadness.Managers {
    public abstract class BaseMiniGame : MonoBehaviour {

        [SerializeField] private PlayerStats playerStats;

        public new Camera camera;

        public List<BasePlayer> players => GameManager.Instance.players;

        protected abstract void OnSetupPlayer(BasePlayer player);

        protected virtual void Awake() {
            GameManager.Instance.currentMiniGame = this;
        }

        protected virtual void Start() {

        }

        protected virtual void Reset() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void SetupPlayer(BasePlayer basePlayer) {
            basePlayer.InitPlayer(playerStats);
            OnSetupPlayer(basePlayer);
        }

    }
}