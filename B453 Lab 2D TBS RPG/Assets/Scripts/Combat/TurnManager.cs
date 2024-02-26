using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [Tooltip("Array of the characters involved in this battle.")]
    [SerializeField] private CombatCharacter[] characters;
    [Tooltip("The delay in seconds between each turn.")]
    [SerializeField] private float nextTurnDelay = 1.0f;

    // The index of the current character who is taking their turn.
    private int curCharacterIndex = -1;
    [Tooltip("The current character who is taking their turn.")]
    public CombatCharacter currentCharacter;

    // A public singleton instance of the TurnManager.
    public static TurnManager instance;

    private void Awake()
    {
        // If instance isn't null or isn't this...
        if (instance != null && instance != this)
        {
            // Destroy any instance of this class if it's not what's stored in the static instance reference.
            Destroy(gameObject);
        }
        // If the instance was null or is already this instance...
        else
        {
            // Set the static instance reference to be this instance of TurnManager.
            instance = this;
        }
    }

    private void Start()
    {
        // Call OnBeginTurn as soon as the TurnManager comes alive.
        OnBeginTurn();
    }

    // Method to being a character taking a turn.
    public void OnBeginTurn()
    {
        // Increase the character index by one.
        curCharacterIndex++;

        // If the last character in this battle is selected...
        if (curCharacterIndex == characters.Length)
        {
            // Switch back to the first character.
            curCharacterIndex = 0;
        }

        // Set the currentCharacter to match the one in the array based on the index.
        currentCharacter = characters[curCharacterIndex];
        // Invoke the onBeginTurn event.
        CombatEvents.instance.e_onBeginTurn.Invoke(currentCharacter);
    }

    // Method to end one character's turn.
    public void EndTurn()
    {
        // Invoke the onEndTurn event.
        CombatEvents.instance.e_onEndTurn.Invoke(currentCharacter);

        // This is something maybe some of you haven't seen before.
        // This will call a certain method after a certain amount of time in seconds.
        // So here, we're going to continue with the next character's turn after the seconds set in nextTurnDelay.
        Invoke(nameof(OnBeginTurn), nextTurnDelay);
    }

    // Method to do something once a character dies.
    private void OnCharacterDie(CombatCharacter character)
    {
        Debug.Log("Character Died");
    }
}