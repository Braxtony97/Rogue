using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityModel
{
    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
    public Enums.AbilityType AbilityType { get; private set; }
    public List<Enums.ModifierType> CompatibleModifiers { get; private set; }
    public ModifierModel AttachedModifier { get; private set; }

    public event Action<ModifierModel> OnModifierAttached;
    public event Action OnModifierDetached;

    public AbilityModel(AbilityData data)
    {
        Name = data.Name;
        Icon = data.Icon;
        AbilityType = data.AbilityType;
        CompatibleModifiers = data.CompatibleModifiers;
    }

    public bool CanAttachModifier(ModifierModel modifier)
    {
        return 
            modifier != null && 
            CompatibleModifiers.Contains(modifier.ModifierType) && 
            AttachedModifier == null;
    }

    public void AttachModifier(ModifierModel modifier)
    {
        if (!CanAttachModifier(modifier))
            return;

        AttachedModifier = modifier;
        OnModifierAttached?.Invoke(modifier);
    }

    public void DetachModifier()
    {
        AttachedModifier = null;
        OnModifierDetached?.Invoke();
    }
}
