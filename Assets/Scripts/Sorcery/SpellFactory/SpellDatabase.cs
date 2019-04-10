using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sorcery.Spells;
using BaseGameLogic.Singleton;
using System;

namespace Sorcery
{
    [CreateAssetMenu(fileName = "SpellDatabase", menuName = "Sorcery/Spell Database")]
    public class SpellDatabase : SingletonScriptableObject<SpellDatabase>
    {
        [SerializeField] private List<Spell> spells = new List<Spell>();
        private Dictionary<string, Spell> spellDatabase = new Dictionary<string, Spell>();

        internal Spell GetSpell(string item)
        {
            Spell spell = null;
            spellDatabase.TryGetValue(item, out spell);
            return spell;
        }

        protected override void Initialize()
        {
            foreach (var item in spells)
                spellDatabase.Add(item.name, item);
        }
    }

}
