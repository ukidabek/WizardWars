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
        public class Pool
        {
            private GameObject prefab = null;
            private Transform parrent = null;
            private int maxCount = -1;

            private List<GameObject> List = new List<GameObject>();

            public Pool(GameObject prefab, Transform parrent = null, int initialCount = 5, int maxCount = -1)
            {
                this.prefab = prefab;
                this.parrent = parrent;
                this.maxCount = maxCount;

                for (int i = 0; i < initialCount; i++)
                    CreateNewInstance();
            }

            private GameObject CreateNewInstance()
            {
                GameObject instance = GameObject.Instantiate(prefab, parrent, false);
                instance.SetActive(false);
                List.Add(instance);
                return instance;
            }

            public GameObject Get()
            {
                GameObject instance = null;
                for (int i = 0; i < List.Count; i++)
                {
                    if (!List[i].activeSelf)
                    {
                        instance = List[i];
                        instance.SetActive(true);
                        List.RemoveAt(i);
                        List.Add(instance);
                        break;
                    }
                }

                return instance == null ? CreateNewInstance() : instance;
            }
        }

        [SerializeField] private Sprite icon = null;
        public Sprite Icon { get => icon; }

        [SerializeField] private float castTime = 1f;
        public float CastTime { get => castTime; }

        [SerializeField] private float cooldwonTime = 1f;
        public float CooldwonTime { get => cooldwonTime; }

        [SerializeField] private float cost = 5f;
        public float Cost { get => cost; }

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
