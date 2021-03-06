﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Sorcery.Spells
{
    [CreateAssetMenu(fileName = "NewSpell", menuName = "Sorcery/Spell")]
    public class Spell : ScriptableObject
    {
        [SerializeField] private Sprite icon = null;
        public Sprite Icon { get => icon; }

        [SerializeField] private float castTime = 1f;
        public float CastTime { get => castTime; }

        [SerializeField] private float cooldwonTime = 1f;
        public float CooldwonTime { get => cooldwonTime; }

        [SerializeField] private float cost = 5f;
        public float Cost { get => cost; }

        [SerializeField] private SpellType type = null;
        public SpellType Type { get => type; }

        [SerializeField] private GameObject spellObject = null;
        private Pool pool = null;
        public GameObject SpellObject
        {
            get
            {
                if (pool == null)
                    pool = new Pool(spellObject);
                return pool.Get();
            }
        }
    }
}
