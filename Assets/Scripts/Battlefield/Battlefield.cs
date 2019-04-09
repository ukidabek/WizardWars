using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Battlefield
{
    public abstract class FieldPrefabProvider :MonoBehaviour
    {
        public abstract GameObject Prefab { get; }
    }

    public class Battlefield : MonoBehaviour
    {
        [SerializeField] private  Vector2Int size = Vector2Int.zero;

        [SerializeField] private FieldPrefabProvider provider = null;
    }
}
