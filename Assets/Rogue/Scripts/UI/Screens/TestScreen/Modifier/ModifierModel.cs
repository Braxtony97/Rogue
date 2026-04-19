using System;
using UnityEngine;

public class ModifierModel
{
    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
    public Enums.ModifierType ModifierType { get; private set; }
    public bool IsAttached => AttachedToAbility != null;

    public AbilityModel AttachedToAbility { get; private set; }

    public event Action<AbilityModel> OnAttached;
    public event Action OnDetached;

    public ModifierModel(ModifierData data)
    {
        Name = data.Name;
        Icon = data.Icon;
        ModifierType = data.ModifierType;
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
