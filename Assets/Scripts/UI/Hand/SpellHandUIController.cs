using PlayerLogic;
using Sorcery.Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class SelectSpellEvent : UnityEvent<int> { }


public class SpellHandUIController : MonoBehaviour
{
    [SerializeField] private Player.ID playerID = Player.ID.A;
    [SerializeField] private Player watchedPlayer = null;
    [SerializeField] private SpellButtonController[] buttonControllers = null;

    [Space] public SelectSpellEvent SelectSpellEvent = new SelectSpellEvent();

    private void Awake()
    {
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

        Player.OnPlayerAdded += ConnectToPlayer;
    }

    private void ConnectToPlayer(Player player)
    {
        if (playerID == player.Id)
        {
            watchedPlayer = player;
            watchedPlayer.Hand.UpdateSpellHand.AddListener(UpdateSpells);
            watchedPlayer.CastStart.AddListener(OnSpellCasetStart);
            watchedPlayer.CastEnd.AddListener(OnSpellCasetEnd);
            watchedPlayer.CooldownUpdateEvent.AddListener(UpdateButtonCooldown);

            var invoker = watchedPlayer.GetComponentInChildren<CastSpellCommandInvoker>();
            SelectSpellEvent.AddListener((int i) => { invoker.Index = i; });
        }
    }

    private void OnDestroy()
    {
        if (watchedPlayer != null)
        {
            watchedPlayer.Hand.UpdateSpellHand.RemoveListener(UpdateSpells);
            watchedPlayer.CastStart.RemoveListener(OnSpellCasetStart);
            watchedPlayer.CastEnd.RemoveListener(OnSpellCasetEnd);
            watchedPlayer.CooldownUpdateEvent.RemoveListener(UpdateButtonCooldown);
            SelectSpellEvent.RemoveAllListeners();
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
}
