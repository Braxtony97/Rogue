using System;
using UnityEngine;

public class ModifierViewModel
{
    public string Name => _model.Name;
    public Sprite Icon => _model.Icon;
    public bool IsAttached => _model.IsAttached;
    public Enums.ModifierType ModifierType => _model.ModifierType;

    public event Action OnDataChanged;

    private readonly ModifierModel _model;
    private bool _isHighlighted;

    public ModifierViewModel(ModifierModel model)
    {
        _model = model;

        _model.OnAttached += _ => OnDataChanged?.Invoke();
        _model.OnDetached += () => OnDataChanged?.Invoke();
    }

    public bool CanAttachToAbility(AbilityViewModel abilityVM)
    {
        return abilityVM?.CanAcceptModifier(this) ?? false;
    }

    public void AttachToAbility(AbilityViewModel abilityVM)
    {
        if (!CanAttachToAbility(abilityVM))
            return;

        abilityVM.AttachModifier(this);
    }

    public void SetHighlight(bool highlight)
    {
        _isHighlighted = highlight;
        OnDataChanged?.Invoke();
    }

    public bool IsHighlighted => _isHighlighted;
    public ModifierModel GetModel() => _model;
}
