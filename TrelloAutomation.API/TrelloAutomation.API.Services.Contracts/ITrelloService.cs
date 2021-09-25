using System.Threading.Tasks;
using TrelloAutomation.API.Services.Model;

namespace TrelloAutomation.API.Services.Contracts
{
    public interface ITrelloService
    {
        void SetAuthorization(string token, string key);
        Task<bool> CheckDailyStartAsync();
        Task<bool> CheckWeeklyReportAsync();
    }
}
