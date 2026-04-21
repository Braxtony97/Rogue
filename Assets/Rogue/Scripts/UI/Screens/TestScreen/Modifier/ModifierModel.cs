using System;
using UnityEngine;

public class ModifierModel
{
    public string Name { get; private set; }
    public Sprite IconLogo { get; private set; }
    public Color IconBackColor { get; }
    public Enums.ModifierType ModifierType { get; private set; }
    public Enums.ModifierNameId ModifierName { get; private set; }
    public bool IsAttached => AttachedToAbility != null;

    public AbilityModel AttachedToAbility { get; private set; }

    public event Action<AbilityModel> OnAttached;
    public event Action OnDetached;

    public ModifierModel(ModifierData data, GameData gameData)
    {
        Name = data.Name;
        ModifierType = data.ModifierType;
        ModifierName = data.ModifierName;

        ModifierIcon modifierIcon = gameData.GetModifierIcon(ModifierType);
        IconLogo = modifierIcon.IconLogo;
        IconBackColor = modifierIcon.ColorBack;
    }

    public void AttachToAbility(AbilityModel ability)
    {
        if (AttachedToAbility != null)
            Detach();

        AttachedToAbility = ability;
        OnAttached?.Invoke(ability);
    }

    public void Detach()
    {
        AttachedToAbility = null;
        OnDetached?.Invoke();
    }
}
