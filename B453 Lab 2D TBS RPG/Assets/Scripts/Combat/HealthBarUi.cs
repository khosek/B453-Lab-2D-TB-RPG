using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBarUi : MonoBehaviour
{
    [Tooltip("The character related to this health bar.")]
    [SerializeField] CombatCharacter character;
    [Tooltip("The text on this health bar.")]
    [SerializeField] TMP_Text healthText;
    [Tooltip("The slider for this health bar.")]
    [SerializeField] Slider healthSlider;

    private void Start()
    {
        // Start listening for the onHealthChange event, and call OnHealthUpdate() in response to the event being detected.
        CombatEvents.instance.e_onHealthChange.AddListener(OnHealthUpdate);
        // Set the max value of the slider to match the character's max hp.
        healthSlider.maxValue = character.maxHp;
        // Set the current slider value to match the character's current health.
        healthSlider.value = character.curHp;
        // Set the text to display current hitpoints and max hitpoints.
        healthText.text = character.curHp + " / " + character.maxHp;
    }

    // Method to update the text and the value of the healthbar.
    public void OnHealthUpdate()
    {
        // Update the text to show the new current hp and max.
        healthText.text = character.curHp + " / " + character.maxHp;
        // Update the slider to represent the new current hp.
        healthSlider.value = character.curHp;
    }
}