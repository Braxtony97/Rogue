using System;
using UnityEngine;

public class CharacterViewModel
{
    public event Action<string> OnHealthTextChanged;
    public event Action<string> OnArmorTextChanged;

    public string Name => _model.Name;
    public Enums.CharacterName CharacterNameId=> _model.CharacterNameId;
    public Sprite Avatar => _model.Avatar;
    public Sprite FullPortrait => _model.FullPortrait;
    public string CurrentHealthText => $"{_model.CurrentHealth}";
    public string CurrentArmorText => $"{_model.CurrentArmor}";
    public string MaxHealthText => $"{_model.MaxHealth}";
    public string MaxArmorText => $"{_model.MaxArmor}";

    private readonly CharacterModel _model;

    public CharacterViewModel(CharacterModel model)
    {
        _model = model;

        Subscribe();
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

    public void SelectHero() => 
        EventAggregator.Instance.Publish(new EventsProvider.CharacterSelectedEvent(_model.CharacterNameId));

    public void Unsubscribe()
    {
        _model.OnHealthChanged -= OnHealthChanged;
        _model.OnArmorChanged -= OnArmorChanged;
    }
}
