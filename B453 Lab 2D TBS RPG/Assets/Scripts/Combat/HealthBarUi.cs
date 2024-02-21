using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBarUi : MonoBehaviour
{
    [SerializeField] CombatCharacter character;
    [SerializeField] TMP_Text healthText;
    [SerializeField] Slider healthSlider;

    private void Start()
    {
        CombatEvents.instance.e_onHealthChange.AddListener(OnHealthUpdate);
        healthSlider.maxValue = character.maxHp;
        healthSlider.value = character.curHp;
        healthText.text = character.curHp + " / " + character.maxHp;
    }

    public void OnHealthUpdate()
    {
        healthText.text = character.curHp + " / " + character.maxHp;
        healthSlider.value = character.curHp;
    }
}
