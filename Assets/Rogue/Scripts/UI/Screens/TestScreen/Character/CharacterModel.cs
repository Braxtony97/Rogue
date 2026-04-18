using System;
using System.Collections.Generic;
using UnityEngine;

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
    public string Name => _data.Name;
    public Enums.CharacterName CharacterNameId => _data.CharacterNameId;
    public Sprite Avatar => _data.Avatar;
    public Sprite FullPortrait => _data.FullPortrait;
    public int MaxHealth => _data.MaxHealth;
    public int MaxArmor => _data.MaxArmor;

    public readonly List<AbilityData> Abilities;
    public readonly List<ModifierData> Modifiers;

    private int _currentHealth;
    private int _currentArmor;
    private readonly CharacterData _data;

    public CharacterModel(CharacterData data)
    {
        _data = data;
        _currentHealth = _data.MaxHealth;
        _currentArmor = _data.MaxArmor;
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
