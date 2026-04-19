public static class EventsProvider
{
    public class OpenScreenEvent
    {
        public Enums.ScreenType ScreenType { get; }

        public OpenScreenEvent(Enums.ScreenType screenType) => 
            ScreenType = screenType;
    }

    public class CharacterSelectedEvent
    {
        public Enums.CharacterName CharacterName { get; }
        public CharacterSelectedEvent(Enums.CharacterName name) => 
            CharacterName = name;
    }

    public class AbilityHoverEvent
    {
        public AbilityViewModel ViewModel;
        public bool IsHover;

        public AbilityHoverEvent(AbilityViewModel viewModel, bool isHover)
        {
            ViewModel = viewModel;
            IsHover = isHover;
        }
    }
}
