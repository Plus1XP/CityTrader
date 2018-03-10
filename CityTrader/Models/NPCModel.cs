namespace Models
{
    public abstract class NPC
    {
        public NPC()
        {
        }

        public bool IsPlayerResponseValid { get; set; }

        protected string Name { get; set; }

        protected string EncounterMessage { get; set; }

        protected string PenaltyMessageLow { get; set; }

        protected string PenaltyMessageHigh { get; set; }

        protected int PenaltyPercentageLow { get; set; }

        protected int PenaltyPercentageHigh { get; set; }

        protected int EncounterRate { get; set; }

        public abstract string Cooperate();

        public abstract string CooperationMessage(decimal charge);

        public abstract string ReceivePenalty();

        public abstract string PenaltyMessage(decimal penalty);

        public abstract int InteractionRate();

        public string Encounter()
        {
            return this.EncounterMessage;
        }

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