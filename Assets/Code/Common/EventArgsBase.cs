namespace Code.Common
{
    public class EventArgsBase
    {
        public EventId EventId { get; }

        protected EventArgsBase(EventId id)
        {
            EventId = id;
        }
    }
}