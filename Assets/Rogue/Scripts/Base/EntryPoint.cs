using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameData _data;
    [SerializeField] private UIManager _uiManager;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        LoadGame();
    }

    private void LoadGame()
    {
        _uiManager.Initialize();

        _uiManager.OpenScreen(Enums.ScreenType.MainMenu);
    }
}
