using Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerLogic;
using Battlefield;

public class CastSpellCommandInvoker : CommandInvoker/*, IBattlefieldUser*/
{
    [SerializeField] private Player player = null;  // Change to id.
    [SerializeField] private int index = -1;
    public int Index { get => index; set => index = value; }
    [SerializeField] private Vector3 position = Vector3.zero;
    [SerializeField] private Quaternion rotation = Quaternion.identity;
    [SerializeField] private Vector2Int coordinate = Vector2Int.zero;

    protected override Command Command => new CastSpellCommand((int)player.Id, index, coordinate);

    private void Awake()
    {
        Battlefield.Battlefield.FieldSelectedCallback += GetCoordinate;
    }

    private void GetCoordinate(Vector2Int coordinate)
    {
        this.coordinate = coordinate;
        if (index >= 0)
        {
            Invoke();
            index = -1;
        }
    }
}
