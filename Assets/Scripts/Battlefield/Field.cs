using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{
    public class Field : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Vector2 vector = Vector2.zero;
            vector.y = transform.localPosition.z;
            vector.x = transform.parent.localPosition.x;
            Debug.Log(vector);
        }
    }
}
