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
        public int CurrentLevel = 1;
        public int CurrentStorage { get; set; } = 1000;
        public int LocationID { get; set; } = 1;
        public string LocationName { get; set; } = "London";
        public bool isDebtPaid { get; set; } = false;
        public bool isBuying { get; set; }
        public bool hasQuitGame { get; set; }
        public bool isDayOver { get; set; }
        public bool hasProductPriceUpdated { get; set; } = false;

        private int baseStorage = 1000;
        private const float loan = 1000;
        private float debt = 1000;
        private const float interestRate = 0.5f;
        private double currentExperience = 0;
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
            //EventManager.Instance.OnExperiencedGained += GainExperience;
        }

        public string DayDetails()
        {
            string dayDetails = $"Day:{Day} | City:{LocationName} | Money:{Money:C} | Debt:{debt:C} | Level:{CurrentLevel} \n";            
            return dayDetails;
        }

        public string HiddenValues()
        {
            string hiddenValues = $"Hidden Values | EXP:{currentExperience} | MAXEXP:{maxExperience} | EventChance: {eventChanceResults} \n";
            return hiddenValues;
        }

        public void AddInterest()
        {
            if (!PlayerModel.Instance.isDebtPaid && PlayerModel.Instance.Day < 30)
            {
                debt += loan * interestRate;
            }
        }

        public string PayLoan()
        {
            string message = $"Your debt totals:{debt:C} Would you like yo pay now? (y)es or (n)o";
            return message;
        }

        public void PayLoan(string response)
        {
            if (response.Equals("y") || response.Equals("yes"))
            {
                Money -= (decimal)debt;
                debt = 0;
                isDebtPaid = true;
            }
            else if (response.Equals("n") || response.Equals("no"))
            {
                isDebtPaid = false;
            }
        }

        public long GainExperience(int currentPrice, int oldPrice, int quantity)
        {
            //Dividing the Exp by 100 for more managable numbers. 
            //Also dividing by level to control over leveling. 
            //Also testing a long cast to prevent overflow exceptions.
            long experienceGained = ((currentPrice - oldPrice) * (long)quantity / 100 / CurrentLevel);
            return experienceGained;
        }

        public string PlayerExperience(long experienceGained)
        {
            string message1 = string.Empty;
            string message2 = string.Empty;
            currentExperience += experienceGained;
            while (currentExperience >= maxExperience)
            {
                message1 = LevelUp();
                message2 = IncreaseStorage();
            }
            return message1 + message2;
        }

        private string LevelUp()
        {
            CurrentLevel++;
            maxExperience *= (experienceModifier * CurrentLevel);
            string message = $"You have gain a level, you are now level {CurrentLevel} \n";
            return message;
        }

        private string IncreaseStorage()
        {
            CurrentStorage = CurrentLevel * baseStorage;
            string message = $"Your storage has increased, you can now store {CurrentStorage} of each item.";
            return message;
        }
        
        public string PlayerCheck(int currentPrice, int oldPrice, int quantity)
        {
            long experienceGained = GainExperience(currentPrice, oldPrice, quantity);
            string message = PlayerExperience(experienceGained);
            return message;
        }

        public override string ToString()
        {
            return $"You are current level {CurrentLevel} and you are {currentExperience} / {maxExperience}";
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
