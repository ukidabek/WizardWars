using Battlefield;
using Commands;
using PlayerLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldRegionSelectCommand : Command
{
    private Player player = null;  // Change to id.
    private Battlefield.Battlefield battlefield = null;
    private int index;

    public BattlefieldRegionSelectCommand(Player player, Battlefield.Battlefield battlefield, int index)
    {
        this.player = player;
        this.battlefield = battlefield;
        this.index = index;
    }

    public override void Execute()
    {
        var spell = player.Hand[index];
        var region = BattlefieldRegionManager.Instance.GetRegion(spell.Type);
        battlefield.ShowFields(region);
    }
}

public class BattlefieldRegionSelectCommandInvoker : CommandInvoker, IBattlefieldUser
{
    [SerializeField] private Player player = null;  // Change to id.
    [SerializeField] private int index = -1;
    public int Index { get => index; set => index = value; }
    [SerializeField] private Battlefield.Battlefield battlefield = null;

    protected override Command Command => new BattlefieldRegionSelectCommand(player, battlefield, index);

    public void GetBattlefield(Battlefield.Battlefield battlefield)
    {
        this.battlefield = battlefield;
    }
}
