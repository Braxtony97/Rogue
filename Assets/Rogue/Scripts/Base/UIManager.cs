using UnityEngine;
using static EventsProvider;

public class UIManager : MonoBehaviour
{
    public ScreenUI CurrentScreen => _currentScreen;

    [SerializeField] private ScreenUI[] _screens;
    [SerializeField] private Transform canvas;

    private ScreenUI _currentScreen;

    public void Initialize()
    {
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
                _currentScreen.Initialize();
            }
        }
    }
}
