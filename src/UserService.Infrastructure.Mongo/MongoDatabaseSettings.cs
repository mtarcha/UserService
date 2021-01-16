namespace UserService.Infrastructure.Mongo
{
    public class MongoDatabaseSettings
    {
        public string CollectionName { get; set; }

        public string DatabaseName { get; set; }

        public string ConnectionString { get; set; }
    }
}