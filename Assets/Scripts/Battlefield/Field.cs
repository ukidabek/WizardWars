using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{
    public class Field : MonoBehaviour
    {
        public event Action<Vector2Int> OnMouseDownCallback = null;

        [SerializeField] private MeshRenderer meshRenderer = null;
        [SerializeField] private Transform fieldTransfomr = null;
        public Transform FieldTransfomr
        {
            get => fieldTransfomr == null ? transform : fieldTransfomr;
            set => fieldTransfomr = value;
        }

        private void Start()
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        private void OnMouseDown()
        {
            Vector2Int vector = Vector2Int.zero;
            vector.y = (int)transform.localPosition.z;
            vector.x = (int)transform.parent.localPosition.x;
            Debug.Log(vector.ToString());
            OnMouseDownCallback?.Invoke(vector);
        }

        private void OnMouseEnter()
        {
            meshRenderer.material.color = Color.red;
        }

        private void OnMouseExit()
        {
            meshRenderer.material.color = Color.white;
        }
    }
}
