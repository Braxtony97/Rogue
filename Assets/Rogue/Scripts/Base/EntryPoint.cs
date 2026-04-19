using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameData _data;
    [SerializeField] private UIManager _uiManager;

    private List<CharacterViewModel> _characterViewModels = new List<CharacterViewModel>();

    private void Awake()
    {
        Application.targetFrameRate = 60;
        LoadGame();
    }

    private void LoadGame()
    {
        foreach (var characterData in _data.Characters)
        {
            var model = new CharacterModel(characterData);
            var viewModel = new CharacterViewModel(model);
            _characterViewModels.Add(viewModel);
        }

        _uiManager.Initialize(_characterViewModels);

        _uiManager.OpenScreen(Enums.ScreenType.MainMenu);
    }
}
