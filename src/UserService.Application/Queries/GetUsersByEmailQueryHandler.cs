using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Application.Queries
{
    public class GetUsersByEmailQueryHandler : IRequestHandler<GetUsersByEmailQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetUsersByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUsersByEmailPartAsync(request.EmailPart, cancellationToken);
        }
    }
}