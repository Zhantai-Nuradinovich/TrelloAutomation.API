using System.Threading.Tasks;
using TrelloAutomation.API.Services.Model;

namespace TrelloAutomation.API.Services.Contracts
{
    public interface ITrelloService
    {
        Task<bool> CheckDailyStartAsync(string token, string key);
        Task<bool> CheckWeeklyReportAsync(string token, string key);
    }
}
