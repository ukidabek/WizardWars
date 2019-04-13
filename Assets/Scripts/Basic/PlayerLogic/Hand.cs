using Sorcery.Spells;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerLogic
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private List<Spell> spells = null;
        public List<Spell> Spells { get => spells; set => UpdateSpellHand.Invoke(spells = value); }
        public UpdateSpellHand UpdateSpellHand = new UpdateSpellHand();

        public Spell this[int i]
        {
            get => spells[i];
            set
            {
                spells[i] = value;
                UpdateSpellHand.Invoke(spells);
            }
        }
    }
}