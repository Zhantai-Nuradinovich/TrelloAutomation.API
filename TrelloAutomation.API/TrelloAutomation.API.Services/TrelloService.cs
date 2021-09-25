using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using TrelloAutomation.API.API.Common.Settings;
using TrelloAutomation.API.Services.Contracts;
using TrelloAutomation.API.Services.Model;
using Manatee.Trello.Rest;
using Manatee.Trello;
using System.Linq;

namespace TrelloAutomation.API.Services
{
    public class TrelloService : ITrelloService
    {
        private AppSettings _settings;
        private readonly IMapper _mapper;

        public TrelloService(IOptions<AppSettings> settings, IMapper mapper)
        {
            _settings = settings?.Value;
            _mapper = mapper;
        }

        #region Base Methods
        public void SetAuthorization(string token, string key)
        {
            TrelloAuthorization.Default.AppKey = key;
            TrelloAuthorization.Default.UserToken = token;
        }

        private async Task<IBoardCollection> GetBoards()
        {
            ITrelloFactory factory = new TrelloFactory();
            var me = await factory.Me();
            var boards = me.Boards;
            await boards.Refresh();
            return boards;
        }

        private async Task<IBoard> GetBoardByName(string boardName)
        {
            var boards = await GetBoards();
            return boards.Where(x => x.Name.ToLower().StartsWith(boardName.ToLower())).FirstOrDefault(); //test board;
        }

        #endregion

        #region Daily preparation
        public async Task<bool> CheckDailyStartAsync()
        {
            var board = await GetBoardByName("zhan.");
            var strategyList = board.Lists.Where(x => x.Name.StartsWith("Strategy")).FirstOrDefault();
            await strategyList.Refresh();

            //Getting planning and reporting cards
            var dailyCard = strategyList.Cards.Where(x => x.Name.ToLower().StartsWith("daily")).FirstOrDefault();
            var dailyReportCard = strategyList.Cards.Where(x => x.Name.ToLower().StartsWith("report")).FirstOrDefault();
            if (dailyCard == null || dailyReportCard == null)
                return false;

            var reportingComments = dailyReportCard.Comments;
            await reportingComments.Refresh();
            var theLastMessage = reportingComments.OrderByDescending(x => x.CreationDate).FirstOrDefault();

            DateTime date = GetDateFromDailyReport(dailyCard.Description);
            DateTime dateFromReport = GetDateFromDailyReport(theLastMessage.Data.Text);
            if (date != dateFromReport)
                return false;

            SetAuthorization("I'm DONE!", "I'm DONE!"); //clean key and token
            return true;
        }

        public Task<bool> CheckWeeklyReportAsync()
        {
            throw null;
        }

        #endregion

        #region Auxilary methods
        private DateTime GetDateFromDailyReport(string dailyReport)
        {
            string[] dayAndMonth = dailyReport.Split("**")[1].Split(".");
            int.TryParse(dayAndMonth[0], out int day);
            int.TryParse(dayAndMonth[1], out int month);
            DateTime date = new DateTime(DateTime.Now.Year, month, day);
            return date;
        }
        #endregion
    }
}
