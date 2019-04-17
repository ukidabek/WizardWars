using BaseGameLogic.Singleton;
using Battlefield;
using Sorcery.Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattlefieldRegionManager", menuName = "Sorcery/BattlefieldRegionManager")]
public class BattlefieldRegionManager : SingletonScriptableObject<BattlefieldRegionManager>
{
    [Serializable]
    public class SpellTypeRegionMaping
    {
        [SerializeField] private SpellType spellType = null;
        public SpellType SpellType { get => spellType; }

        [SerializeField] private Region region = new Region();
        public Region Region { get => region; }
    }

    [SerializeField] private List<SpellTypeRegionMaping> spellTypeToRegionMap = new List<SpellTypeRegionMaping>();
    private Dictionary<SpellType, Region> pairs = new Dictionary<SpellType, Region>();

    protected override void Initialize()
    {
        foreach (var item in spellTypeToRegionMap)
            pairs.Add(item.SpellType, item.Region);
    }

    public Region GetRegion(SpellType spellType)
    {
        Region region = null;
        pairs.TryGetValue(spellType, out region);
        return region;
    }

}
