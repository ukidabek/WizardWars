using System;
using UnityEngine;
using UnityEngine.Events;

namespace Sorcery.Spells
{
    [Serializable] public class OnCollisionEnterEvent : UnityEvent<Collision> { }

    public class SpellOnCollisionEnter : MonoBehaviour
    {
        public OnCollisionEnterEvent OnCollisionEnterEvent = new OnCollisionEnterEvent();

        private void OnCollisionEnter(Collision collision)
        {
            string log = string.Format("Object {0} collided with {1}", this.transform.root.name, collision.gameObject.name);
            Debug.Log(log, this);
            if (true) // Conditions
            {
                OnCollisionEnterEvent.Invoke(collision);
            }
        }

    }
}
