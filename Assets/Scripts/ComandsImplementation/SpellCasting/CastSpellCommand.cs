using Commands;
using UnityEngine;
using PlayerLogic;
using Battlefield;

public class CastSpellCommand : Command
{
    private int name = 0;
    private int index = 0;
    private int id;
    private Vector2Int coordinate;

    public CastSpellCommand(int id, int index, Vector2Int coordinate)
    {
        this.id = id;
        this.index = index;
        this.coordinate = coordinate;
    }

    private Transform GetFieldTransform()
    {
        return BattlefieldManager.Instance.SelectedBattlefield.GetField(coordinate).FieldTransfomr;
    }

    public override void Execute()
    {
        var field = GetFieldTransform();
        Player.Get((Player.ID)name)?.CastSpell(index, field.position, field.rotation);
    }
}
