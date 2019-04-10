using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    [DisallowMultipleComponent]
    public abstract class CommandInvoker : MonoBehaviour
    {
        protected abstract Command Command { get; }

        public void Invoke()
        {
            CommandManager.Instance.EnqueueCommand(Command);
        }

        private void Reset()
        {
            gameObject.name = GetType().Name;
        }

        private void OnValidate()
        {
            gameObject.name = GetType().Name;
        }
    }
}