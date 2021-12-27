namespace ShoppingCart.Events
{
    public interface IEventStore
    {        
        IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
        void Raise(string eventName, object content);
    }

    public class EventStore : IEventStore
    {
        public EventStore()
        {

        }
        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            throw new NotImplementedException();

            //return database.Where(e => e.SequenceNumber >= firstEventSequenceNumber &&
            //                           e.SequenceNumber <= lastEventSequenceNumber)
            //                           .OrderBy(e => e.SequenceNumber);
        }

        public void Raise(string eventName, object content)
        {
            //var seqNumber = database.NextSequenceNumber();
            //database.Add(new Event(
            //    seqNumber,
            //    DateTimeOffset.UtcNow,
            //    eventName,
            //    content)
            //);
        }
    }
}
