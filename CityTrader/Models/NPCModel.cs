namespace Models
{
    public abstract class NPC
    {
        public bool IsPlayerResponseValid;

        public string EncounterMessage;

        protected string name;

        protected string penaltyMessageLow;

        protected string penaltyMessageHigh;

        protected int penaltyPercentageLow;

        protected int penaltyPercentageHigh;

        protected int encounterRate;

        public NPC()
        {
        }

        public abstract string Cooperate();

        public abstract string CooperationMessage(decimal charge);

        public abstract string ReceivePenalty();

        public abstract string PenaltyMessage(decimal penalty);

        public abstract int InteractionRate();

        public string PlayerInteraction(string playerResponse)
        {            
            string message = string.Empty;

            if (playerResponse.Equals("y") || playerResponse.Equals("yes"))
            {
                message = this.Cooperate();
                this.IsPlayerResponseValid = true;
            }
            else if (playerResponse.Equals("n") || playerResponse.Equals("no"))
            {
                message = this.TryToEscape();
                this.IsPlayerResponseValid = true;
            }            
            else
            {
                message = "Invalid Response";
                this.IsPlayerResponseValid = false;
            }

            return message;
        }

        private string TryToEscape()
        {
            string message = string.Empty;

            int escapeOutcome = RNGModel.RandomNumber.Next(0, 2);

            if (escapeOutcome == 0)
            {
                message = this.EscapeMessage();
            }
            else if (escapeOutcome == 1)
            {
                message = this.ReceivePenalty();
            }

            return message;
        }

        private string EscapeMessage()
        {
            string message = "You got away";
            return message;
        }
    }
}