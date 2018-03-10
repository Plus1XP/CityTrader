namespace Models
{
    public class Debug
    {
        public string GetStatus()
        {            
            string playerStatus = Player.Instance.Status();
            string playerHiddenValues = Player.Instance.HiddenValues();
            string status = playerStatus + playerHiddenValues;
            return status;
        }

        public void RefreshProductPrices()
        {
            Player.Instance.HasProductPriceUpdated = false;
        }

        public void RemoveDebtFlag()
        {
            Player.Instance.IsDebtPaid = true;
        }

        public void AdjustDay(int? userChoice)
        {            
            Player.Instance.Day = userChoice.Value;
        }

        public void AdjustMoney(int? userChoice)
        {
            Player.Instance.Money = (decimal)userChoice.Value;
        }

        public string AdjustEXP(int? userChoice)
        {
            string message = Player.Instance.RewardExperienceGained((long)userChoice.Value);
            return message;
        }

        public void AdjustLevel(int? userChoice)
        {
            Player.Instance.Level = userChoice.Value;
        }

        public void AdjustStorage(int? userChoice)
        {
            Player.Instance.Storage = userChoice.Value;
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