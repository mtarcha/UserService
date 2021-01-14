using System.Threading;
using System.Threading.Tasks;

namespace UserService.Api.Services
{
    public interface IStorageSeeder
    {
        Task SeedAsync(CancellationToken token);
    }
}