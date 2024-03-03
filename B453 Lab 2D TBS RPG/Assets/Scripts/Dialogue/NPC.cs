using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IConversational
{
    [Header("Conversation Details")]
    [Tooltip("The file path for where the dialog is stored.")]
    [SerializeField] string conversationDataFilePath;
    [Tooltip("Whether this NPC is currently engaged in a conversation or not.")]
    public bool isInConversation = false;

    private void Start()
    {
        // Listen for the e_onDialogEnd event to be fired and respond by calling ResetDialog.
        GameEvents.instance.e_onDialogEnd.AddListener(ResetDialog);
    }

    // Starts a conversation with this NPC.
    public void StartConversation()
    {
        // First check to make sure they're not already in a conversation...
        if (!isInConversation)
        {
            // Set isInConversation to true so a second conversation can't be triggered before the current one ends.
            isInConversation = true;
            // Fire off the e_startDialog trigger and pass in the file path for this specific conversation.
            GameEvents.instance.e_startDialog.Invoke(conversationDataFilePath);
        }
        else
        {
            return;
        }
    }

    // Resets the dialog after their conversation is finished so they can have another one.
    public void ResetDialog()
    {
        isInConversation = false;
    }
}
