using System.Collections.Generic;
using UnityEngine;

public class CharacterModel
{
    public readonly string CharacterName;
    public readonly Sprite Avatar;
    public readonly Sprite FullPortrait;
    public readonly int MaxHealth;
    public readonly int MaxArmor;
    public readonly List<AbilityData> Abilities;
    public readonly List<ModifierData> Modifiers;

    public CharacterModel(CharacterData characterData)
    {
        CharacterName = characterData.Name;
        Avatar = characterData.Avatar;
        FullPortrait = characterData.FullPortrait;
        MaxHealth = characterData.MaxHealth;
        MaxArmor = characterData.MaxArmor;
        Abilities = new List<AbilityData>(characterData.Abilities);
        Modifiers = new List<ModifierData>(characterData.Modifiers);
    }
}
