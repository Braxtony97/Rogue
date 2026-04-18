using System;

public class SelectCharacterManager
{
    public static SelectCharacterManager Instance => 
        _instance ??= new SelectCharacterManager();
    public event Action<CharacterModel> OnSelectedHeroChanged;

    private static SelectCharacterManager _instance; 
    private CharacterModel _selectedCharacter;

    public CharacterModel SelectedCharacter
    {
        get => _selectedCharacter;
        set
        {
            if (_selectedCharacter != value)
            {
                _selectedCharacter = value;
                OnSelectedHeroChanged?.Invoke(_selectedCharacter);
            }
        }
    }
}
