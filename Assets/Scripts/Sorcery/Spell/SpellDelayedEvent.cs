using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sorcery.Spells
{
    public class SpellDelayedEvent : MonoBehaviour
    {
        [SerializeField] private float time = 1f;
        public UnityEvent DelayedEvent = new UnityEvent();

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(time);
            DelayedEvent.Invoke();
        }

        public void InvokeDelayEvent()
        {
            StartCoroutine(Delay());
        }
    }
}