using BaseGameLogic.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Battlefield
{
    [Serializable]
    public class Region
    {
        [SerializeField] private string name = string.Empty;
        public string Name { get => name; }

        [Serializable]
        public class AxisLimit
        {
            [SerializeField] private bool limit = false;
            public bool Limit { get => limit; }

            [SerializeField] private Vector2Int range = Vector2Int.zero;
            public Vector2Int Range { get => range; }

            public bool IsInLimit(int v)
            {
                if (!limit) return true;
                return v >= Range.x && v <= (Range.y - 1);
            }
        }

        [SerializeField] private AxisLimit xAxisLimit = new AxisLimit();
        public AxisLimit XAxisLimit { get => xAxisLimit; }

        [SerializeField] private AxisLimit yAxisLimit = new AxisLimit();
        public AxisLimit YAxisLimit { get => yAxisLimit; }

        public bool IsInLimit(int x, int y)
        {
            return xAxisLimit.IsInLimit(x) && yAxisLimit.IsInLimit(y);
        }
    }

    public class Battlefield : MonoBehaviour
    {
        [SerializeField] private Vector2Int size = Vector2Int.zero;
        private Field[,] fields = null;
        [SerializeField] private float toFieldDistance = 1f;

        [Space]
        [SerializeField] private Transform playerASlot = null;
        public Transform PlayerASlot { get => playerASlot; }
        [SerializeField] private Transform playerBSlot = null;
        public Transform PlayerBSlot { get => playerBSlot; }
        [SerializeField] private Transform cameraSlot = null;
        public Transform CameraSlot { get => cameraSlot; }

        [Space]
        [SerializeField] private FieldPrefabProvider provider = null;
        [SerializeField] private Vector2Int selectedFieldCoordinate = Vector2Int.zero;
        public Vector2Int SelectedFieldCoordinate { get => selectedFieldCoordinate; }

        public static event Action<Vector2Int> FieldSelectedCallback = null;
        [Space] [SerializeField] private Color gridColor = Color.red;
        [SerializeField] private Color regionColor = Color.cyan;

        private Region region = new Region();

        public void Build()
        {
            fields = new Field[size.x, size.y];
            for (int x = 0; x < size.x; x++)
            {
                var row = new GameObject("Row");
                row.transform.SetParent(transform);
                row.transform.localRotation = Quaternion.identity;
                row.transform.localPosition = new Vector3(x, 0, 0);
                for (int y = 0; y < size.y; y++)
                {
                    var field = provider.Field;
                    field.gameObject.SetActive(false);
                    fields[x, y] = field.GetComponent<Field>();
                    fields[x, y].OnClick.AddListener(OnMouseDown);
                    field.transform.SetParent(row.transform);
                    field.transform.localRotation = Quaternion.identity;
                    field.transform.localPosition = new Vector3(0, 0, y);
                }
            }
        }

        private void OnMouseDown(Vector2Int coordinate)
        {
            FieldSelectedCallback?.Invoke(selectedFieldCoordinate = coordinate);
        }

        private int InvertAxixCoordinate(int size, int coordinate, bool invert)
        {
            return invert ? (size - 1) - coordinate : coordinate;
        }

        public Field GetField(Vector2Int coordinate, bool invertX = false, bool invertY = false)
        {
            return fields[InvertAxixCoordinate(size.x, coordinate.x, invertX), InvertAxixCoordinate(size.y, coordinate.y, invertY)];
        }

        public void ShowFields(Region region)
        {
            this.region = region;
            for (int x = 0; x < size.x; x++)
                for (int y = 0; y < size.y; y++)
                    fields[x, y].gameObject.SetActive(region.IsInLimit(x, y));
        }

        public void HideAllFields()
        {
            for (int x = 0; x < size.x; x++)
                for (int y = 0; y < size.y; y++)
                    if (fields[x, y].gameObject.activeSelf)
                        fields[x, y].gameObject.SetActive(false);
        }

        private void OnValidate()
        {
            if (playerASlot != null)
                playerASlot.transform.localPosition = Vector3.right * (size.x / 2f) + -Vector3.forward * toFieldDistance;

            if (playerBSlot != null)
                playerBSlot.transform.localPosition = Vector3.right * (size.x / 2f) + Vector3.forward * (size.y + toFieldDistance);
        }

        private void OnDrawGizmos()
        {
            var color = Gizmos.color;
            Gizmos.color = gridColor;
            Vector3 startPosition = Vector3.zero;
            for (int x = 0; x <= size.x; x++)
            {
                bool fieldInRegion = region.XAxisLimit.Limit && x >= region.XAxisLimit.Range.x && x <= region.XAxisLimit.Range.y;
                Gizmos.color = fieldInRegion ? regionColor : gridColor;

                startPosition = transform.position + transform.right * x;
                Gizmos.DrawLine(startPosition, startPosition + transform.forward * size.y);
            }
            for (int y = 0; y <= size.y; y++)
            {
                bool fieldInRegion = region.YAxisLimit.Limit && y >= region.YAxisLimit.Range.x && y <= region.YAxisLimit.Range.y;
                Gizmos.color = fieldInRegion ? regionColor : gridColor;
                startPosition = transform.position + transform.forward * y;
                Gizmos.DrawRay(startPosition, transform.right * size.x);
            }
            Gizmos.color = color;

            Gizmos.matrix = transform.localToWorldMatrix;
            if (playerASlot != null)
                Gizmos.DrawWireCube(playerASlot.transform.localPosition + Vector3.up, Vector3.one + Vector3.up);
            if (playerBSlot != null)
                Gizmos.DrawWireCube(playerBSlot.transform.localPosition + Vector3.up, Vector3.one + Vector3.up);
            if (cameraSlot != null)
                Gizmos.DrawWireCube(cameraSlot.transform.localPosition, Vector3.one);
        }
    }
}
