using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloAutomation.API.Services.Model
{
    public class TrelloAction : TrelloBaseEntity
    {
        public string IdMemberCreator { get; set; }
        public TrelloActionData Data { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
    public class TrelloActionData
    {
        public string Text { get; set; }
        public TrelloCard Card { get; set; }
        public TrelloBoard Board { get; set; }
        public TrelloList List { get; set; }
    }
}
