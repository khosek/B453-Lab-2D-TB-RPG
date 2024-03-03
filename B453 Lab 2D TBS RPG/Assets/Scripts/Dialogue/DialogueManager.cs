using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    [Header("Current Conversation")]
    [Tooltip("The currently loaded conversation.")]
    public Conversation currentConversation;
    [Tooltip("The current index for the loaded conversation.")]
    public int currentConversationIndex;
    [Tooltip("How many seconds to wait between displaying each letter in the conversation.")]
    public float letterDisplayDelay;
    private string currentLine; // The current line of conversation.

    [Header("UI References")]
    [Tooltip("The name text for this NPC.")]
    public TMP_Text npcName;
    [Tooltip("The text for this conversation.")]
    public TMP_Text dialogText;
    [Tooltip("The container box for the dialog.")]
    public GameObject dialogBox;

    [Header("Conversation File Path")]
    [Tooltip("The folder in the game directory where the dialog is stored.")]
    [SerializeField] string dialogFilePath = "Dialog/";

    private void Start()
    {
        // Start listening for the e_startDialog event and call IntakeConversationAndStart in response.
        GameEvents.instance.e_startDialog.AddListener(IntakeConversationAndStart);
    }

    // Begin a new conversation.
    public void IntakeConversationAndStart(string path)
    {
        // Load in the conversation from the file path and store it here.
        currentConversation = ConvertJSONToConversation(currentConversation, path);

        // Grab the name from the conversation and display it.
        npcName.text = currentConversation.name;
        // Activate the dialog box so it becomes visible.
        dialogBox.SetActive(true);
        // Starts printing out the dialog letter by letter, line by line.
        StartCoroutine(PrintDialog(currentConversation.lines));

    }

    // Prints one letter at a time, line by line.
    private IEnumerator PrintDialog(string[] lines)
    {
        // Cycle through each line in the array.
        foreach(string line in lines)
        {
            // Reset the line to be blank.
            currentLine = "";
            // Cycle through each letter (character) in the string.
            foreach(char letter in line)
            {
                // Add a single letter to the text.
                currentLine += letter;
                // Display the text.
                dialogText.text = currentLine;
                // Wait for the specified amount of time in seconds.
                yield return new WaitForSeconds(letterDisplayDelay);
            }

            // Wait here until the player presses E which will advance the conversation to the next line.
            while (!Input.GetKeyDown(KeyCode.E))
            {
                // Wait until the next frame then continue from here.
                yield return null;
            }

            // Wait until the next frame and then continue from here.
            yield return null;
        }

        // Hide the dialog box.
        dialogBox.SetActive(false);
        // Fire off the e_onDialogEnd event.
        GameEvents.instance.e_onDialogEnd.Invoke();
    }

    // Convert the JSON file stored on disk back to a Conversation.
    public Conversation ConvertJSONToConversation(Conversation conversation, string jsonFilePath)
    {
        // First combine the folder name with the file name of the conversation we want.
        string filePath = Path.Combine(dialogFilePath, jsonFilePath);

        // First check to see if this file even exists at the given path.
        if (File.Exists(filePath))
        {
            // Pull out all the content of the JSON file and store it as a string.
            string jsonData = File.ReadAllText(filePath);
            // Convert the JSON data to a ConversationData.
            ConversationData dialogData = JsonUtility.FromJson<ConversationData>(jsonData);

            // Pull out the lines and set them.
            conversation.lines = dialogData.lines;
            // Pull out the NPC name and set it.
            conversation.npcName = dialogData.npcName;

            // Return the now converted conversation.
            return conversation;
        }
        else
        {
            Debug.LogError($"Dialog JSON is not found at: {filePath}");
        }

        return null;
    }
}
