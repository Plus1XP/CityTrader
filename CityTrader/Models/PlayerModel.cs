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
        public decimal Money { get; set; } = 1000;
        public int Level = 1;
        public int MaxStorage { get; set; } = 1000;
        public int LocationID { get; set; } = 1;
        public string LocationName { get; set; } = "London";
        public bool isDebtPaid { get; set; } = false;
        public bool isBuying { get; set; }
        public bool hasQuitGame { get; set; }
        public bool isDayOver { get; set; }
        public bool hasProductPriceUpdated { get; set; } = false;

        private int InitialStorage;
        private float loan = 1000;
        private float interest;
        private const float interestRate = 0.5f;
        private double experience = 0;
        private double maxExperience = 100;
        private float experienceModifier = 1.5f;
        private decimal score;

        //For hidden value
        public int eventChanceResults { get; set; }

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
            SetStorage();
            EventManager.Instance.OnExperiencedGained += GainExperience;

        }

        public string DayDetails()
        {
            string dayDetails = $"Day:{Day} | City:{LocationName} | Money:{Money:C} | Loan:{loan:C} | Level:{Level} \n";
            string hiddenValues = $"Hidden Values - EXP:{experience} | MAXEXP:{maxExperience} | EventChance: {eventChanceResults} \n";
            return dayDetails + hiddenValues;
        }

        public void SetInterestRate()
        {
            interest = loan * interestRate;
        }

        public void AddInterest()
        {
            if (!PlayerModel.Instance.isDebtPaid && PlayerModel.Instance.Day < 30)
            {
                loan += interest;
            }
        }

        public string PayLoan()
        {
            string message = $"Your loan totals:{loan:C} Would you like yo pay now? (y)es or (n)o";
            return message;
        }

        public void PayLoan(string response)
        {
            if (response.Equals("y") || response.Equals("yes"))
            {
                Money -= (decimal)loan;
                loan = 0;
                isDebtPaid = true;
            }
            else if (response.Equals("n") || response.Equals("no"))
            {
                isDebtPaid = false;
            }
        }

        public void GainExperience(long amount)
        {
            experience += amount;
            while (experience >= maxExperience)
            {
                LevelUp();               
            }
        }

        private string LevelUp()
        {
            Level++;
            maxExperience *= (experienceModifier * Level);
            //Message not displayed since changing from Console.WriteLine to Return.
            string message = $"You have gain a level, you are now level {Level}";
            return message;
        }

        public long ExperienceReward(int currentPrice, int oldPrice, int quantity)
        {
            //Dividing the Exp by 100 for more managable numbers. Also dividing by level to control over leveling. Also testing a long cast to prevent overflow exceptions.
            long productEXP = ((currentPrice - oldPrice) * (long)quantity / 100 / Level);
            return productEXP;
        }

        public void SetStorage()
        {
            InitialStorage = MaxStorage;
        }

        public void SetMaxStorage()
        {            
            MaxStorage = Level * InitialStorage;
            //string message = $"Your storage has increased!";
            //return message;
        }

        public override string ToString()
        {
            return $"You are current level {Level} and you are {experience} / {maxExperience}";
        }

        public string FinalScore()
        {
            score = Money - (decimal)loan;
            string message = $"\nFinal Score:{score:C}";
            return message;
        }

        //For hidden value.
        public int EventResults(int eventChance)
        {
            eventChanceResults = eventChance;
            return eventChanceResults;
        }

        //public int SetEventChance(int eventRate, int minimumEventChance)
        //{
        //    int eventChance;
        //    int playerLevelEventRate = (eventRate + 1) - Level;
        //    return eventChance = Math.Max(minimumEventChance, playerLevelEventRate);
        //}
    }
}
