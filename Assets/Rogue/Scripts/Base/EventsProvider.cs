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
        public AbilityViewModel ViewModel { get; }
        public bool IsHover { get; }

        public AbilityHoverEvent(AbilityViewModel viewModel, bool isHover)
        {
            ViewModel = viewModel;
            IsHover = isHover;
        }
    }

    public class ModifierDragStateChangedEvent
    {
        public ModifierViewModel Modifier { get; }
        public bool IsDragging { get; }

        public ModifierDragStateChangedEvent(ModifierViewModel modifier, bool isDragging)
        {
            Modifier = modifier;
            IsDragging = isDragging;
        }
    }
}
