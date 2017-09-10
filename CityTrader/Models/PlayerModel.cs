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
        public decimal money { get; set; } = 1000;
        public double loan { get; set; } = 1000;
        public double interest { get; private set; }
        private const double interestRate = 0.5;
        public int locationID { get; set; } = 1;
        public string locationName { get; set; } = "London";
        public int Level { get; set; }
        public int Experience { get; set; } = 0;
        public float MaxExperience { get; set; } = 100;
        private float experienceModifier = 1.5f;
        private double score { get; set; }
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
            string daydetails = $"Day:{day} | City:{locationName} | Money:{money:C} | Loan:{loan:C} | Level:{Level} EXP:{Experience} MAXEXP:{MaxExperience}\n";
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
                money -= Convert.ToDecimal(loan);
                loan = 0;
                isDebtPaid = true;
            }
            else if (response.Equals("n"))
            {
                isDebtPaid = false;
            }
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
            while (Experience >= MaxExperience)
            {
                Level++;
                MaxExperience *= experienceModifier;
            }
        }

        //public void GainExperience(int amount)
        //{
        //    if ((Experience + amount) >= MaxExperience)
        //    {
        //        LevelUp();
        //    }

        //    Experience += amount;
        //}

        //private void LevelUp()
        //{
        //    Level++;
        //    MaxExperience *= experienceModifier;
        //}

        public string FinalScore()
        {
            string message;
            score = Convert.ToDouble(money) - loan;
            message = $"\nFinal Score:{score:C}";
            return message;
        }
    }
}
