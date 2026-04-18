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
    [SerializeField] private Transform container;
    [SerializeField] private AbilityView abilityPrefab;

    [Header("Characters Avatar")]
    [SerializeField] private CharacterView[] charactersView; 

    private CharacterViewModel _currentViewModel;
    private List<CharacterViewModel> _characterViewModels;
    private List<AbilityView> _currentAbilityViews = new List<AbilityView>();

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

        UpdateAbilities(characterViewModel.Abilities);
    }

    private void UpdateAbilities(List<AbilityData> abilities)
    {
        ClearAbilities();

        if (abilities == null) 
            return;

        foreach (var ability in abilities)
        {
            var abilityView = Instantiate(abilityPrefab, container);
            abilityView.Initialize(ability);
            _currentAbilityViews.Add(abilityView);
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

    private void UpdateHealthDisplay(string currentHealth, string maxHealth) => 
        healthText.text = $"{currentHealth}/{maxHealth}";

    private void UpdateArmorDisplay(string currenArmor, string maxArmor) =>
        armorText.text = $"{currenArmor}/{maxArmor}";

    public override void Deinitialize() 
    {
        ClearAbilities();
        EventAggregator.Instance.Unsubscribe<CharacterSelectedEvent>(CharacterSelected);
    }
}
