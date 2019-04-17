using Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerLogic;
using Battlefield;

public class CastSpellCommandInvoker : CommandInvoker, IBattlefieldUser
{
    [SerializeField] private Player player = null;  // Change to id.
    [SerializeField] private int index = -1;
    public int Index { get => index; set => index = value; }
    [SerializeField] private Vector3 position = Vector3.zero;
    [SerializeField] private Quaternion rotation = Quaternion.identity;

    [SerializeField] private Battlefield.Battlefield battlefield = null;

    protected override Command Command => new CastSpellCommand((int)player.Id, index, position, rotation);

    public void GetBattlefield(Battlefield.Battlefield battlefield)
    {
        if (this.battlefield != null)
            this.battlefield.FieldSelectedCallback.RemoveListener(GetTransform);

        battlefield.FieldSelectedCallback.AddListener(GetTransform);
        this.battlefield = battlefield;
    }

    public void GetTransform(Transform transform)
    {
        if(index >= 0)
        {
            position = transform.position;
            rotation = transform.rotation;
            Invoke();
            index = -1;
        }
    }
}
