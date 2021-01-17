using System;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Sql
{
    public class UserEncryptionKeys
    {
        public Guid Id { get; set; }

        public string EncryptionKey { get; set; }

        public User User { get; set; }
    }
}