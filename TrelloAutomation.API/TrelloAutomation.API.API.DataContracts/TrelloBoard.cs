using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloAutomation.API.API.DataContracts
{
    /// <summary>
    /// Board datacontract summary to be replaced
    /// </summary>
    public class TrelloBoard : TrelloBaseEntity
    {
        /// <summary>
        /// BoardName
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Short link to the board
        /// </summary>
        public string ShortLink { get; set; }
    }
}
