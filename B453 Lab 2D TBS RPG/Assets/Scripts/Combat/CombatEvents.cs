using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatEvents : MonoBehaviour
{
    // Creates a static instance of CombatEvents. Combined with the logic in Awake(), this makes a singleton.
    public static CombatEvents instance;

    // Some events that other classes will be able to invoke or listen for.
    public UnityEvent<CombatCharacter> e_onBeginTurn;
    public UnityEvent<CombatCharacter> e_onEndTurn;
    public UnityEvent<CombatCharacter> e_onCharacterDie;
    public UnityEvent e_onHealthChange;

    private void Awake()
    {
        // Check to see if the instance property has a reference to CombatEvents stored in it and if that instance isn't this instance of the class.
        if (instance != null && instance != this)
        {
            // If the instance isn't null and this instance is not what's stored in instance, destroy this instance of the class.
            Destroy(this);
        }
        else
        {
            // If the instance was null, or it is already this instance of the class, set the instance as this instance of the class.
            instance = this;
        }
    }

    private void Start()
    {
        // Check each event and if it's null, initialize it.
        if (e_onBeginTurn == null)
        {
            e_onBeginTurn = new UnityEvent<CombatCharacter>();
        }

        if (e_onEndTurn == null)
        {
            e_onEndTurn = new UnityEvent<CombatCharacter>();
        }

        if (e_onCharacterDie == null)
        {
            e_onCharacterDie = new UnityEvent<CombatCharacter>();
        }

        if (e_onHealthChange == null)
        {
            e_onHealthChange = new UnityEvent();
        }
    }
}