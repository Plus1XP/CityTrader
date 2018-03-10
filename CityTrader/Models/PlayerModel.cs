namespace Models
{
    public class Player
    {
        private const float Loan = 1000;
        private const float InterestRate = 0.5f;

        private static Player instance = null;

        private float debt = 1000;
        private float experienceModifier = 1.5f;

        private double currentExperiencePoints = 0;
        private double maxExperiencePoints = 100;

        private decimal score;

        private int baseStorage = 1000;

        // <field name="eventChanceResults">Used for debugging, hidden value from "ProductModel" to "Player.HiddenValues()".</field>
        private int eventChanceResults;

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }

                return instance;
            }
        }

        public string Location { get; set; } = "London";

        public int Day { get; set; } = 1;

        public int Level { get; set; } = 1;

        public int Storage { get; set; } = 1000;

        public decimal Money { get; set; } = 1000;

        public bool IsDebtPaid { get; set; } = false;

        public bool IsBuying { get; set; }

        public bool HasQuitGame { get; set; }

        public bool IsDayOver { get; set; }

        public bool HasProductPriceUpdated { get; set; } = false;

        public string Status()
        {
            string dayDetails = $"Day:{Day} | City:{Location} | Money:{Money:C} | Debt:{debt:C} | Level:{Level} \n";            
            return dayDetails;
        }

        public string HiddenValues()
        {
            string hiddenValues = $"Hidden Values | EXP:{currentExperiencePoints} | MAXEXP:{maxExperiencePoints} | EventChance: {eventChanceResults} \n";
            return hiddenValues;
        }

        // <method name="EventResults">Used for debugging, sets hidden value from "ProductModel".</method>
        public void EventResults(int eventChance)
        {
            this.eventChanceResults = eventChance;
        }

        public string FinalScore()
        {
            this.score = this.Money - (decimal)Loan;
            string message = $"\nFinal Score:{score:C}";
            return message;
        }

        public void AddDailyInterest()
        {
            if (!Player.Instance.IsDebtPaid && Player.Instance.Day < 30)
            {
                this.debt += Loan * InterestRate;
            }
        }

        public string PayLoan()
        {
            string message = $"Your debt totals:{debt:C} Would you like yo pay now? (y)es or (n)o";
            return message;
        }

        public void PayLoan(string playerResponse)
        {
            if (playerResponse.Equals("y") || playerResponse.Equals("yes"))
            {
                this.Money -= (decimal)this.debt;
                this.debt = 0;
                this.IsDebtPaid = true;
            }
            else if (playerResponse.Equals("n") || playerResponse.Equals("no"))
            {
                this.IsDebtPaid = false;
            }
        }

        public string AddExperiencePoints(int currentSalePrice, int previousSalePrice, int quantity)
        {
            long experienceGained = this.CalculateExperienceGained(currentSalePrice, previousSalePrice, quantity);
            string message = this.RewardExperienceGained(experienceGained);
            return message;
        }

        public long CalculateExperienceGained(int currentSalePrice, int previousSalePrice, int quantity)
        {
            // Dividing the Exp by 100 for more managable numbers. 
            // Also dividing by level to control over leveling. 
            // Also testing a long cast to prevent overflow exceptions.
            long experienceGained = (currentSalePrice - previousSalePrice) * (long)quantity / 100 / this.Level;
            return experienceGained;
        }

        public string RewardExperienceGained(long experienceGained)
        {
            string message1 = string.Empty;
            string message2 = string.Empty;
            this.currentExperiencePoints += experienceGained;

            while (this.currentExperiencePoints >= this.maxExperiencePoints)
            {
                message1 = this.LevelUp();
                message2 = this.IncreaseStorage();
            }

            return message1 + message2;
        }

        private string LevelUp()
        {
            this.Level++;
            this.maxExperiencePoints *= this.experienceModifier * this.Level;
            string message = $"You have gain a level, you are now level {Level} \n";
            return message;
        }

        private string IncreaseStorage()
        {
            this.Storage = this.Level * this.baseStorage;
            string message = $"Your storage has increased, you can now store {Storage} of each item.";
            return message;
        }       
    }
}
