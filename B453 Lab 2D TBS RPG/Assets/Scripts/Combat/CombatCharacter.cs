using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCharacter : MonoBehaviour
{
    [Tooltip("Is this a player or not?")]
    public bool isPlayer;
    [Tooltip("A list of the combat actions available to use.")]
    public List<CombatActions> combatActions;

    [Tooltip("The current hp of this character.")]
    public int curHp;
    [Tooltip("The maximum hp of this character.")]
    public int maxHp;

    [Tooltip("The opponent of this character.")]
    [SerializeField] private CombatCharacter opponent;
    private Vector3 startPos;

    [SerializeField] int xp;

    private void Start()
    {
        // Saves the current position the character is spawned in at.
        startPos = transform.position;
    }

    // Method for sustaining damage.
    public void TakeDamage(int damageToTake)
    {
        Debug.Log("Damage to take: " + damageToTake);

        // Subtract the damage from the current hp.
        curHp -= damageToTake;

        // Invoke the onHealthChange event.
        // RECALL: Events are like shouting out into space that something happened. Only classes listening for this event will do something in response.
        // This is a great way to decouple code and keep things separate.
        CombatEvents.instance.e_onHealthChange.Invoke();

        // Check to see if the current health is 0 or less.
        if (curHp <= 0)
        {
            // Die if hp is 0 or less.
            Die();
        }
    }

    // Method to kill this character.
    private void Die()
    {
        // Invoke the onCharacterDie event.
        CombatEvents.instance.e_onCharacterDie.Invoke(this);
        GameManager.Instance.ReturnOverworld(xp);
        // Destroy this character.
        Destroy(gameObject);
    }

    // Method to regain lost hitpoints.
    public void Heal(int healAmount)
    {
        // Add the healAmount to the current hitpoints.
        curHp += healAmount;

        // Invoke the onHealthChange event.
        CombatEvents.instance.e_onHealthChange.Invoke();

        // Check to see if the current health exceeds max health.
        if (curHp > maxHp)
        {
            // Set current health to max health.
            curHp = maxHp;
        }
    }

    // Method to use a Combat Action.
    public void CastCombatAction(CombatActions combatAction)
    {
        // Check to see if the Combat Action has a positive damage amount.
        if (combatAction.Damage > 0)
        {
            // Start the attack coroutine using the cast Combat Action.
            StartCoroutine(AttackOpponent(combatAction));
        }
        // Check to see instead of there exists a projectile prefab for this Combat Action.
        else if (combatAction.ProjectilePrefab != null)
        {
            // Instantiate the projectile.
            GameObject proj = Instantiate(combatAction.ProjectilePrefab, transform.position, Quaternion.identity);
            proj.GetComponent<Projectile>().SetTarget(opponent);
            //proj.GetComponent<Projectile>().Initialize(opponent, TurnManager.instance.EndTurn);
        }
        // Check to see instead if the Combat Action has a positive heal amount.
        else if (combatAction.HealAmount > 0)
        {
            // Call the Heal method and pass the heal amount.
            Heal(combatAction.HealAmount);
            // End this character's turn.
            TurnManager.instance.EndTurn();
        }
        else
        {
            // End this character's turn.
            TurnManager.instance.EndTurn();
        }
    }

    // Coroutine for Attacking another character.
    IEnumerator AttackOpponent(CombatActions combatAction)
    {
        // As long as this character is not at the opponent's position...
        while (transform.position != opponent.transform.position)
        {
            // Move this character towards the opponent at a rate of 50 meters per second multiplied by the time between this frame and the last.
            transform.position = Vector3.MoveTowards(transform.position, opponent.transform.position, 50 * Time.deltaTime);
            // When coroutines return something, it pauses the execution of the coroutine until a specific condition is met.
            // Returning null in a coroutine pauses the coroutine and waits for the next frame, when the next frame comes, it resumes.
            yield return null;
        }

        // Deal the damage to the opponent (remember this only happens once the attacking character has moved to the opponents location).
        opponent.TakeDamage(combatAction.Damage);

        // As long as this character is not at their original spawn location...
        while (transform.position != startPos)
        {
            // Move the character towards their starting position. Note they move back slower than they rushed forward to attack.
            transform.position = Vector3.MoveTowards(transform.position, startPos, 20 * Time.deltaTime);
            // Pause the coroutine until the next frame, and then continue it.
            yield return null;
        }

        // End this character's turn.
        TurnManager.instance.EndTurn();
    }

    // Returns the percentage of remaining health the character has in relation to their max.
    public float GetHealthPercentage()
    {
        // Return the current health divided by the max health and force it to be a float.
        return (float)curHp / maxHp;
    }
}