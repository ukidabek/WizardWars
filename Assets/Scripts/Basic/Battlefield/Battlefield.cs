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

        [Space] public FieldSelectedCallback FieldSelectedCallback = new FieldSelectedCallback();
        [SerializeField] private Color gridColor = Color.red;

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
                    fields[x, y] = field.GetComponent<Field>();
                    fields[x, y].OnMouseDownCallback += OnMouseDown;
                    field.transform.SetParent(row.transform);
                    field.transform.localRotation = Quaternion.identity;
                    field.transform.localPosition = new Vector3(0, 0, y);
                }
            }
        }

        private void OnMouseDown(Vector2Int coordinate)
        {
            FieldSelectedCallback.Invoke(fields[coordinate.x, coordinate.y].FieldTransfomr);
        }


        private void OnValidate()
        {
            if(playerASlot != null)
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
                startPosition = transform.position + transform.right * x;
                Gizmos.DrawLine(startPosition, startPosition + transform.forward * size.y);
            }
            for (int y = 0; y <= size.y; y++)
            {
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
