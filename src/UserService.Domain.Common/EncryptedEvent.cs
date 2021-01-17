namespace UserService.Domain.Common
{
    public class EncryptedEvent<TKey>
    {
        public EncryptedEvent(TKey aggregatorId, string encryptedData)
        {
            AggregatorId = aggregatorId;
            EncryptedData = encryptedData;
        }

        public TKey AggregatorId { get; private set; }

        public string EncryptedData { get; private set; }
    }
}