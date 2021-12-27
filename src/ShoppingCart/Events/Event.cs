using System;
namespace ShoppingCart.Events
{
    public class Event
    {
        public long SequenceNumber { get; }
        public DateTimeOffset OccuredAt { get; }
        public string Name { get; }
        public object Content { get; }
        public Event(long SequenceNumber,DateTimeOffset OccuredAt,string Name,object Content)
        {
            this.SequenceNumber = SequenceNumber;
            this.OccuredAt = OccuredAt;
            this.Name = Name;
            this.Content = Content;
        }
    }
}