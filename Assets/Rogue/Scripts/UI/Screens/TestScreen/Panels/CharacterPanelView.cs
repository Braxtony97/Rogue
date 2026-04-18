using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EventsProvider;

public class CharacterPanelView : PanelUI
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text currentHealthText;
    [SerializeField] private TMP_Text currentArmorText;
    [SerializeField] private TMP_Text maxHealthText;
    [SerializeField] private TMP_Text maxArmorText;
    [SerializeField] private Image portraitImage;

    [SerializeField] private CharacterView[] charactersView;

    private CharacterViewModel _currentViewModel;
    private List<CharacterViewModel> _characterViewModels;

    public override void Initialize(List<CharacterViewModel> characterViewModels)
    {
        _characterViewModels = characterViewModels;

        for(int i = 0; i < charactersView.Length && i < _characterViewModels.Count; i++)
        {
            charactersView[i].Initialize(_characterViewModels[i]);
        }

        EventAggregator.Instance.Subscribe<CharacterSelectedEvent>(CharacterSelected);
    }

    private void CharacterSelected(CharacterSelectedEvent characterSelectedEvent)
    {
        if (_currentViewModel != null)
        {
            _currentViewModel.OnHealthTextChanged -= UpdateCurrentHealth;
            _currentViewModel.OnArmorTextChanged -= UpdateCurrentArmor;
        }

        foreach (CharacterViewModel characterViewModel in _characterViewModels)
        {
            if (characterViewModel.CharacterNameId == characterSelectedEvent.CharacterName)
            {
                _currentViewModel = characterViewModel;
                break;
            }
        }

        _currentViewModel.OnHealthTextChanged += UpdateCurrentHealth;
        _currentViewModel.OnArmorTextChanged += UpdateCurrentArmor;

        nameText.text = _currentViewModel.Name;
        portraitImage.sprite = _currentViewModel.FullPortrait;
        UpdateMaxHealth(_currentViewModel.MaxHealthText);
        UpdateMaxArmor(_currentViewModel.MaxArmorText);
    }

    private void UpdateCurrentHealth(string text) => currentHealthText.text = text;
    private void UpdateCurrentArmor(string text) => currentArmorText.text = text;
    private void UpdateMaxHealth(string text) => maxHealthText.text = text;
    private void UpdateMaxArmor(string text) => maxArmorText.text = text;

    public override void Deinitialize() 
    {
        EventAggregator.Instance.Unsubscribe<CharacterSelectedEvent>(CharacterSelected);
    }
}
