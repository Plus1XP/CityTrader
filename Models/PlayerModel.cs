using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PlayerModel
    {

        public int Day { get; set; } = 1;
        public int Money { get; set; } = 1000;
        public int Loan { get; set; } = 1000;
        public int Score { get; set; }
        public int LocationID { get; set; } = 3;
        public string LocationName { get; set; } = "Berlin";
        public bool isDebtPaid { get; set; } = false;
        public bool isBuying { get; set; }
        public bool hasQuitGame { get; set; }
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

        }

        public string DayDetails()
        {
            string daydetails = $"Day:{Day} | City:{LocationName} | Money:{Money:C} | Loan:{Loan:C} \n";
            return daydetails;            
        }

        public void PayLoan()
        {
            Console.WriteLine("Your loan totals:{0:C} Would you like yo pay now? (y) or (n)", Loan);
            string response = Console.ReadLine();
            response = response.ToLower();
            if (response == "y")
            {
                Money -= Loan;
                Loan = 0;
                isDebtPaid = true;
            }
            else if (response == "n")
            {
                isDebtPaid = false;
            }
        }

        public void FinalScore()
        {
            Score = Money - Loan;
            Console.WriteLine("Final Score: {0:C}", Score);
            Console.ReadKey();
        }
    }
}
