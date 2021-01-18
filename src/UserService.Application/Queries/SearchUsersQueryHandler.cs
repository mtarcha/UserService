using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Application.Queries
{
    public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _userRepository;

        public SearchUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.FindUsersAsync(request.Email, cancellationToken);
        }
    }
}