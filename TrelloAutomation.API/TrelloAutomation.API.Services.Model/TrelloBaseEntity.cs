using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloAutomation.API.Services.Model
{
    /// <summary>
    /// Base datacontract summary to be replaced
    /// </summary>
    public class TrelloBaseEntity
    {
        /// <summary>
        /// Id of entity
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// BaseUrl
        /// </summary>
        public string BaseUrl { get; set; } = "https://api.trello.com/1";
    }
}
