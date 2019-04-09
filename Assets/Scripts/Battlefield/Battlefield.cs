using BaseGameLogic.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Battlefield
{
    public abstract class FieldPrefabProvider :MonoBehaviour
    {
        public abstract GameObject Field { get; }
    }

    public class Battlefield : SingletonMonoBehaviour<Battlefield>
    {
        [SerializeField] private  Vector2Int size = Vector2Int.zero;

        [SerializeField] private FieldPrefabProvider provider = null;

        protected override void Awake()
        {
            base.Awake();

            for (int x = 0; x < size.x; x++)
            {
                var row = new GameObject("Row");
                row.transform.SetParent(transform);
                row.transform.localPosition = new Vector3(x, 0, 0);
                for (int y = 0; y < size.y; y++)
                {
                    var field = provider.Field;
                    field.transform.SetParent(row.transform);
                    field.transform.localPosition = new Vector3(0, 0, y);
                }
            }
        }
    }
}
