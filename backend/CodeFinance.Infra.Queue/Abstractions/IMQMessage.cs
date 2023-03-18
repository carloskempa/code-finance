namespace CodeFinance.Infra.Queue.Abstractions
{
    public interface IMQMessage
    {
        string ModelName { get; }
        string Body { get; set; }
        byte[] ToBytes();
        string ToJson();
    }
}
