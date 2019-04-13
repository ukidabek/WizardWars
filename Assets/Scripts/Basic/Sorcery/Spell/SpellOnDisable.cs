using UnityEngine;
using UnityEngine.Events;

namespace Sorcery.Spells
{
    public class SpellOnDisable : MonoBehaviour
    {
        public UnityEvent OnDisableEvent = new UnityEvent();

        private void OnDisable()
        {
            OnDisableEvent.Invoke();
        }
    }
}
