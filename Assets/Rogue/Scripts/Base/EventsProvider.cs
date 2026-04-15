public static class EventsProvider
{
    public class OpenScreenEvent
    {
        public Enums.ScreenType ScreenType;

        public OpenScreenEvent(Enums.ScreenType screenType) => 
            ScreenType = screenType;
    }
}
