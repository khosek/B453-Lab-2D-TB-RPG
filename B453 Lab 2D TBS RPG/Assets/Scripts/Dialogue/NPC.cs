using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IConversational
{
    [Header("Conversation Details")]
    [SerializeField] string conversationDataFilePath;
    public bool isInConversation = false;

    private void Start()
    {
        GameEvents.instance.e_onDialogEnd.AddListener(ResetDialog);
    }

    public void StartConversation()
    {
        if (!isInConversation)
        {
            isInConversation = true;
            GameEvents.instance.e_startDialog.Invoke(conversationDataFilePath);
        }
        else
        {
            return;
        }
    }

    public void ResetDialog()
    {
        isInConversation = false;
    }
}
