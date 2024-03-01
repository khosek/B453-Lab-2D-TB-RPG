using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    [Header("Current Conversation")]
    public Conversation currentConversation;
    public int currentConversationIndex;
    public float letterDisplayDelay;
    private string currentLine;

    [Header("UI References")]
    public TMP_Text npcName;
    public TMP_Text dialogText;
    public GameObject dialogBox;

    [Header("Conversation File Path")]
    [SerializeField] string dialogFilePath = "Dialog/";

    private void Start()
    {
        GameEvents.instance.e_startDialog.AddListener(IntakeConversationAndStart);
    }

    public void IntakeConversationAndStart(string path)
    {
        currentConversation = ConvertJSONToConversation(currentConversation, path);

        npcName.text = currentConversation.name;
        dialogBox.SetActive(true);
        StartCoroutine(PrintDialog(currentConversation.lines));

    }

    private IEnumerator PrintDialog(string[] lines)
    {
        foreach(string line in lines)
        {
            currentLine = "";
            foreach(char letter in line)
            {
                currentLine += letter;
                dialogText.text = currentLine;
                yield return new WaitForSeconds(letterDisplayDelay);
            }

            while (!Input.GetKeyDown(KeyCode.E))
            {
                yield return null;
            }

            yield return null;
        }

        dialogBox.SetActive(false);
        GameEvents.instance.e_onDialogEnd.Invoke();
    }

    public Conversation ConvertJSONToConversation(Conversation conversation, string jsonFilePath)
    {
        string filePath = Path.Combine(dialogFilePath, jsonFilePath);

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            ConversationData dialogData = JsonUtility.FromJson<ConversationData>(jsonData);

            conversation.lines = dialogData.lines;
            conversation.npcName = dialogData.npcName;

            return conversation;
        }
        else
        {
            Debug.LogError($"Dialog JSON is not found at: {filePath}");
        }

        return null;
    }
}
