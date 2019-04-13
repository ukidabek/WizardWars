using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldSpot : MonoBehaviour
{
    private void Start()
    {
        BattlefieldManager.Instance.PositionBattlefield(this.transform);
    }
}
