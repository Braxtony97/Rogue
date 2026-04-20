using System;
using UnityEngine;

public class ModifierViewModel
{
    public event Action<bool> OnHighlihtChanged;
    public event Action<bool> OnDragStateChanged;
    public event Action<bool> OnAttachedChanged;

    public ModifierModel GetModel() => _model;
    public string Name => _model.Name;
    public Sprite IconLogo => _model.IconLogo;
    public Color IconBackColor => _model.IconBackColor;
    public bool IsAttached => _model.IsAttached; 
    public Enums.ModifierType ModifierType => _model.ModifierType;

    private readonly ModifierModel _model;
    private bool _isHighlighted;
    private bool _isCompatibleHighlight;
    private bool _isDragging;

    public ModifierViewModel(ModifierModel model)
    {
        _model = model;

        _model.OnAttached += OnAttached;
        _model.OnDetached += OnDetached;
    }

    public void SetDragging(bool isDragging)
    {
        _isDragging = isDragging;
        OnDragStateChanged?.Invoke(isDragging);
    }

    private void OnAttached(AbilityModel ability)
    {
        OnAttachedChanged?.Invoke(true);
    }

    private void OnDetached()
    {
        OnAttachedChanged?.Invoke(false);
    }

    public bool CanAttachToAbility(AbilityViewModel abilityViewModel)
    {
        if (_model.IsAttached)
            return false;

        return abilityViewModel?.CanAcceptModifier(this) ?? false;
    }

    public void AttachToAbility(AbilityViewModel abilityViewModel)
    {
        if (!CanAttachToAbility(abilityViewModel))
            return;

        abilityViewModel.AttachModifier(this);
    }

    public void SetCompatibleHighlight(bool isCompatible)
    {
        _isCompatibleHighlight = isCompatible;
        OnHighlihtChanged?.Invoke(_isCompatibleHighlight);
    }
}
