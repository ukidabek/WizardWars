using BaseGameLogic.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Battlefield
{
    public abstract class FieldPrefabProvider : MonoBehaviour
    {
        public abstract GameObject Field { get; }
    }

    [Serializable] public class FieldSelectedCallback : UnityEvent<Transform> { }

    public class Battlefield : SingletonMonoBehaviour<Battlefield>
    {
        [SerializeField] private Vector2Int size = Vector2Int.zero;
        private Field[,] fields = null;

        [SerializeField] private FieldPrefabProvider provider = null;

        [Space] public FieldSelectedCallback FieldSelectedCallback = new FieldSelectedCallback();

        protected override void Awake()
        {
            base.Awake();

            fields = new Field[size.x, size.y];
            for (int x = 0; x < size.x; x++)
            {
                var row = new GameObject("Row");
                row.transform.SetParent(transform);
                row.transform.localPosition = new Vector3(x, 0, 0);
                for (int y = 0; y < size.y; y++)
                {
                    var field = provider.Field;
                    fields[x, y] = field.GetComponent<Field>();
                    fields[x, y].OnMouseDownCallback += OMD;
                    field.transform.SetParent(row.transform);
                    field.transform.localPosition = new Vector3(0, 0, y);
                }
            }
        }

        private void OMD(Vector2Int coordinate)
        {
            FieldSelectedCallback.Invoke(fields[coordinate.x, coordinate.y].FieldTransfomr);
        }
    }
}
