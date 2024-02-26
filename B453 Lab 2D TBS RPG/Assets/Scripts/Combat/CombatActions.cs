using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: This class is for scriptable objects. If you don't fully understand what these are, please look them up in the Unity documentation.
// Creating a new item in the menu so we can create new CombatActions from within the Create menu in the Editor.
[CreateAssetMenu(fileName = "Combat Action", menuName = "New Combat Action")]
public class CombatActions : ScriptableObject
{
    [Header("Information")]
    [Tooltip("The name of this Combat Action.")]
    public string DisplayName;
    [Tooltip("The type of attack of this Combat Action.")]
    public AttackType ActionType;

    [Header("Damage")]
    [Tooltip("How much damage this attack will do.")]
    public int Damage;
    [Tooltip("The prefab to use if this is a projectile attack.")]
    public GameObject ProjectilePrefab;

    [Header("Heal")]
    [Tooltip("How much hp to regain if this is a heal action.")]
    public int HealAmount;
}