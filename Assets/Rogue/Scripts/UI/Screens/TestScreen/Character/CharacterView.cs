using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EventsProvider;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Image imageForAvatar;
    [SerializeField] private Button _selectButton;
    [SerializeField] private GameObject _selectedHighlight;

    private CharacterViewModel _viewModel;

    public void Initialize(CharacterViewModel viewModel)
    {
        _viewModel = viewModel;

        UpdateView();

        _selectButton.onClick.AddListener(() => _viewModel.SelectCharacter());
        EventAggregator.Instance.Subscribe<CharacterSelectedEvent>(CharacterSelect);
    }

    private void CharacterSelect(CharacterSelectedEvent characterSelectedEvent)
    {
        bool isThisSelected = _viewModel.CharacterNameId == characterSelectedEvent.CharacterName;
        _selectedHighlight.SetActive(isThisSelected);
    }

    private void UpdateView()
    {
        imageForAvatar.sprite = _viewModel.Avatar;
    }
    private void OnDestroy()
    {
        _viewModel?.Unsubscribe();
        _selectButton.onClick.RemoveAllListeners();
    }
}
