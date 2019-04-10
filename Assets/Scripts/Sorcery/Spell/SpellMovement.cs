using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sorcery.Spells
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpellMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody controlledRigidbody = null;
        [SerializeField] private float speed = 3f;

        private void OnEnable()
        {
            controlledRigidbody.isKinematic = false;
            controlledRigidbody.velocity = Vector3.forward * speed;
        }

        private void OnDisable()
        {
            controlledRigidbody.velocity = Vector3.zero;
            controlledRigidbody.isKinematic = true;
        }

        private void Reset()
        {
            controlledRigidbody = GetComponent<Rigidbody>();
        }

        private void OnValidate()
        {
            controlledRigidbody = GetComponent<Rigidbody>();
        }
    }
}