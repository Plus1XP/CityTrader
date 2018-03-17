namespace Models
{
    public class Debug
    {
        public string Status
        {
            get
            {
                return Player.Instance.Status + Player.Instance.HiddenValues;
            }
        }

        public int Day
        {
            set
            {
                Player.Instance.Day = value;
            }
        }

        public decimal Money
        {
            set
            {
                Player.Instance.Money = (decimal)value;
            }
        }

        public int Level
        {
            set
            {
                Player.Instance.Level = value;
            }
        }

        public int Storage
        {
            set
            {
                Player.Instance.Storage = value;
            }
        }

        public string AdjustEXP(int? userChoice)
        {
            string message = Player.Instance.RewardExperienceGained((long)userChoice.Value);
            return message;
        }

        public void RefreshProductPrices()
        {
            Player.Instance.HasProductPriceUpdated = false;
        }

        public void RemoveDebtFlag()
        {
            Player.Instance.IsDebtPaid = true;
        }

        public void SetBuyingFlag()
        {
            Player.Instance.IsBuying = true;
        }

        public void SetSellingFlag()
        {
            Player.Instance.IsBuying = false;
        }

        public void EncounterNPC()
        {            
        }
    }
}