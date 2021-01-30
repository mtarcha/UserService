using System;

namespace UserService.Infrastructure.Sql.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public bool IsEmailVerified { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public Guid? EncryptionKeyId { get; set; }
    }
}