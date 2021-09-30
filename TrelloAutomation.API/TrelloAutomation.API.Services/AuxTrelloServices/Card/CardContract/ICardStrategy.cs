using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloAutomation.API.Services.AuxTrelloServices.Card.CardContract
{
    interface ICardStrategy
    {
        Task<List<string>> ProcessCard(Manatee.Trello.ICard card);
    }
}
