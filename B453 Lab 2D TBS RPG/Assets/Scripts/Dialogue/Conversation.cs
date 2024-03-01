using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Conversation", menuName = "New Conversation")]
public class Conversation : ScriptableObject
{
    [Header("Conversation Content")]
    public string[] lines;

    [Header("NPC Details")]
    public string npcName;
}

[System.Serializable]
public class ConversationData
{
    public string[] lines;
    public string npcName;
}
