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
using System.Collections.Generic;
using TrelloAutomation.API.Services.Model.Common;
using TrelloAutomation.API.Services.AuxTrelloServices.Card;

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

        private async Task<List<IBoard>> GetBoardsByName(string boardName)
        {
            var boards = await GetBoards();
            return boards.Where(x => x.Name.ToLower().StartsWith(boardName.ToLower())).ToList();
        }

        #endregion

        #region Daily
        public async Task<BaseResponse<string[]>> CheckDailyStartAsync()
        {
            var response = new BaseResponse<string[]>();
            var errors = new List<string>();
            var boards = await GetBoardsByName("zhan.");
            foreach (var board in boards)
            {
                var strategyList = board.Lists.Where(x => x.Name.ToLower().StartsWith("strategy")).FirstOrDefault();
                if(strategyList == null)
                {
                    errors.Add("Couldn't find Strategy List in " + board.Name + " board");
                    continue;
                }
                await strategyList.Refresh();

                var dailyCard = strategyList.Cards.Where(x => x.Name.ToLower().StartsWith("daily")).FirstOrDefault();
                var dailyReportCard = strategyList.Cards.Where(x => x.Name.ToLower().StartsWith("report")).FirstOrDefault();
                if (dailyCard == null || dailyReportCard == null)
                {
                    errors.Add("Couldn't find Daily and Report cards in " + board.Name + " board");
                    continue;
                }
                var reportingComments = dailyReportCard.Comments;
                await reportingComments.Refresh();

                var theLastMessage = reportingComments.OrderByDescending(x => x.CreationDate).FirstOrDefault();
                if(string.IsNullOrEmpty(dailyCard.Description) || string.IsNullOrEmpty(theLastMessage.Data.Text))
                {
                    errors.Add("Description in Daily card or Comments in Report are empty in " + board.Name + " board");
                    continue;
                }

                DateTime date = CardService.GetDateFromDailyReport(dailyCard.Description);
                DateTime dateFromReport = CardService.GetDateFromDailyReport(theLastMessage.Data.Text);
                if (date != dateFromReport)
                {
                    errors.Add("Dates are not equal (" + date.ToShortDateString() 
                                                       + " AND " + dateFromReport.ToShortDateString() 
                                                       + "). Check if yesterday's report is in the right place in " 
                                                       + board.Name + " board");
                }
            }
            SetAuthorization("I'm DONE!", "I'm DONE!"); //clean key and token

            response.IsSuccess = !errors.Any();
            response.Data = errors.ToArray();
            response.Message = !errors.Any() ? "Trello is ready." : "Trello is not ready! Something went wrong, see more details.";
            return response;
        }
        public async Task<BaseResponse<string[]>> PrepareDailyStartAsync()
        {
            var response = new BaseResponse<string[]>();
            var errors = new List<string>();
            var boards = await GetBoardsByName("zhan.");
            foreach (var board in boards)
            {
                var strategyList = board.Lists.Where(x => x.Name.ToLower().StartsWith("strategy")).FirstOrDefault();
                if (strategyList == null)
                {
                    errors.Add("Couldn't find Strategy List in " + board.Name + " board");
                    continue;
                }
                await strategyList.Refresh();

                var dailyCard = strategyList.Cards.Where(x => x.Name.ToLower().StartsWith("daily")).FirstOrDefault();
                var dailyReportCard = strategyList.Cards.Where(x => x.Name.ToLower().StartsWith("report")).FirstOrDefault();
                if (dailyCard == null || dailyReportCard == null)
                {
                    errors.Add("Couldn't find Daily and Report cards in " + board.Name + " board");
                    continue;
                }
                var reportingComments = dailyReportCard.Comments;
                await reportingComments.Refresh();

                var theLastMessage = reportingComments.OrderByDescending(x => x.CreationDate).FirstOrDefault();
                if (string.IsNullOrEmpty(dailyCard.Description) || string.IsNullOrEmpty(theLastMessage.Data.Text))
                {
                    errors.Add("Description in Daily card or Comments in Report are empty in " + board.Name + " board");
                    continue;
                }

                DateTime date = CardService.GetDateFromDailyReport(dailyCard.Description);
                DateTime dateFromReport = CardService.GetDateFromDailyReport(theLastMessage.Data.Text);
                if (date != dateFromReport)
                {
                    var newComment = dailyCard.Description;
                    await reportingComments.Add(newComment);

                    var dailyComments = dailyCard.Comments;
                    await dailyComments.Refresh();

                    var comment = dailyComments.FirstOrDefault().Data.Text;
                    dailyComments.FirstOrDefault().Data.Text = CardService.SetDateToDailyReport(comment);
                }
            }
            SetAuthorization("I'm DONE!", "I'm DONE!"); //clean key and token

            response.IsSuccess = !errors.Any();
            response.Data = errors.ToArray();
            response.Message = !errors.Any() ? "Trello is ready." : "Trello is not ready! Something went wrong, see more details.";
            return response;
        }
        #endregion

        #region Weekly
        public Task<BaseResponse<string[]>> CheckWeeklyReportAsync()
        {
            return null;
        }
        public async Task<BaseResponse<string[]>> PrepareReportsAsync()//Todo: consider analyzer for reports
        {
            var response = new BaseResponse<string[]>();
            var errors = new List<string>();
            var boards = await GetBoardsByName("zhan.");
            foreach (var board in boards)
            {
                var strategyList = board.Lists.Where(x => x.Name.ToLower().StartsWith("strategy")).FirstOrDefault();
                if (strategyList == null)
                {
                    errors.Add("Couldn't find Strategy List in " + board.Name + " board");
                    continue;
                }
                await strategyList.Refresh();

                var dailyReportCard = strategyList.Cards.Where(x => x.Name.ToLower().StartsWith("report")).FirstOrDefault();
                if (dailyReportCard == null)
                {
                    errors.Add("Couldn't find Report card in " + board.Name + " board");
                    continue;
                }

                TrelloBoardType boardType = GetBoardType(board);
                var possibleErrors = await CardService.ProcessCard(dailyReportCard, boardType);//write results of the week to the description
                if (possibleErrors.Any())
                    errors.AddRange(possibleErrors);

            }
            SetAuthorization("I'm DONE!", "I'm DONE!"); //clean key and token
            
            response.IsSuccess = !errors.Any();
            response.Data = errors.ToArray();
            response.Message = !errors.Any() ? "Trello is ready." : "Something went wrong, see more details.";
            return response;
        }
        #endregion

        #region Auxilary methods

        private TrelloBoardType GetBoardType(IBoard board)
        {
            if (board.Name.ToLower().Contains("plan"))
                return TrelloBoardType.Plan;
            else if (board.Name.ToLower().Contains("health"))
                return TrelloBoardType.Health;
            else if (board.Name.ToLower().Contains("project"))
                return TrelloBoardType.Project;
            else if (board.Name.ToLower().Contains("education"))
                return TrelloBoardType.Routine;

            return TrelloBoardType.None;
        }

        //todo: Validator
        //private bool ValidateTrelloEntity(Model.Common.TrelloErrorTypes type, object entity, out string message, string boardName)
        //{
        //    switch (type)
        //    {
        //        case Model.Common.TrelloErrorTypes.Board:
        //            break;
        //        case Model.Common.TrelloErrorTypes.List:
        //            if (entity == null)
        //            {
        //                message = "Couldn't find Strategy List in " + boardName + " board";
        //                return false;
        //            }
        //            break;
        //        case Model.Common.TrelloErrorTypes.Card:
        //            break;
        //        case Model.Common.TrelloErrorTypes.Comment:
        //            break;
        //        case Model.Common.TrelloErrorTypes.Date:
        //            break;

        //    }
        //    message = "Ok.";
        //    return true;
        //}
        #endregion
    }
}
