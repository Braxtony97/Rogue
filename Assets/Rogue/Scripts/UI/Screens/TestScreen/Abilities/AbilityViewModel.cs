using System;
using UnityEngine;

public class AbilityViewModel
{
    public string Name => _model.Name;
    public Sprite Icon => _model.Icon;
    public bool HasModifier => _model.AttachedModifier != null;

    public event Action<bool> OnHighlightChanged;
    public event Action OnDataChanged;
    public event Action<ModifierModel> OnModifierAttached;
    public event Action OnModifierDetached;
    public bool IsHighlighted => _isHighlighted;
    public AbilityModel GetModel() => _model;

    private readonly AbilityModel _model;
    private bool _isHighlighted;
    private ModifierViewModel _currentlyDraggedModifier;

    public AbilityViewModel(AbilityModel model)
    {
        _model = model;

        _model.OnModifierAttached += (modifier) => OnModifierAttached?.Invoke(modifier);
        _model.OnModifierDetached += () => OnModifierDetached?.Invoke();
    }

    public bool CanAcceptModifier(ModifierViewModel modifierViewModel)
    {
        return _model.CanAttachModifier(modifierViewModel?.GetModel());
    }

    public void UpdateHighlightFromDrag(ModifierViewModel draggedModifier)
    {
        _currentlyDraggedModifier = draggedModifier;

        bool shouldHighlight = draggedModifier != null && CanAcceptModifier(draggedModifier);

        SetHighlight(shouldHighlight);
    }

    public void AttachModifier(ModifierViewModel modifierViewModel)
    {
        ModifierModel modifierModel = modifierViewModel?.GetModel();

        if (modifierModel == null || !CanAcceptModifier(modifierViewModel))
            return;

        if (_model.AttachedModifier != null)
            _model.DetachModifier();

        _model.AttachModifier(modifierModel);
        modifierModel.AttachToAbility(_model);
    }

    public void DetachModifier()
    {
        var attached = _model.AttachedModifier;

        if (attached != null)
        {
            attached.Detach();
            _model.DetachModifier();
        }
    }

    public void SetHighlight(bool highlight)
    {
        _isHighlighted = highlight;
        OnHighlightChanged?.Invoke(_isHighlighted);
    }

    public void ClearDragHighlight()
    {
        _currentlyDraggedModifier = null;
        SetHighlight(false);
    }
}
