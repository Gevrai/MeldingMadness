using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MeldingMadness.Managers {
    public class PlayerInputManager : MonoBehaviour {
        public void OnPlayerJoined(PlayerInput player) {
            Debug.Log($"PlayerInputManager::OnPlayerJoined {player.playerIndex}");
        }
        public void OnPlayerLeft(PlayerInput player) {
            Debug.Log($"PlayerInputManager::OnPlayerLeft {player.playerIndex}");
        }
    }
}