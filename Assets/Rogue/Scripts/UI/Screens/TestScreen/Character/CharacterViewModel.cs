using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterViewModel
{
    public event Action<string> OnHealthTextChanged;
    public event Action<string> OnArmorTextChanged;
    public event Action OnAbilitiesChanged;
    public event Action OnModifiersChanged;

    public string Name => _model.Name;
    public Enums.CharacterName CharacterNameId=> _model.CharacterNameId;
    public Sprite Avatar => _model.Avatar;
    public Sprite FullPortrait => _model.FullPortrait;
    public string CurrentHealthText => $"{_model.CurrentHealth}";
    public string CurrentArmorText => $"{_model.CurrentArmor}";
    public string MaxHealthText => $"{_model.MaxHealth}";
    public string MaxArmorText => $"{_model.MaxArmor}";
    public List<AbilityViewModel> AbilityViewModels { get; private set;}
    public List<ModifierViewModel> ModifierViewModels { get; private set; }

    private readonly CharacterModel _model;

    public CharacterViewModel(CharacterModel model)
    {
        _model = model;

        InitializeAbilities();
        InitializeModifiers();

        Subscribe();
    }

    private void InitializeModifiers()
    {
        ModifierViewModels = new List<ModifierViewModel>();

        foreach (ModifierModel modifierModel in _model.Modifiers)
        {
            ModifierViewModel modifier = new ModifierViewModel(modifierModel);
            modifier.OnDataChanged += () => OnModifiersChanged?.Invoke();
            ModifierViewModels.Add(modifier);
        }
    }

    private void InitializeAbilities()
    {
        AbilityViewModels = new List<AbilityViewModel>();

        foreach (AbilityModel abilityModel in _model.Abilities)
        {
            AbilityViewModel ability = new AbilityViewModel(abilityModel);
            ability.OnDataChanged += () => OnAbilitiesChanged?.Invoke();
            AbilityViewModels.Add(ability);
        }
    }

    private void Subscribe()
    {
        _model.OnHealthChanged += OnHealthChanged;
        _model.OnArmorChanged += OnArmorChanged;
    }

    private void OnHealthChanged(int health) => 
        OnHealthTextChanged?.Invoke(CurrentHealthText);

    private void OnArmorChanged(int armor) => 
        OnArmorTextChanged?.Invoke(CurrentArmorText);

    public void SelectCharacter() => 
        EventAggregator.Instance.Publish(new EventsProvider.CharacterSelectedEvent(_model.CharacterNameId));

    public void Unsubscribe()
    {
        _model.OnHealthChanged -= OnHealthChanged;
        _model.OnArmorChanged -= OnArmorChanged;
    }
}
