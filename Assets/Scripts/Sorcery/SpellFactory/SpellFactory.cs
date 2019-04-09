using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sorcery.Spells;

namespace Sorcery
{
    public class SpellFactory : MonoBehaviour
    {
        public static SpellFactory Instance { get; private set; }

        [SerializeField] private List<Spell> spells = new List<Spell>();
        private Dictionary<string, Spell> spellDatabase = new Dictionary<string, Spell>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);

            foreach (var item in spells)
            {
                spellDatabase.Add(item.name, item);
            }
        }
    }

}
