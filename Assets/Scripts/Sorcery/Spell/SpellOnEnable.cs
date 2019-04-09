using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sorcery.Spells
{
    public class SpellOnEnable : MonoBehaviour
    {
        public UnityEvent OnEnableEvent = new UnityEvent();

        private void OnEnable()
        {
            OnEnableEvent.Invoke();
        }
    }
}
