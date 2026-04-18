using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameScreen : ScreenUI
{
    [Header("Main Buttons")]
    [SerializeField] private Button _close;
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _characterButton;

    [Header("Panels")]
    [SerializeField] private PanelUI _inventoryPanel;
    [SerializeField] private PanelUI _characterPanel;

    [SerializeField] private Transform _canvas;

    private List<CharacterViewModel> _characterViewModels;
    private UIPanelController _uiPanelController;

    public override void Initialize(List<CharacterViewModel> characterViewModels, UIPanelController uiPanelController)
    {
        _characterViewModels = characterViewModels;
        _uiPanelController = uiPanelController;

        _close.onClick.AddListener(() 
            => EventAggregator.Instance.Publish(new EventsProvider.OpenScreenEvent(Enums.ScreenType.MainMenu))); ;

        _inventoryButton.onClick.AddListener(ShowInventory);
        _characterButton.onClick.AddListener(ShowCharacter);
    }

    private void ShowCharacter()
    {
        _uiPanelController.ShowPanel(Enums.PanelType.Character, _canvas);
    }

    private void ShowInventory()
    {
        _uiPanelController.ShowPanel(Enums.PanelType.Inventory, _canvas);
    }

    public override void Deinitialize()
    {
        _close.onClick.RemoveAllListeners();
    }
}
