using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PlayerModel
    {

        public int day { get; set; } = 1;
        public int money { get; set; } = 1000; // Chanage to something bigger
        public double loan { get; set; } = 1000;
        public double interest { get; private set; }
        private const double interestRate = 0.5;
        private long score { get; set; }
        public int locationID { get; set; } = 1;
        public string locationName { get; set; } = "London";
        public bool isDebtPaid { get; set; } = false;
        public bool isBuying { get; set; }
        public bool hasQuitGame { get; set; }
        public bool isDayOver { get; set; }
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
            string daydetails = $"Day:{day} | City:{locationName} | Money:{money:C} | Loan:{loan:C} \n";
            return daydetails;            
        }

        public void SetInterestRate()
        {
            interest = loan * interestRate;
        }

        public void AddInterest()
        {
            if (!PlayerModel.Instance.isDebtPaid && PlayerModel.Instance.day < 30)
            {
                loan += interest;
            }            
        }

        public string PayLoan()
        {
            string message = $"Your loan totals:{loan:C} Would you like yo pay now? (y) or (n)";
            return message;
        }

        public void PayLoan(string response)
        {
            if (response.Equals("y"))
            {
                money -= Convert.ToInt32(loan);
                loan = 0;
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
            score = money - Convert.ToInt32(loan);
            message = $"\nFinal Score:{score:C}";
            return message;
        }
    }
}
