using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Data/Character")]
public class CharacterData : ScriptableObject
{
    public string Name;
    public Enums.CharacterName CharacterNameId;
    public Sprite Avatar;
    public Sprite FullPortrait;
    public int MaxHealth;
    public int MaxArmor;
    public List<AbilityData> Abilities;
    public List<ModifierData> Modifiers;
}
