using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Tooltip("The CombatCharacter associated with this EnemyAI")]
    [SerializeField] CombatCharacter character;
    [Tooltip("An animation curve that will be used for deciding when to use the healing action.")]
    public AnimationCurve healRate;

    private void Start()
    {
        // Start listening for the onBeginTurn event and call OnBeginTurn() in response.
        CombatEvents.instance.e_onBeginTurn.AddListener(OnBeginTurn);
    }

    // Method to check to see if it's this Enemy's turn, and then determine what combat action to take.
    public void OnBeginTurn(CombatCharacter c)
    {
        // Check to see if it's now this enemy's turn...
        if (character == c)
        {
            // If it is their turn, call DetermineCombatAction() to choose what to do.
            DetermineCombatAction();
        }
    }

    // Method to determine what action this AI enemy should take on their turn.
    public void DetermineCombatAction()
    {
        // Grab the percentage of current HP to max hp and store it.
        float healthPercentage = character.GetHealthPercentage();
        // Generate a random float between 0 and 1 and check to see if it's less than the value of the healRate curve at the percentage specified.
        // If the random value is less than the curve point percentage, set wantToHeal to true.
        bool wantToHeal = Random.value < healRate.Evaluate(healthPercentage);

        // Create CombatAction reference.
        CombatActions ca = null;

        // First check to see if this AI enemy wants to heal and make sure they have a heal action to use.
        if (wantToHeal && DetermineIfHasCombatActionType(AttackType.Heal))
        {
            // Set the CombatActions to be a heal.
            ca = GetCombatActionOfType(AttackType.Heal);
        }
        // Otherwise, check to see if there's an attack action they can use.
        else if (DetermineIfHasCombatActionType(AttackType.Attack))
        {
            // Set the CombatActions to be an attack.
            ca = GetCombatActionOfType(AttackType.Attack);
        }

        // Make sure the Combat Actions isn't null...
        if (ca != null)
        {
            // Cast the action which will be either a heal or attack.
            character.CastCombatAction(ca);
        }
        // Otherwise if their action is null...
        else
        {
            // End this Enemy AI's turn.
            TurnManager.instance.EndTurn();
        }
    }

    // Method to check to see if a certain type of Combat Action exists for this Enemy AI.
    private bool DetermineIfHasCombatActionType(AttackType type)
    {
        // Check to see if there is a combat action of the matching type passed in.
        // This uses the Exists() method from the C# List class to determine if the list has a specific thing.
        // The code that's being passed as an argument to Exists is a lambda expression.
        // It is basically saying, for each thing x in combatActions, if the ActionType of x matches the passed in type, return true.
        // If it goes through all the items and a matching action type isn't found, return false.
        return character.combatActions.Exists(x => x.ActionType == type);
    }

    // Method to return a CombatActions (so a single action).
    private CombatActions GetCombatActionOfType(AttackType type)
    {
        // Store a list of all the available combat actions this Enemy AI has.
        // Again we see a lambda expression. We're using the FindAll() method that's built into C# to go through each thing in a list.
        // Then we're using the lambda to say: for each x in combatActions, if it matches the type passed in, add it to the list.
        List<CombatActions> availableActions = character.combatActions.FindAll(x => x.ActionType == type);

        // Return a single random action to use from the list of all actions that match the passed in type.
        return availableActions[Random.Range(0, availableActions.Count)];
    }
}