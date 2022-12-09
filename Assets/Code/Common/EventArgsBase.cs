namespace Code.Common
{
    public abstract class EventArgsBase
    {
        public EventId EventId { get; }

        protected EventArgsBase(EventId id)
        {
            EventId = id;
        }
    }
}