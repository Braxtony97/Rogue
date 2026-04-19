using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CharacterModel
{
    public int CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }

    public int CurrentArmor
    {
        get => _currentArmor;
        private set
        {
            _currentArmor = Mathf.Clamp(value, 0, MaxArmor);
            OnArmorChanged?.Invoke(_currentArmor);
        }
    }

    public event Action<int> OnHealthChanged;
    public event Action<int> OnArmorChanged;
    public string Name => _characterData.Name;
    public Enums.CharacterName CharacterNameId => _characterData.CharacterNameId;
    public Sprite Avatar => _characterData.Avatar;
    public Sprite FullPortrait => _characterData.FullPortrait;
    public int MaxHealth => _characterData.MaxHealth;
    public int MaxArmor => _characterData.MaxArmor;

    public List<AbilityModel> Abilities { get; private set; }

    public List<ModifierModel> Modifiers { get; private set; }

    private int _currentHealth;
    private int _currentArmor;
    private readonly CharacterData _characterData;
    private readonly GameData _gameData;

    public CharacterModel(CharacterData characterData, GameData data)
    {
        _characterData = characterData;
        _gameData = data;
        _currentHealth = _characterData.MaxHealth;
        _currentArmor = _characterData.MaxArmor;

        InitializeAbilities();
        InitializeModifiers();
    }

    private void InitializeModifiers()
    {
        Modifiers = new List<ModifierModel>();

        foreach (ModifierData modifierData in _characterData.Modifiers)
        {
            ModifierModel modifierModel = new ModifierModel(modifierData, _gameData);
            Modifiers.Add(modifierModel);
        }
    }

    private void InitializeAbilities()
    {
        Abilities = new List<AbilityModel>();

        foreach (AbilityData abilityData in _characterData.Abilities)
        {
            AbilityModel abilityModel = new AbilityModel(abilityData);
            Abilities.Add(abilityModel);
        }
    }

    public void TakeDamage(int damage)
    {
        int remainingDamage = damage;
        if (CurrentArmor > 0)
        {
            int armorAbsorb = Mathf.Min(CurrentArmor, remainingDamage);
            CurrentArmor -= armorAbsorb;
            remainingDamage -= armorAbsorb;
        }

        if (remainingDamage > 0)
        {
            CurrentHealth -= remainingDamage;
        }
    }
}
