using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharCreationManager : MonoBehaviour
{
    [Header("Character Data")]
    [Tooltip("Array of sprites to display in the center of the screen.")]
    [SerializeField] Sprite[] classSprites;
    [Tooltip("The Image displayed in the center of the UI to display different character types.")]
    [SerializeField] Image characterImage;
    [Tooltip("The class the player currently has selected.")]
    [SerializeField] TextMeshProUGUI classText;
    [Tooltip("The different stats to display.")]
    [SerializeField] TextMeshProUGUI[] statText;
    [Tooltip("Field for the player to input their desired name.")]
    [SerializeField] TMP_InputField nameInput;
    [Tooltip("The description of the class chosen.")]
    [SerializeField] TextMeshProUGUI classDescription;

    // The class that has been chosen.
    private CharacterClasses chosenClass;
    // Array of integers for storing ability score rolls.
    private int[] rolledScores = new int[6];
    // Data structure to store the ability score results.
    private AbilityScores myAbilityScores;
    [Tooltip("Prefab to be used based on chosen class.")]
    [SerializeField] GameObject classPrefab;
    // Unused.
    private int classPrefabIndex = 0;

    // Swap text and sprites based on new class chosen. The chosen class name is passed as a string from different buttons being pressed in their respective OnClick() method in the Inspector.
    // NOTE: You can choose which method to call from what class inside the Inspector for buttons using the OnClick() event. You can also choose which arguments to pass when the method is called.
    public void SwapClass(string className)
    {
        // First check to see if the passed name is a specific class.
        if (className == "Paladin")
        {
            // Update the image of the character in the middle of the screen to reflect the chosen class.
            characterImage.sprite = classSprites[0];
            // Update the chosen class to match the passed class name.
            chosenClass = CharacterClasses.Paladin;
            // Set the prefab index (not used).
            classPrefabIndex = 0;
            // Update the description text to reflect the chosen class description.
            classDescription.text = "Fuelled by the Oath you swore to uphold justice and righteousness, you are a beacon of hope in dark times.";
        }
        else if (className == "Cleric")
        {
            characterImage.sprite = classSprites[1];
            chosenClass = CharacterClasses.Cleric;
            classPrefabIndex = 1;
            classDescription.text = "Clerics are representatives of the gods they worship, wielding potent divine magic for good or ill.";
        }
        else if (className == "Mage")
        {
            characterImage.sprite = classSprites[2];
            chosenClass = CharacterClasses.Mage;
            classPrefabIndex = 2;
            classDescription.text = "Mages master the arcane by specialising in individual schools of magic, combining ancient spells with modern research.";
        }
        else if (className == "Thief")
        {
            characterImage.sprite = classSprites[3];
            chosenClass = CharacterClasses.Thief;
            classPrefabIndex = 3;
            classDescription.text = "With stealth, skill, and uncanny reflexes, a thief's versatility lets them get the upper hand in almost any situation.";
        }

        // Update the classText to display level 1 and the name of the chosen class.
        classText.text = "Level 1 " + className;
    }

    // Create the new character class, pass it to the GameManager and start the game.
    public void ConfirmCharacterCreation()
    {
        // Store the name the player typed into the input field.
        string chosenName = nameInput.text;

        // Grab all the randomly rolled scores for each ability and update the values for that respective ability inside the myAbilityScores object.
        myAbilityScores.Strength = rolledScores[0];
        myAbilityScores.Dexterity = rolledScores[1];
        myAbilityScores.Constitution = rolledScores[2];
        myAbilityScores.Intelligence = rolledScores[3];
        myAbilityScores.Wisdom = rolledScores[4];
        myAbilityScores.Charisma = rolledScores[5];

        // Create a new Character object using the chosen name, class, rolled ability scores, the correct prefab based on chosen class, and with the type of Human player.
        Character newCharacter = new Character(chosenName, chosenClass, myAbilityScores, classPrefab, CharacterTypes.Human);

        // Load up the Overworld scene and pass in this new Character along with the image for this class.
        GameManager.Instance.EnterOverworld(newCharacter, characterImage.sprite);
    }

    // Randomly roll a D12 for player stats.
    public void RollStats()
    {
        // For loop runs one time for each ability.
        for (int i = 0; i < statText.Length; i++)
        {
            // Randomly selects a number between 1 and 12 (13 is not included). This simulates rolling two six sided dice (2D6 for us D&D and/or Warhammer 40K players).
            int roll = Random.Range(1, 13);
            // Update the text for the score just rolled to reflect the result of the roll.
            statText[i].text = roll.ToString();
            // Store the result of the roll in the rolledScores array, used for later updating the myAbilitScores object with correct values.
            rolledScores[i] = roll;
        }
    }
}