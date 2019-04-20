using System.Collections;
using System.Collections.Generic;
using PlayerLogic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class CastBarController : PlayerObserwator
{
    [SerializeField] private Slider slider = null;

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    private void UpdateSlider(float value)
    {
        slider.value = value;
        if (value >= 1f)
            gameObject.SetActive(false);
        else if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }

    protected override void EndObserwation(Player player)
    {
        if (player != null)
            player.CastUpdateEvent.RemoveListener(UpdateSlider);
    }

    protected override void StartObserwation(Player player)
    {
        if (player != null)
            player.CastUpdateEvent.AddListener(UpdateSlider);
    }

    private void Reset()
    {
        slider = GetComponent<Slider>();
    }
}
