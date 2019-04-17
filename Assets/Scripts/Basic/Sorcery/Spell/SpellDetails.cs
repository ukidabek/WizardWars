using UnityEngine;

namespace Sorcery.Spells
{
    public abstract class SpellDetails : ScriptableObject
    {
        public static implicit operator string (SpellDetails type)
        {
            return type.name;
        }
    }
}
