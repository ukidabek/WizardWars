using Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerLogic;

public class CastSpellCommandInvoker : CommandInvoker
{
    [SerializeField] private Player player = null;  // Change to id.
    [SerializeField] private int index = -1;
    public int Index { get => index; set => index = value; }
    [SerializeField] private Vector3 position = Vector3.zero;
    [SerializeField] private Quaternion rotation = Quaternion.identity;

    protected override Command Command => new CastSpellCommand((int)player.Id, index, position, rotation);

    private void Start()
    {
        //Battlefield.Battlefield.Instance.FieldSelectedCallback.AddListener(GetTransform);
    }

    public void GetBattlefield(Battlefield.Battlefield battlefield)
    {
        battlefield.FieldSelectedCallback.AddListener(GetTransform);
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
