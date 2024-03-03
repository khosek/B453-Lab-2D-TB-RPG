using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a menu option for creating a new Conversation prefab.
[CreateAssetMenu(fileName = "Create Conversation", menuName = "New Conversation")]
public class Conversation : ScriptableObject
{
    [Header("Conversation Content")]
    [Tooltip("The array that stores all lines of a conversation.")]
    public string[] lines;

    [Header("NPC Details")]
    [Tooltip("The name of the NPC who has this conversation.")]
    public string npcName;
}

[System.Serializable]
public class ConversationData
{
    // Array to store each line of the dialogue for a specific conversation.
    public string[] lines;
    // The name of the NPC that has this conversation.
    public string npcName;
}
