using Sorcery;
using Sorcery.Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerLogic
{
    [Serializable] public class UpdateSpellHand : UnityEvent<List<Spell>> { }
    [Serializable] public class CastUpdateEvent : UnityEvent<float> { }
    [Serializable] public class CastNotyfication : UnityEvent<int> { }
    [Serializable] public class CooldownUpdateEvent : UnityEvent<int, float> { }
    [Serializable] public class PlayerInfoUpdated : UnityEvent<Player.PlayerInfo> { }

    [RequireComponent(typeof(SpellBook), typeof(Hand))]
    public class Player : MonoBehaviour
    {
        public enum ID { A, B };
        [Serializable]
        public class PlayerInfo
        {
            public string Name = string.Empty;
            public string AvatarID = string.Empty;
        }

        public static event Action<Player> OnPlayerAdded = null;
        private static Dictionary<ID, Player> players = new Dictionary<ID, Player>();
        public static Player Get(ID name)
        {
            Player player = null;
            players.TryGetValue(name, out player);
            return player;
        }

        [SerializeField] private ID id = ID.A;
        public ID Id { get => id; }

        [SerializeField] PlayerInfo info = new PlayerInfo();

        [Space]
        [SerializeField] private SpellBook spellBook = null;
        public SpellBook SpellBook { get => spellBook; }
        [SerializeField] private Hand hand = null;
        public Hand Hand { get => hand; }

        [Space]
        public CastNotyfication CastStart = new CastNotyfication();
        public CastUpdateEvent CastUpdateEvent = new CastUpdateEvent();
        public CastNotyfication CastEnd = new CastNotyfication();
        public CooldownUpdateEvent CooldownUpdateEvent = new CooldownUpdateEvent();
        public PlayerInfoUpdated PlayerInfoUpdated = new PlayerInfoUpdated();

        private void OnDestroy()
        {
            if (players.ContainsKey(id))
                players.Remove(id);
        }

        protected void Start()
        {
            players.Add(id, this);
            OnPlayerAdded?.Invoke(this);

            hand.Spells = spellBook.GetHand();
            PlayerInfoUpdated.Invoke(info);
        }

        protected void Reset()
        {
            spellBook = GetComponentInChildren<SpellBook>();
            hand = GetComponentInChildren<Hand>();
        }

        private IEnumerator Cast(Spell spell, Vector3 positon, Quaternion rotation, int index)
        {
            CastStart.Invoke(index);
            var time = spell.CastTime;
            while ((time -= Time.deltaTime) > 0)
            {
                CastUpdateEvent.Invoke(1 - (time / spell.CastTime));
                yield return null;
            }
            var spellInstance = spell.SpellObject;
            spellInstance.transform.position = positon;
            spellInstance.transform.rotation = rotation;
            spellInstance.gameObject.SetActive(true);
            CastEnd.Invoke(index);
            StartCoroutine(Cooldown(spell, index));
        }

        private IEnumerator Cooldown(Spell spell, int index)
        {
            var time = spell.CooldwonTime;
            while ((time -= Time.deltaTime) > 0)
            {
                CooldownUpdateEvent.Invoke(index, (time / spell.CooldwonTime));
                yield return null;
            }
            CooldownUpdateEvent.Invoke(index, 0);
            spellBook.ReturnSpell(hand[index]);
            hand[index] = spellBook.GetSpell();
        }

        public void CastSpell(int index, Vector3 positon, Quaternion rotation)
        {
            StartCoroutine(Cast(hand[index], positon, rotation, index));
        }
    }
}