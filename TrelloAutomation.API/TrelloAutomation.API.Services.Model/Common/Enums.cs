using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloAutomation.API.Services.Model.Common
{
    public enum TrelloErrorType
    {
        Board = 1,
        List = 2,
        Card = 3,
        Comment = 4,
        Date = 5
    }

    public enum TrelloBoardType
    {
        Plan = 1,
        Health = 2,
        Project = 3,
        SelfDevelopment = 4,
        Routine = 5,
        None = 6
    }
}
