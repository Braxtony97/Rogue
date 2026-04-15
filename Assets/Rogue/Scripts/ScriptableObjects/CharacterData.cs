using System.Collections.Generic;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Data/Character")]
public class CharacterData : ScriptableObject
{
    public string Name;
    public CharacterNameId CharacterNameId;
    public Sprite Avatar;
    public Sprite FullPortrait;
    public int MaxHealth;
    public int MaxArmor;
    public List<AbilityData> Abilities;
    public List<ModifierData> Modifiers;
}
