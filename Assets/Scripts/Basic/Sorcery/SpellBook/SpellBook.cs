using Sorcery.Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sorcery
{
    public class SpellBook : MonoBehaviour
    {
        [SerializeField] private List<string> spells = new List<string>();
        private Queue<Spell> spellsQueue = new Queue<Spell>();

        private void Awake()
        {
            foreach (var item in spells)
            {
                Spell spell = SpellDatabase.Instance.GetSpell(item);
                if(spell != null)
                    spellsQueue.Enqueue(spell);
            }
        }

        public List<Spell> GetHand()
        {
            List<Spell> hand = new List<Spell>();
            for (int i = 0; i < 5; i++)
                hand.Add(GetSpell());
            return hand;
        }

        public void ReturnSpell(Spell spell)
        {
            spellsQueue.Enqueue(spell);
        }

        public Spell GetSpell()
        {
            return spellsQueue.Dequeue();
        }
    }
}
