using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloAutomation.API.API.DataContracts
{
    /// <summary>
    /// Card datacontract summary to be replaced
    /// </summary>
    public class TrelloCard : TrelloBaseEntity
    {
        /// <summary>
        /// Card's name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ????
        /// </summary>
        public int IdShort { get; set; }
        /// <summary>
        /// Short link to the Card
        /// </summary>
        public string ShortLink { get; set; }
    }
}
