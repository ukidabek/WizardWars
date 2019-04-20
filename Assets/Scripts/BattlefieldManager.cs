using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlefield;
using PlayerLogic;
using BaseGameLogic.Singleton;
using System;
using UnityEngine.Events;

namespace Battlefield
{
    [Serializable] public class BattleFieldSelectedEvent : UnityEvent<Battlefield> { }
    [Serializable] public class FieldSelectedCallback : UnityEvent<Transform> { }

    public class BattlefieldManager : SingletonMonoBehaviour<BattlefieldManager>
    {
        private class PlayerSpot
        {
            private Transform transform;
            private Player.ID id = Player.ID.A;

            public PlayerSpot(Transform transform, Player.ID id)
            {
                this.transform = transform;
                this.id = id;
            }

            public void Move(Player player)
            {
                if (player.Id == id)
                {
                    player.transform.position = transform.position;
                    player.transform.rotation = transform.rotation;
                }
            }
        }

        private List<PlayerSpot> playerSpots = new List<PlayerSpot>();
        private Queue<Player> players = new Queue<Player>();

        [Space]
        public BattleFieldSelectedEvent BattleFieldSelected = new BattleFieldSelectedEvent();
        public FieldSelectedCallback FieldSelectedCallback = new FieldSelectedCallback();

        [Space]
        [SerializeField] private Battlefield selectedBattlefield = null;
        public Battlefield SelectedBattlefield { get => selectedBattlefield; }

        protected override void Awake()
        {
            base.Awake();
            Player.OnPlayerAdded += OnPlayerAdd;
        }

        private void OnPlayerAdd(Player player)
        {
            players.Enqueue(player);
        }

        public void SelectBattlefield()
        {
            var battlefields = GameObject.FindObjectsOfType<Battlefield>();
            if (battlefields.Length > 0)
            {
                selectedBattlefield = battlefields[UnityEngine.Random.Range(0, battlefields.Length)];
                selectedBattlefield.Build();
                SetPlayerPosition(Player.ID.A, selectedBattlefield.PlayerASlot);
                SetPlayerPosition(Player.ID.B, selectedBattlefield.PlayerBSlot);

                var cameraTransform = Camera.main.transform;
                cameraTransform.position = selectedBattlefield.CameraSlot.position;
                cameraTransform.rotation = selectedBattlefield.CameraSlot.rotation;

                BattleFieldSelected.Invoke(selectedBattlefield);
            }
        }

        private void SetPlayerPosition(Player.ID id, Transform playerSlot)
        {
            var player = Player.Get(id);
            player.transform.position = playerSlot.position;
            player.transform.rotation = playerSlot.rotation;
        }

        private IEnumerator MovePlayerCoroutine()
        {
            yield return new WaitForEndOfFrame();
            while (players.Count > 0)
            {
                var player = players.Dequeue();
                foreach (var spot in playerSpots)
                    spot.Move(player);
            }
        }

        internal void MovePlayer(Player.ID id, Transform transform)
        {
            playerSpots.Add(new PlayerSpot(transform, id));
        }

        public void HideAllFields()
        {
            SelectedBattlefield?.HideAllFields();
        }
    }
}