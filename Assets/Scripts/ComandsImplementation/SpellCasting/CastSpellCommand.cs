using Commands;
using UnityEngine;
using PlayerLogic;

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
