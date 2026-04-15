using UnityEngine;
using UnityEngine.UI;

public class NewGameScreen : ScreenUI
{
    [Header("Main Buttons")]
    [SerializeField] private Button _close;

    public override void Initialize()
    {
        _close.onClick.AddListener(() => EventAggregator.Instance.Publish(new EventsProvider.OpenScreenEvent(Enums.ScreenType.MainMenu))); ;
    }

    public override void Deinitialize()
    {
        _close.onClick.RemoveAllListeners();
    }
}
