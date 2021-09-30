using Manatee.Trello;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloAutomation.API.Services.AuxTrelloServices.Card.CardContract;
using TrelloAutomation.API.Services.AuxTrelloServices.Card.CardStrategy;
using TrelloAutomation.API.Services.Model.Common;

namespace TrelloAutomation.API.Services.AuxTrelloServices.Card
{
    public static class CardService
    {
        private static ICardStrategy _cardStrategy;
        public static async Task<List<string>> ProcessCard(ICard card, TrelloBoardType boardType)
        {
            switch (boardType)
            {
                case TrelloBoardType.Plan:
                    _cardStrategy = new CardPlanStrategy();
                    break;
                case TrelloBoardType.Health:
                    _cardStrategy = new CardHealthStrategy();
                    break;
                case TrelloBoardType.Project:
                    _cardStrategy = new CardHealthStrategy();
                    break;
                default:
                    return null;
            }
            return await _cardStrategy.ProcessCard(card);
        }

        #region AuxilaryMethods
        public static DateTime GetDateFromDailyReport(string dailyReport)
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
