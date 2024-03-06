using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A struct for all the different ability score values.
public struct AbilityScores
{
    public int Strength; // Physical Power
    public int Dexterity; // Agility
    public int Constitution; // Endurance
    public int Intelligence; // Reasoning and Memory
    public int Wisdom; // Perception and Insight
    public int Charisma; // Force of Personality
}

public class Character
{
    [Header("Stats and Details")]
    [Tooltip("The name of this character.")]
    public string characterName;
    [Tooltip("This character's class.")]
    public CharacterClasses characterClass;
    [Tooltip("This character's type (Human or NPC)")]
    public CharacterTypes characterType;
    [Tooltip("The AbilityScores struct for this Character that stores all their ability scores.")]
    public AbilityScores score;
    [Tooltip("The specific prefab to use for this Character.")]
    public GameObject classPrefab;

    public int maxHp = 20;
    public int curHp = 20;
    int xp = 0;
    int level = 1;

    // Constructor for a new Character.
    public Character(string characterName, CharacterClasses characterClass, AbilityScores abs, GameObject classPrefab, CharacterTypes characterType)
    {
        // Update all stats with the passed in data.
        this.characterName = characterName;
        this.characterClass = characterClass;
        this.score = abs;
        this.classPrefab = classPrefab;
        this.characterType = characterType;
    }

    // Return the value of the score passed in.
    public int GetAbilityScoreBonus(AbilityScoreNames abilityName)
    {
        switch (abilityName)
        {
            case AbilityScoreNames.Strength:
                return CalculateAbilityScoreBonus(score.Strength);
            case AbilityScoreNames.Dexterity:
                return CalculateAbilityScoreBonus(score.Dexterity);
            case AbilityScoreNames.Consitution:
                return CalculateAbilityScoreBonus(score.Constitution);
            case AbilityScoreNames.Intelligence:
                return CalculateAbilityScoreBonus(score.Intelligence);
            case AbilityScoreNames.Wisdom:
                return CalculateAbilityScoreBonus(score.Wisdom);
            case AbilityScoreNames.Charisma:
                return CalculateAbilityScoreBonus(score.Charisma);
            default:
                return 0;
        }
    }

    // Calculate the bonus for the passed in ability score.
    public int CalculateAbilityScoreBonus(int baseValue)
    {
        if (baseValue <= 2)
        {
            return -1;
        }
        else if (baseValue <= 4)
        {
            return 0;
        }
        else if (baseValue <= 6)
        {
            return 1;
        }
        else if (baseValue <= 8)
        {
            return 2;
        }
        else if (baseValue <= 10)
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }

    public void gainXP(int xp)
    {
        this.xp += xp;
        if(this.xp > 10)
        {
            level += this.xp / 10;
            maxHp = level * 20;
            this.xp %= 10;
        }
    }
}