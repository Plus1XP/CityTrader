namespace Models
{
    public class Player
    {
        // <field name="eventChance">Used for debugging, hidden value from "ProductModel" to "Player.HiddenValues()".</field>
        public int EventChance;

        public int Day = 1;
        public int Level = 1;
        public int Storage = 1000;

        public decimal Money = 1000;

        public string Location = "London";

        public bool HasProductPriceUpdated = false;
        public bool IsDebtPaid = false;
        public bool IsBuying;
        public bool HasQuitGame;
        public bool IsDayOver;

        private const float Loan = 1000;
        private const float InterestRate = 0.5f;

        private static Player instance = null;

        private float debt = 1000;
        private float exponent = 1.5f;

        private double experience = 0;
        private double nextLevelUp = 100;

        private int baseStorage = 1000;

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

        public string Status
        {
            get
            {
                return $"Day:{Day} | City:{Location} | Money:{Money:C} | Debt:{debt:C} | Level:{Level} \n";
            }
        }

        public string HiddenValues
        {
            get
            {
                return $"Hidden Values | EXP:{experience} | NextLevelUp:{nextLevelUp} | EventChance: {EventChance} \n";
            }
        }

        public decimal Score
        {
            get
            {
                return this.Money - (decimal)Loan;
            }
        }

        public string FinalScore()
        {
            string message = $"\nFinal Score:{Score:C}";
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

        public string AddExperiencePoints(int currentPrice, int purchasePrice, int quantity)
        {
            long experienceGained = this.CalculateExperienceGained(currentPrice, purchasePrice, quantity);
            string message = this.RewardExperienceGained(experienceGained);
            return message;
        }

        public long CalculateExperienceGained(int currentPrice, int purchasePrice, int quantity)
        {
            // Dividing the Exp by 100 for more managable numbers. 
            // Also dividing by level to control over leveling. 
            // Also testing a long cast to prevent overflow exceptions.
            long experienceGained = (currentPrice - purchasePrice) * (long)quantity / 100 / this.Level;
            return experienceGained;
        }

        public string RewardExperienceGained(long experienceGained)
        {
            string message1 = string.Empty;
            string message2 = string.Empty;
            this.experience += experienceGained;
            
            while (this.experience >= this.nextLevelUp)
            {
                message1 = this.LevelUp();
                message2 = this.IncreaseStorage();
            }

            return message1 + message2;
        }

        private string LevelUp()
        {
            this.Level++;
            this.nextLevelUp *= this.Level * this.exponent;            
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
