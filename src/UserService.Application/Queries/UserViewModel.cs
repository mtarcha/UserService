using System;

namespace UserService.Application.Queries
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public bool IsEmailVerified { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}