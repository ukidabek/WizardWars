using UnityEngine;

namespace Battlefield
{
    public abstract class FieldPrefabProvider : MonoBehaviour
    {
        public abstract GameObject Field { get; }
    }
}
