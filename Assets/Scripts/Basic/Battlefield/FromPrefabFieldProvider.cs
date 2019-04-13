using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Battlefield
{
    public class FromPrefabFieldProvider : FieldPrefabProvider
    {

        [SerializeField] private GameObject fieldPrefab = null;
        public override GameObject Field
        {
            get
            {
                GameObject instance = fieldPrefab == null? null: Instantiate(fieldPrefab);
                instance?.SetActive(true);
                return instance;
            }
        }
    }
}
