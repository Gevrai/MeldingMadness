using MeldingMadness.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MeldingMadness.Player {
    [RequireComponent(typeof(PlayerMovement))]
    public class BasePlayer : MonoBehaviour {
        public string playerName;

        [HideInInspector] public PlayerMovement playerMovement { get; private set; }

        private void Start() {
            playerMovement = GetComponent<PlayerMovement>();
            GameManager.Instance.RegisterPlayer(this);
            DontDestroyOnLoad(gameObject);
        }

        public void InitPlayer(PlayerStats stats) {
            GetComponent<Rigidbody>().constraints = stats.constraints;
            var playerIndex = GetComponent<PlayerInput>().playerIndex;
            var color = new Color[] { Color.red, Color.blue, Color.green }[playerIndex];
            GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
            playerMovement.stats = stats;
        }

    }
}