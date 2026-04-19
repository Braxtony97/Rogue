using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ModifierIcon
{
    public Enums.ModifierType ModifierType;
    public Sprite IconLogo;
    public Color ColorBack;
}

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
    public List<CharacterData> Characters;
    public List<ModifierIcon> ModifierIcons;

    public ModifierIcon GetModifierIcon(Enums.ModifierType type)
    {
        foreach (var ModifierIcon in ModifierIcons)
        {
            if (ModifierIcon.ModifierType == type)
                return ModifierIcon;
        }

        Debug.LogWarning($"ModifierIcon for type '{type}' not found");

        return null;
    }
}
