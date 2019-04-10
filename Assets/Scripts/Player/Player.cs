using BaseGameLogic.Singleton;
using Sorcery;
using Sorcery.Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class UpdateSpellHand : UnityEvent<List<Spell>> { }
[Serializable] public class CastUpdateEvent : UnityEvent<float> { }
[Serializable] public class CooldownUpdateEvent : UnityEvent<int, float> { }

[RequireComponent(typeof(SpellBook))]
public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField] private SpellBook spellBook = null;

    [SerializeField] private List<Spell> hand = null;

    [Space]
    public UpdateSpellHand UpdateSpellHand = new UpdateSpellHand();
    public CastUpdateEvent CastUpdateEvent = new CastUpdateEvent();
    public CooldownUpdateEvent CooldownUpdateEvent = new CooldownUpdateEvent();

    protected override void Start()
    {
        base.Start();
        UpdateSpellHand.Invoke(hand = spellBook.GetHand());
    }

    protected override void Reset()
    {
        base.Reset();
        spellBook = GetComponent<SpellBook>();
    }

    private IEnumerator Cast(Spell spell, Vector3 positon, Quaternion rotation, int index)
    {
        var time = spell.CastTime;
        while ((time -= Time.deltaTime) > 0)
        {
            CastUpdateEvent.Invoke(1 - (time / spell.CastTime));
            yield return null;
        }
        Instantiate(spell.SpellObject, positon, rotation);
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
        UpdateSpellHand.Invoke(hand);
    }

    public void CastSpell(int index, Vector3 positon, Quaternion rotation)
    {
        StartCoroutine(Cast(hand[index], positon, rotation, index));
    }
}
