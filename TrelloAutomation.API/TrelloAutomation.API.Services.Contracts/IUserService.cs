using System.Threading.Tasks;
using TrelloAutomation.API.Services.Model;

namespace TrelloAutomation.API.Services.Contracts
{
    public interface IUserService
    {
        Task<User> CreateAsync(User user);

        Task<bool> UpdateAsync(User user);

        Task<bool> DeleteAsync(string id);

        Task<User> GetAsync(string id);
    }
}
