using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sorcery.Spells
{    
    [CreateAssetMenu(fileName = "NewSpell", menuName = "Sorcery/Create Spell")]
    public class Spell : ScriptableObject
    {
        [SerializeField] private Sprite icon = null;
        public Sprite Icon { get => icon; }

        [SerializeField] private float castTime = 1f;
        public float CastTime { get => castTime; }

        [SerializeField] private float cost = 5f;
        public float Cost { get => cost; }

        [SerializeField] private GameObject spellObject = null;
        public GameObject SpellObject { get => spellObject; }
    }
}
