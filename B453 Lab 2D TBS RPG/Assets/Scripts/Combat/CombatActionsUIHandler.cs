using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatActionsUIHandler : MonoBehaviour
{
    [Tooltip("The container for the combat buttons.")]
    [SerializeField] private GameObject visualContainer;
    [Tooltip("Array of the buttons for different actions.")]
    [SerializeField] private Button[] combatActionButtons;

    private void Start()
    {
        // Start listening for the onBeginTurn event and call OnBeginTurn() in response.
        CombatEvents.instance.e_onBeginTurn.AddListener(OnBeginTurn);
        // Start listening for the onEndTurn event and call OnEndTurn() in reponse.
        CombatEvents.instance.e_onEndTurn.AddListener(OnEndTurn);
    }

    // Method to add buttons when the turn begins.
    public void OnBeginTurn(CombatCharacter character)
    {
        Debug.Log("Combat Actions UI Handler is Called");

        // If the character is not the player, exit the method without doing anything.
        if (!character.isPlayer)
        {
            // Completely exits and concludes this method instantly.
            return;
        }

        // Set the container for the buttons to be visible.
        visualContainer.SetActive(true);

        // For loop that will execute once for each combat action in the combatActionButtons array.
        for (int i = 0; i < combatActionButtons.Length; i++)
        {
            // Check first to see if the current button index is less than how many combat actions the player has.
            // In other words, if the player only has two actions, only two buttons will be activated.
            if (i < character.combatActions.Count)
            {
                // Activate the button at index i and make it visible.
                combatActionButtons[i].gameObject.SetActive(true);
                // Grab and save a reference to the combat action of the player at index i.
                CombatActions ca = character.combatActions[i];

                // Update the text of button i to match the name of combat action i.
                combatActionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = ca.DisplayName;
                // Remove all onClick listeners from button i.
                combatActionButtons[i].onClick.RemoveAllListeners();
                // Add a listener to button i for OnClickCombatAction. So when the player clicks a button, it will cast that action.
                // This syntax is a lambda expression, if you haven't seen one in C# before.
                combatActionButtons[i].onClick.AddListener(() => OnClickCombatAction(ca));
            }
            // if i is greater than how many combat actions the player has...
            else
            {
                // Make sure the button that's not needed is not displayed.
                combatActionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    // Method to disable buttons for certain characters if it's not their turn.
    public void OnEndTurn(CombatCharacter character)
    {
        // Set the container to be disabled (it will disappear).
        visualContainer.SetActive(false);
    }

    // Method called when a button is clicked on to trigger a certain combat action.
    public void OnClickCombatAction(CombatActions combatAction)
    {
        // Cast the clicked on Combat Action for the character whose turn it currently is.
        TurnManager.instance.currentCharacter.CastCombatAction(combatAction);
    }
}