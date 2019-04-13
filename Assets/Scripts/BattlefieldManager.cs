using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlefield;
using PlayerLogic;
using BaseGameLogic.Singleton;
using System;


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
            if(player.Id == id)
            {
                player.transform.position = transform.position;
                player.transform.rotation = transform.rotation;
            }
        }
    }

    private List<PlayerSpot> playerSpots = new List<PlayerSpot>();
    private Queue<Player> players = new Queue<Player>();

    protected override void Awake()
    {
        base.Awake();
        Player.OnPlayerAdded += OnPlayerAdd;
        StartCoroutine(MovePlayerCoroutine());
    }

    private void OnPlayerAdd(Player player)
    {
        players.Enqueue(player);
    }

    private IEnumerator MovePlayerCoroutine()
    {
        yield return new WaitForEndOfFrame();
        while(players.Count > 0)
        {
            var player = players.Dequeue();
            foreach (var spot in playerSpots)
                spot.Move(player);
        }
    }

    internal void PositionBattlefield(Transform transform)
    {
        if (Battlefield.Battlefield.Instance != null)
        {
            Battlefield.Battlefield.Instance.transform.position = transform.position;
            Battlefield.Battlefield.Instance.transform.rotation = transform.rotation;
        }
    }

    internal void MovePlayer(Player.ID id, Transform transform)
    {
        playerSpots.Add(new PlayerSpot(transform, id));
    }
}
