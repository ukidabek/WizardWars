using Sorcery.Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellButtonController : MonoBehaviour
{
    public Action SpellSelectedCallback = null;

    [SerializeField] private Spell spell = null;
    public Spell Spell
    {
        get => spell;
        set
        {
            spell = value;
            if (spell)
            {
                if (button != null)
                    button.image.sprite = spell.Icon;
            }
        }
    }

    [SerializeField] private Button button = null;
    public Button.ButtonClickedEvent ButtonClickedEvent { get => button.onClick; }
    [SerializeField] private Image coolDownInage = null;
    public float Cooldown
    {
        get => coolDownInage.fillAmount;
        set => coolDownInage.fillAmount = value;
    }

    private void Awake()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SpellSelectedCallback?.Invoke());
    }
}
