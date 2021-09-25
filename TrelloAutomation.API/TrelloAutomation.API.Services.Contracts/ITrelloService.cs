using System.Threading.Tasks;
using TrelloAutomation.API.Services.Model;

namespace TrelloAutomation.API.Services.Contracts
{
    public interface ITrelloService
    {
        void SetAuthorization(string token, string key);
        Task<BaseResponse<string[]>> CheckDailyStartAsync();
        Task<BaseResponse<string[]>> CheckWeeklyReportAsync();
    }
}
