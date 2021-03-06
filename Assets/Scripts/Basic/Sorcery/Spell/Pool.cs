﻿using System.Collections.Generic;
using UnityEngine;

namespace Sorcery.Spells
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
                    List.RemoveAt(i);
                    List.Add(instance);
                    break;
                }
            }

            return instance == null ? CreateNewInstance() : instance;
        }
    }
}
