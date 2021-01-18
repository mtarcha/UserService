namespace UserService.Infrastructure.EventSourcing
{
    public class EncryptedEvent<TId>
    {
        public EncryptedEvent(TId aggregateRootId, string encryptedData)
        {
            AggregateRootId = aggregateRootId;
            EncryptedData = encryptedData;
        }

        public TId AggregateRootId { get; private set; }

        public string EncryptedData { get; private set; }
    }
}