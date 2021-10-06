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
        List<string> _possibleErrors;

        public CardPlanStrategy()
        {
            _possibleErrors = new List<string>();
        }

        public async Task<List<string>> ProcessCard(ICard card)
        {
            CalculationResult result = new CalculationResult();

            var comments = card.Comments;
            await comments.Refresh();
            if (comments.Count() == 0)
            {
                _possibleErrors.Add("Нет комментариев");
                return _possibleErrors;
            }
            string cardDescription = card.Description;
            result.StartMoney = GetStartMoney(cardDescription);
            result.CurrentWeek = GetCurrentWeek(comments.Count());

            //todo: group comments by weeks!
            foreach (var comment in comments)
            {
                string commentContent = comment.Data.Text;
                var tasks = GetTasks(commentContent);
                result.TasksDone += tasks.Item1;
                result.TasksCount += tasks.Item2;

                var pomodoros = GetPomodoros(commentContent);
                result.PomodoroDone += pomodoros.Item1;
                result.PomodoroCount += pomodoros.Item2;

                int money = GetMoney(commentContent);
                result.SpentMoney += money; 

                result.AddHours(GetAllHours(commentContent));
            }
            string newDescription = GetUpdatedReportDescription(cardDescription, result);
            if (!string.IsNullOrEmpty(cardDescription) && !string.IsNullOrEmpty(newDescription))
                card.Description = newDescription;

            return _possibleErrors;
        }

        private int GetStartMoney(string description)
        {
            var startMoney = description.Split("---")[0].Split("-")[1].Trim();
            int.TryParse(startMoney, out int result);
            return result;
        }

        private int GetCurrentWeek(int commentsCount)
        {
            int currentWeek = 1;

            if (commentsCount > 28)
                currentWeek = 5;
            else if (commentsCount > 21)
                currentWeek = 4;
            else if (commentsCount > 14)
                currentWeek = 3;
            else if (commentsCount > 7)
                currentWeek = 2;

            return currentWeek;
        }

        private Tuple<int, int> GetTasks(string comment)
        {
            var tasks = GetKeyValuesFromComment(comment, "- Done tasks -", "- Pomodoros -").Split("/");
            int.TryParse(tasks[0], out int tasksDone);
            int.TryParse(tasks[1], out int tasksCount);

            return new Tuple<int, int>(tasksDone, tasksCount);
        }
        
        private Tuple<int, int> GetPomodoros(string comment)
        {
            var pomodoros = GetKeyValuesFromComment(comment, "- Pomodoros -", "- Hours -").Split("/");
            int.TryParse(pomodoros[0], out int pomodorosDone);
            int.TryParse(pomodoros[1], out int pomodorosCount);

            return new Tuple<int, int>(pomodorosDone, pomodorosCount);
        }

        private int GetMoney(string comment)
        {
            var money = GetKeyValuesFromComment(comment, "- Money spent -", "- Finished at -");
            int.TryParse(money, out int result);

            return result;
        }

        private CalculationResult GetAllHours(string comment)
        {
            string totalHours = GetKeyValuesFromComment(comment, "- Screen time -", "- Messengers");
            string messengers = GetKeyValuesFromComment(comment, "- Messengers `-`", "- Browser `-`");
            string browser = GetKeyValuesFromComment(comment, "- Browser `-`", "- Youtube `-`");
            string youtube = GetKeyValuesFromComment(comment, "- Youtube `-`", "- Job `-`");
            string job = GetKeyValuesFromComment(comment, "- Job `-`", "- Money spent -");
            string endTime = GetKeyValuesFromComment(comment, "- Finished at -", "- **Комментарий**");

            var result = new CalculationResult();
            //Some processing. Todo: make global constants instead of "key word"
            return result;
        }
        
        private string GetUpdatedReportDescription(string oldDescription, CalculationResult result)
        {
            return "";
        }

        private string GetKeyValuesFromComment(string comment, string requestedKey, string nextKey)
        {
            var key1Index = comment.IndexOf(requestedKey);
            var key2Index = comment.IndexOf(nextKey);
            return comment.Substring(key1Index, key2Index - key1Index).Trim();
        }

        class CalculationResult
        {
            public int CurrentWeek { get; set; }
            public int StartMoney { get; set; }
            public int SpentMoney { get; set; }
            public int TasksCount { get; set; }
            public int TasksDone { get; set; }
            public int PomodoroCount { get; set; }
            public int PomodoroDone { get; set; }
            public TimeSpan Messengers { get; set; }
            public TimeSpan Browsers { get; set; }
            public TimeSpan Youtube { get; set; }
            public TimeSpan Jobs { get; set; }
            public TimeSpan TotalHours { get; set; }
            public TimeSpan EndHour { get; set; }

            internal void AddHours(CalculationResult phoneHours)
            {
                Messengers += phoneHours.Messengers;
                Browsers += phoneHours.Browsers;
                Youtube += phoneHours.Youtube;
                Jobs += phoneHours.Jobs;
                TotalHours += phoneHours.TotalHours;
            }
        }
    }
}
