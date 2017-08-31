using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PlayerModel
    {

        public int Day { get; set; } = 28;
        public int Money { get; set; } = 1000;
        public double Loan { get; set; } = 1000;
        public double Interest { get; private set; }
        private const double InterestRate = 0.5;
        private int Score { get; set; }
        public int LocationID { get; set; } = 3;
        public string LocationName { get; set; } = "Berlin";
        public bool isDebtPaid { get; set; } = false;
        public bool isBuying { get; set; }
        public bool hasQuitGame { get; set; }
        //public bool isDayOver { get; set; }
        public bool hasProductPriceUpdated { get; set; } = false;

        private static PlayerModel instance = null;

        public static PlayerModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerModel();
                }

                return instance;
            }
        }

        private PlayerModel()
        {
            SetInterestRate();
        }

        public string DayDetails()
        {
            string daydetails = $"Day:{Day} | City:{LocationName} | Money:{Money:C} | Loan:{Loan:C} \n";
            return daydetails;            
        }

        public void SetInterestRate()
        {
            Interest = Loan * InterestRate;
        }

        public void AddInterest()
        {
            Loan += Interest;
        }

        public string PayLoan()
        {
            string message = $"Your loan totals:{Loan:C} Would you like yo pay now? (y) or (n)";
            return message;
        }

        public void PayLoan(string response)
        {
            if (response.Equals("y"))
            {
                Money -= Convert.ToInt32(Loan);
                Loan = 0;
                isDebtPaid = true;
            }
            else if (response.Equals("n"))
            {
                isDebtPaid = false;
            }
        }

        public string FinalScore()
        {
            string message;
            Score = Money - Convert.ToInt32(Loan);
            message = $"Final Score:{Score:C}";
            return message;
        }
    }
}
