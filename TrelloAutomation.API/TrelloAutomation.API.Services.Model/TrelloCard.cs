using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloAutomation.API.Services.Model
{
    public class TrelloCard : TrelloBaseEntity
    {
        public string Name { get; set; }
        public int IdShort { get; set; }
        public string ShortLink { get; set; }
    }
}
