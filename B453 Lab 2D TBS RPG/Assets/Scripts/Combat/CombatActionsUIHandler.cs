using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatActionsUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject visualContainer;
    [SerializeField] private Button[] combatActionButtons;

    private void Start()
    {
        CombatEvents.instance.e_onBeginTurn.AddListener(OnBeginTurn);
        CombatEvents.instance.e_onEndTurn.AddListener(OnEndTurn);
    }

    public void OnBeginTurn(CombatCharacter character)
    {
        Debug.Log("Combat Actions UI Handler is Called");

        if (!character.isPlayer)
        {
            return;
        }


        visualContainer.SetActive(true);

        for (int i = 0; i < combatActionButtons.Length; i++)
        {
            if (i < character.combatActions.Count)
            {
                combatActionButtons[i].gameObject.SetActive(true);
                CombatActions ca = character.combatActions[i];

                combatActionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = ca.DisplayName;
                combatActionButtons[i].onClick.RemoveAllListeners();
                combatActionButtons[i].onClick.AddListener(() => OnClickCombatAction(ca));
            }
            else
            {
                combatActionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnEndTurn(CombatCharacter character)
    {
        visualContainer.SetActive(false);
    }

    public void OnClickCombatAction(CombatActions combatAction)
    {
        TurnManager.instance.currentCharacter.CastCombatAction(combatAction);
    }
}
