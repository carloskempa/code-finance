namespace CodeFinance.Domain.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }

        public Message()
        {
            MessageType = this.GetType().Name;
        }
    }
}
