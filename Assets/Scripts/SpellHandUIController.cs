using Sorcery.Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class SelectSpellEvent : UnityEvent<int> { }

public class SpellHandUIController : MonoBehaviour
{
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
