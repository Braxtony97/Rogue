using System;
using UnityEngine;

public class ModifierViewModel
{
    public string Name => _model.Name;
    public Sprite Icon => _model.Icon;
    public bool IsAttached => _model.IsAttached;
    public Enums.ModifierType ModifierType => _model.ModifierType;

    public event Action<bool> OnHighlihtChanged;
    public bool IsCompatibleHighlight => _isCompatibleHighlight;

    private readonly ModifierModel _model;
    private bool _isHighlighted;
    private bool _isCompatibleHighlight;

    public ModifierViewModel(ModifierModel model)
    {
        _model = model;

        //_model.OnAttached += _ => OnHighlihtChanged?.Invoke(); 
        //_model.OnDetached += () => OnHighlihtChanged?.Invoke();
    }

    public bool CanAttachToAbility(AbilityViewModel abilityViewModel)
    {
        return abilityViewModel?.CanAcceptModifier(this) ?? false;
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
        OnHighlihtChanged?.Invoke(highlight);
    }

    public bool IsHighlighted => _isHighlighted;
    public ModifierModel GetModel() => _model;

    public void SetCompatibleHighlight(bool isCompatible)
    {
        _isCompatibleHighlight = isCompatible;
        OnHighlihtChanged?.Invoke(_isCompatibleHighlight);
    }
}
