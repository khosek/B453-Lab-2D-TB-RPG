using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An enum of the different classes of character's the player can choose.
public enum CharacterClasses
{
    Paladin,
    Mage,
    Thief,
    Cleric
};

// An enum for the different character types.
public enum CharacterTypes
{
    Human,
    Monster
};

// An enum for all the ability scores that determine player stats and performance.
public enum AbilityScoreNames
{
    Strength,
    Dexterity,
    Consitution,
    Intelligence,
    Wisdom,
    Charisma
};

// An enum for the different types of attacks.
public enum AttackType
{
    Attack,
    Heal
};