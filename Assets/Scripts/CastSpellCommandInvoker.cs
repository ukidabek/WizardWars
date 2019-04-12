using Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpellCommand : Command
{
    private int name = 0;
    private int index = 0;
    private Vector3 positon = Vector3.zero;
    private Quaternion rotation = Quaternion.identity;

    public CastSpellCommand(int player, int index, Vector3 positon, Quaternion rotation)
    {
        this.name = player;
        this.index = index;
        this.positon = positon;
        this.rotation = rotation;
    }

    public override void Execute()
    {
        Player.Get((Player.ID)name)?.CastSpell(index, positon, rotation);
    }
}

public class CastSpellCommandInvoker : CommandInvoker
{
    [SerializeField] private Player player = null;  // Change to id.
    [SerializeField] private int index = -1;
    public int Index { get => index; set => index = value; }
    [SerializeField] private Vector3 position = Vector3.zero;
    [SerializeField] private Vector3 rotation = Vector3.zero;

    protected override Command Command => new CastSpellCommand((int)player.Id, index, position, Quaternion.Euler(rotation));

    private void Start()
    {
        Battlefield.Battlefield.Instance.FieldSelectedCallback.AddListener(GetTransform);
    }

    public void GetTransform(Transform transform)
    {
        if(index >= 0)
        {
            position = transform.position;
            Invoke();
            index = -1;
        }
    }
}
