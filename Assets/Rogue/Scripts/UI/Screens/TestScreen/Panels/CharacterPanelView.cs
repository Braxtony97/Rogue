using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EventsProvider;

public class CharacterPanelView : PanelUI
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text armorText;
    [SerializeField] private Image portraitImage;

    [Header("Abilities")]
    [SerializeField] private Transform abilitiesContainer;
    [SerializeField] private AbilityView abilityPrefab;

    [Header("Modifiers")]
    [SerializeField] private Transform modifiersContainer;
    [SerializeField] private ModifierView modifierPrefab;

    [Header("Characters Avatar")]
    [SerializeField] private CharacterView[] charactersView; 

    [SerializeField] private RectMask2D mask; 

    private CharacterViewModel _currentViewModel;
    private List<CharacterViewModel> _characterViewModels;
    private List<AbilityView> _currentAbilityViews = new List<AbilityView>();
    private List<ModifierView> _currentModifierViews = new List<ModifierView>();

    public override void Initialize(List<CharacterViewModel> characterViewModels)
    {
        _characterViewModels = characterViewModels;

        for(int i = 0; i < charactersView.Length && i < _characterViewModels.Count; i++)
        {
            charactersView[i].Initialize(_characterViewModels[i]);
        }

        UpdateUIData(_characterViewModels[0]);

        EventAggregator.Instance.Publish(new CharacterSelectedEvent(_characterViewModels[0].CharacterNameId));

        EventAggregator.Instance.Subscribe<CharacterSelectedEvent>(CharacterSelected);
    }

    private void CharacterSelected(CharacterSelectedEvent characterSelectedEvent)
    {
        foreach (CharacterViewModel characterViewModel in _characterViewModels)
        {
            if (characterViewModel.CharacterNameId == characterSelectedEvent.CharacterName)
            {
                _currentViewModel = characterViewModel;
                break;
            }
        }

        UpdateUIData(_currentViewModel);
    }

    private void UpdateUIData(CharacterViewModel characterViewModel)
    {
        nameText.text = characterViewModel.Name;
        portraitImage.sprite = characterViewModel.FullPortrait;
        UpdateHealthDisplay(characterViewModel.CurrentHealthText, characterViewModel.MaxHealthText);
        UpdateArmorDisplay(characterViewModel.CurrentArmorText, characterViewModel.MaxArmorText);

        UpdateAbilities(characterViewModel.AbilityViewModels);
        UpdateModifiers(characterViewModel.ModifierViewModels);
    }

    private void UpdateAbilities(List<AbilityViewModel> abilityViewModels)
    {
        ClearAbilities();

        if (abilityViewModels == null) 
            return;

        foreach (AbilityViewModel ability in abilityViewModels)
        {
            AbilityView abilityView = Instantiate(abilityPrefab, abilitiesContainer);
            abilityView.Initialize(ability);
            _currentAbilityViews.Add(abilityView);
        }
    }

    private void UpdateModifiers(List<ModifierViewModel> modifierViewModels)
    {
        ClearModifiers();

        if (modifierViewModels == null)
            return;

        foreach (ModifierViewModel modifier in modifierViewModels)
        {
            ModifierView modifierView = Instantiate(modifierPrefab, modifiersContainer);
            modifierView.Initialize(modifier, mask); 
            _currentModifierViews.Add(modifierView);
        }
    }

    private void ClearAbilities()
    {
        foreach (var abilityView in _currentAbilityViews)
        {
            if (abilityView != null)
                Destroy(abilityView.gameObject);
        }
        _currentAbilityViews.Clear();
    }

    private void ClearModifiers()
    {
        foreach (ModifierView modifierView in _currentModifierViews)
        {
            if (modifierView != null)
                Destroy(modifierView.gameObject);
        }
        _currentModifierViews.Clear();
    }

    private void UpdateHealthDisplay(string currentHealth, string maxHealth) => 
        healthText.text = $"{currentHealth}/{maxHealth}";

    private void UpdateArmorDisplay(string currenArmor, string maxArmor) =>
        armorText.text = $"{currenArmor}/{maxArmor}";

    public override void Deinitialize() 
    {
        ClearAbilities();
        ClearModifiers();
        EventAggregator.Instance.Unsubscribe<CharacterSelectedEvent>(CharacterSelected);
    }
}
