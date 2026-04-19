using System;
using UnityEngine;

public class AbilityViewModel
{
    public string Name => _model.Name;
    public Sprite Icon => _model.Icon;
    public bool HasModifier => _model.AttachedModifier != null;
    public Sprite ModifierIcon => _model.AttachedModifier?.Icon;

    public event Action OnDataChanged;
    public bool IsHighlighted => _isHighlighted;
    public AbilityModel GetModel() => _model;

    private readonly AbilityModel _model;
    private bool _isHighlighted;

    public AbilityViewModel(AbilityModel model)
    {
        _model = model;

        _model.OnModifierAttached += _ => OnDataChanged?.Invoke();
        _model.OnModifierDetached += () => OnDataChanged?.Invoke();
    }

    public bool CanAcceptModifier(ModifierViewModel modifierViewModel)
    {
        return _model.CanAttachModifier(modifierViewModel?.GetModel());
    }

    public void AttachModifier(ModifierViewModel modifierViewModel)
    {
        var modifierModel = modifierViewModel?.GetModel();
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
        OnDataChanged?.Invoke();
    }
}
