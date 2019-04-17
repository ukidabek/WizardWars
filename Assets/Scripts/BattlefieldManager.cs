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

    public interface IBattlefieldUser
    {
        void GetBattlefield(Battlefield battlefield);
    }

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

        public BattleFieldSelectedEvent battleFieldSelected = new BattleFieldSelectedEvent();

        protected override void Awake()
        {
            base.Awake();
            Player.OnPlayerAdded += OnPlayerAdd;

            foreach (var item in gameObject.scene.GetRootGameObjects())
                foreach (var user in item.GetComponentsInChildren<IBattlefieldUser>())
                    battleFieldSelected.AddListener(user.GetBattlefield);
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
                var battlefield = battlefields[UnityEngine.Random.Range(0, battlefields.Length)];
                battlefield.Build();
                SetPlayerPosition(Player.ID.A, battlefield.PlayerASlot);
                SetPlayerPosition(Player.ID.B, battlefield.PlayerBSlot);

                var cameraTransform = Camera.main.transform;
                cameraTransform.position = battlefield.CameraSlot.position;
                cameraTransform.rotation = battlefield.CameraSlot.rotation;

                battleFieldSelected.Invoke(battlefield);
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
    }
}