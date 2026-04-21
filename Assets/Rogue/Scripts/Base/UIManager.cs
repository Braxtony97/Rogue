using System.Collections.Generic;
using UnityEngine;
using static EventsProvider;

public class UIManager : MonoBehaviour
{
    public ScreenUI CurrentScreen => _currentScreen;

    [SerializeField] private ScreenUI[] _screens;
    [SerializeField] private Transform canvas;
    [SerializeField] private UIPanelController _uiPanelController;

    private ScreenUI _currentScreen;
    private List<CharacterViewModel> _characterViewModels;
    private Dictionary<Enums.ScreenType, ScreenUI> _instantiatedScreens = new Dictionary<Enums.ScreenType, ScreenUI>();

    public void Initialize(List<CharacterViewModel> characterViewModels)
    {
        _characterViewModels = characterViewModels;
        _uiPanelController.Initialize(_characterViewModels);

        Subscribe();
    }

    private void Subscribe() 
    {
        EventAggregator.Instance.Subscribe<OpenScreenEvent>(ScreenOpen);
    }

    private void ScreenOpen(OpenScreenEvent openScreenEvent) => 
        OpenScreen(openScreenEvent.ScreenType);


    public void OpenScreen(Enums.ScreenType screenType)
    {
        if (_currentScreen != null)
        {
            _currentScreen.gameObject.SetActive(false);
        }

        if (_instantiatedScreens.TryGetValue(screenType, out ScreenUI existingScreen))
        {
            existingScreen.gameObject.SetActive(true);
            _currentScreen = existingScreen;
            return;
        }

        foreach (var screen in _screens)
        {
            if (screen.ScreenType == screenType)
            {
                ScreenUI newScreen = Instantiate(screen);
                newScreen.transform.SetParent(canvas, false);
                newScreen.Initialize(_characterViewModels, _uiPanelController);

                _instantiatedScreens[screenType] = newScreen;
                _currentScreen = newScreen;
                break;
            }
        }
    }
}
