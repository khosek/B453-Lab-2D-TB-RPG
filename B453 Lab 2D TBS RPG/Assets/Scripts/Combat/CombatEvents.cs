using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatEvents : MonoBehaviour
{
    public static CombatEvents instance;

    public UnityEvent<CombatCharacter> e_onBeginTurn;
    public UnityEvent<CombatCharacter> e_onEndTurn;
    public UnityEvent<CombatCharacter> e_onCharacterDie;
    public UnityEvent e_onHealthChange;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
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
