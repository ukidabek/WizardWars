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
