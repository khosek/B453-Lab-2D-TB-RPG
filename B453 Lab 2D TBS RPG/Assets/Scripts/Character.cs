using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string characterName;
    public CharacterClasses characterClass;
    public CharacterTypes characterType;
    public AbilityScores score;
    public GameObject classPrefab;

    public Character(string characterName, CharacterClasses characterClass, AbilityScores abs, GameObject classPrefab, CharacterTypes characterType)
    {
        this.characterName = characterName;
        this.characterClass = characterClass;
        this.score = abs;
        this.classPrefab = classPrefab;
        this.characterType = characterType;
    }

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
}
