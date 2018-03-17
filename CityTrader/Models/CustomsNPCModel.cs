namespace Models
{
    public class CustomsAgent : NPC
    {
        public CustomsAgent() : base()
        {
            this.name = "TSA";
            this.penaltyPercentageLow = 20;
            this.penaltyPercentageHigh = 50;
            this.EncounterMessage = "Do you have anything to declare? \n(Y)es or (N)o?";
            this.encounterRate = 6;
        }

        public override int InteractionRate()
        {
            int randomNumber;
            randomNumber = RNGModel.RandomNumber.Next(0, this.encounterRate);
            return randomNumber;
        }

        public override string Cooperate()
        {
            string message = string.Empty;
            decimal customsCharge = this.CalculateCooperationPercentage();
            Player.Instance.Money -= customsCharge;
            message = this.CooperationMessage(customsCharge);
            return message;
        }

        public decimal CalculateCooperationPercentage()
        {
            decimal customsPercentage = (decimal)penaltyPercentageLow / 100;
            decimal customCharge = Player.Instance.Money * customsPercentage;
            return customCharge;
        }

        public override string CooperationMessage(decimal charge)
        {
            string message = $"Thank you for your cooperation! \nYou have been charged our lowest rate {charge:C}";
            return message;
        }

        public override string ReceivePenalty()
        {
            string message = string.Empty;
            decimal finalPenalty = this.CalculatePenaltyPercentage();
            Player.Instance.Money -= finalPenalty;
            message = this.PenaltyMessage(finalPenalty);
            return message;
        }

        public decimal CalculatePenaltyPercentage()
        {
            int randomPenalty = RNGModel.RandomNumber.Next(penaltyPercentageLow + 5, penaltyPercentageHigh + 1);

            // <field name="randomPenalty">Cast to a decimal instead of using "100.0" otherwise the equation will think its an int.</field>
            decimal penaltyPercentage = (decimal)randomPenalty / 100;

            decimal penaltyCharge = Player.Instance.Money * penaltyPercentage;
            return penaltyCharge;
        }

        public override string PenaltyMessage(decimal penalty)
        {
            string message = $"You have been randomly searched. You have not declared your items! \nYou have been fined {penalty:C}";
            return message;
        }
    }
}
