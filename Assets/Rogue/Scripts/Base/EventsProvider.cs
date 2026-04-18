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
}
