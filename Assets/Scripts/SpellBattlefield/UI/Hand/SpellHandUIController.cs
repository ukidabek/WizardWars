using PlayerLogic;
using Sorcery.Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class SelectSpellEvent : UnityEvent<int> { }

public class SpellHandUIController : PlayerObserwator
{
    [SerializeField] private SpellButtonController[] buttonControllers = null;

    [Space] public SelectSpellEvent SelectSpellEvent = new SelectSpellEvent();

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < buttonControllers.Length; i++)
        {
            int index = i;
            buttonControllers[i].ButtonClickedEvent.RemoveAllListeners();
            buttonControllers[i].ButtonClickedEvent.AddListener(() =>
            {
                Debug.LogFormat("Spell button index of {0} clicked.", index);
                SelectSpellEvent.Invoke(index);
            });
        }
    }

    public void OnSpellCasetStart(int i)
    {
        buttonControllers[i].Interactable = false;
    }

    public void OnSpellCasetEnd(int i)
    {
        buttonControllers[i].Interactable = true;
    }

    public void UpdateSpells(List<Spell> spells)
    {
        for (int i = 0; i < buttonControllers.Length; i++)
            buttonControllers[i].Spell = spells[i];
    }

    public void UpdateButtonCooldown(int index, float value)
    {
        buttonControllers[index].Cooldown = value;
    }

    protected override void StartObserwation(Player player)
    {
        if (player != null)
        {
            player.Hand.UpdateSpellHand.AddListener(UpdateSpells);
            player.CastStart.AddListener(OnSpellCasetStart);
            player.CastEnd.AddListener(OnSpellCasetEnd);
            player.CooldownUpdateEvent.AddListener(UpdateButtonCooldown);

            var invoker = player.GetComponentInChildren<CastSpellCommandInvoker>();
            SelectSpellEvent.AddListener((int i) => { invoker.Index = i; });
        }
    }

    protected override void EndObserwation(Player player)
    {
        if (player != null)
        {
            player.Hand.UpdateSpellHand.RemoveListener(UpdateSpells);
            player.CastStart.RemoveListener(OnSpellCasetStart);
            player.CastEnd.RemoveListener(OnSpellCasetEnd);
            player.CooldownUpdateEvent.RemoveListener(UpdateButtonCooldown);
            SelectSpellEvent.RemoveAllListeners();
        }
    }
}
