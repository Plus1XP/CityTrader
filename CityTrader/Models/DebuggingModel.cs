using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presenters;
using Views;

namespace Models
{
    class DebuggingModel
    {        
        private InputPresenter input = new InputPresenter();
        private GameView view = new GameView();

        private int? choice;

        //private string message;

        public string DebugDetails()
        {            
            string dayDetails = PlayerModel.Instance.DayDetails();
            string hiddenValues = PlayerModel.Instance.HiddenValues();
            string DebugDetails = dayDetails + hiddenValues;
            return DebugDetails;
        }

        //public void AdjustDebt()
        //{
        //    RefreshDayDetails();
        //    input.Response("Enter amount between 1 - 1,000,000 \n0 To exit", null, 0, 1000000, "Invalid", "Exiting", out choice);
        //    PlayerModel.Instance.Debt += choice;
        //}

        public void AdjustDay()
        {
            input.Response("Enter amount between 1 - 30 \n0 To exit", null, 0, 30, "invalid", "Exiting", out choice);
            PlayerModel.Instance.Day = choice.Value;
        }

        public void AdjustMoney()
        {
            input.Response("Enter amount between 1 - 1,000,000,000 \nA - Max \n0 - To exit", 999999000, 0, 1000000000, "invalid", "Exiting", out choice);
            PlayerModel.Instance.Money += (decimal)choice;
        }

        public string AdjustEXP()
        {
            input.Response("Enter amount between 1 - 1,000,000,000 \n0 To exit", null, 0, 1000000000, "invalid", "Exiting", out choice);
            string message = PlayerModel.Instance.PlayerExperience((long)choice);
            return message;
        }

        public void AdjustLevel()
        {
            input.Response("Enter amount between 1 - 50 \n0 To exit", null, 0, 50, "invalid", "Exiting", out choice);
            PlayerModel.Instance.CurrentLevel = choice.Value;
        }

        public void AdjustStorage()
        {
            input.Response("Enter amount between 1 - 50000 \n0 To exit", null, 0, 50000, "invalid", "Exiting", out choice);
            PlayerModel.Instance.CurrentStorage = choice.Value;
        }
    }
}
