using Manatee.Trello;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloAutomation.API.Services.AuxTrelloServices.Card.CardContract;

namespace TrelloAutomation.API.Services.AuxTrelloServices.Card.CardStrategy
{
    public class CardPlanStrategy : ICardStrategy
    {
        public Task<List<string>> ProcessCard(ICard card)
        {
            throw new NotImplementedException();
        }
    }
}
