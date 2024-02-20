using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharCreationManager : MonoBehaviour
{
    [SerializeField] Sprite[] classSprites;
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI classText;
    [SerializeField] TextMeshProUGUI[] statText;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] TextMeshProUGUI classDescription;
    private CharacterClasses chosenClass;
    private int[] rolledScores = new int[6];
    private AbilityScores myAbilityScores;
    [SerializeField] GameObject classPrefab;
    private int classPrefabIndex = 0;

    // Swap text and sprites based on new class chosen.
    public void SwapClass(string className)
    {
        if (className == "Paladin")
        {
            characterImage.sprite = classSprites[0];
            chosenClass = CharacterClasses.Paladin;
            classPrefabIndex = 0;
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

        classText.text = "Level 1 " + className;
    }

    // Create the new character class, pass it to the GameManager and start the game.
    public void ConfirmCharacterCreation()
    {
        string chosenName = nameInput.text;

        myAbilityScores.Strength = rolledScores[0];
        myAbilityScores.Dexterity = rolledScores[1];
        myAbilityScores.Constitution = rolledScores[2];
        myAbilityScores.Intelligence = rolledScores[3];
        myAbilityScores.Wisdom = rolledScores[4];
        myAbilityScores.Charisma = rolledScores[5];

        Character newCharacter = new Character(chosenName, chosenClass, myAbilityScores, classPrefab, CharacterTypes.Human);
        GameManager.Instance.EnterOverworld(newCharacter, characterImage.sprite);
    }

    // Randomly roll a D12 for player stats.
    public void RollStats()
    {
        for (int i = 0; i < statText.Length; i++)
        {
            int roll = Random.Range(1, 13);
            statText[i].text = roll.ToString();
            rolledScores[i] = roll;
        }
    }
}