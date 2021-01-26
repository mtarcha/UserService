using System;

namespace UserService.Infrastructure.Sql.Entities
{
    public class UserEncryptionKeys
    {
        public Guid Id { get; set; }

        public string EncryptionKey { get; set; }

        public User User { get; set; }
    }
}