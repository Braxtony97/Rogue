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
            _currentScreen.Deinitialize();
            Destroy(_currentScreen.gameObject);
        }

        foreach (var screen in _screens)
        {
            if (screen.ScreenType == screenType)
            {
                ScreenUI newScreen = Instantiate(screen);

                newScreen.transform.SetParent(canvas, false);

                _currentScreen = newScreen;
                _currentScreen.Initialize(_characterViewModels, _uiPanelController);
            }
        }
    }
}
