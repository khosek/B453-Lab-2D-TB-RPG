using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] CombatCharacter character;
    public AnimationCurve healRate;

    private void Start()
    {
        CombatEvents.instance.e_onBeginTurn.AddListener(OnBeginTurn);
    }

    public void OnBeginTurn(CombatCharacter c)
    {
        if (character == c)
        {
            DetermineCombatAction();
        }
    }

    public void DetermineCombatAction()
    {
        float healthPercentage = character.GetHealthPercentage();
        bool wantToHeal = Random.value < healRate.Evaluate(healthPercentage);

        CombatActions ca = null;

        if (wantToHeal && DetermineIfHasCombatActionType(AttackType.Heal))
        {
            ca = GetCombatActionOfType(AttackType.Heal);
        }
        else if (DetermineIfHasCombatActionType(AttackType.Attack))
        {
            ca = GetCombatActionOfType(AttackType.Attack);
        }

        if (ca != null)
        {
            character.CastCombatAction(ca);
        }
        else
        {
            TurnManager.instance.EndTurn();
        }
    }

    private bool DetermineIfHasCombatActionType(AttackType type)
    {
        return character.combatActions.Exists(x => x.ActionType == type);
    }

    private CombatActions GetCombatActionOfType(AttackType type)
    {
        List<CombatActions> availableActions = character.combatActions.FindAll(x => x.ActionType == type);

        return availableActions[Random.Range(0, availableActions.Count)];
    }
}
