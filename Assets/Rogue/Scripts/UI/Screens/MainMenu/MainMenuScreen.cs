using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : ScreenUI
{
    [Header("Main Buttons")]
    [SerializeField] private Button _newGame;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _exit;

    public override void Initialize()
    {
        _newGame.onClick.AddListener(() => EventAggregator.Instance.Publish(new EventsProvider.OpenScreenEvent(Enums.ScreenType.NewGame)));

        _exit.onClick.AddListener(QuitApplication);
    }

    private void QuitApplication()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    public override void Deinitialize()
    {
        _newGame.onClick.RemoveAllListeners();
        _exit.onClick.RemoveAllListeners();
    }
}
