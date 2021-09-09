using System;
using System.Collections.Generic;
using MeldingMadness.Player;
using UnityEngine;

namespace MeldingMadness.Managers {
    public class GameManager {
        private static readonly GameManager _instance = new GameManager();
        public static GameManager Instance => _instance;

        public BaseMiniGame currentMiniGame;
        public List<BasePlayer> players { get; private set; } = new List<BasePlayer>();

        public void RegisterPlayer(BasePlayer player) {
            if (players.Contains(player)) {
                throw new InvalidOperationException($"player {player} already registered");
            }
            currentMiniGame.SetupPlayer(player);
            // FIXME maybe player index wont match....
            players.Add(player);
        }

        public void UnregisterPlayer(BasePlayer player) {
            players.Remove(player);
        }

    }
}