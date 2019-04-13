using PlayerLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldPlayerSpot : MonoBehaviour
{
    [SerializeField] private Player.ID id = Player.ID.A;

    private void Start()
    {
        BattlefieldManager.Instance.MovePlayer(id, this.transform);
    }
}
