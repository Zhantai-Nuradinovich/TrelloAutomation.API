using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloAutomation.API.API.DataContracts
{
    /// <summary>
    /// Action datacontract summary to be replaced
    /// </summary>
    public class TrelloAction : TrelloBaseEntity
    {
        /// <summary>
        /// Id of creator
        /// </summary>
        public string IdMemberCreator { get; set; }
        public TrelloActionData Data { get; set; }
        public string Type { get; set; }
        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime Date { get; set; }
    }
    /// <summary>
    /// Represents the Data of TrelloAction
    /// </summary>
    public class TrelloActionData
    {
        public string Text { get; set; }
        public TrelloCard Card { get; set; }
        public TrelloBoard Board { get; set; }
        public TrelloList List { get; set; }
    }
}
