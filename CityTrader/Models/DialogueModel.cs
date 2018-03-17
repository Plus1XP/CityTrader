namespace Models
{
    public class Dialogue
    {
        public string Prompt;
        public string Message;
        public string ExitMessage;
        public string ErrorMessage;
        public string RangeWarningMessage;
        public string TextCommandOverride1;
        public string TextCommandOverride2;

        public int MinimumMenuChoice;
        public int MaximumMenuChoice;

        public int? CommandOverrideMenuChoice;

        public Dialogue()
        {
            this.SetErrorMessage();
        }

        public int SortResponse(string userResponse)
        {
            int? dialogueResult = null;
            this.Message = null;

            if (userResponse.Equals(this.TextCommandOverride1))
            {
                dialogueResult = this.CommandOverrideMenuChoice;
                ////Console.CursorTop--;
            }
            else if (userResponse.Equals(this.TextCommandOverride2))
            {
                dialogueResult = this.CommandOverrideMenuChoice;
            }
            else
            {
                dialogueResult = int.Parse(userResponse);
                this.ValidateResponse(dialogueResult.Value);
            }

            return dialogueResult.Value;
        }

        private void ValidateResponse(int userChoice)
        {
            if ((userChoice < this.MinimumMenuChoice) || (userChoice > this.MaximumMenuChoice))
            {
                this.SetOutOfRangeMessage();
            }
            else if (userChoice == 0)
            {
                this.SetExitMessage();
            }
        }

        private void SetOutOfRangeMessage()
        {
            this.Message = this.RangeWarningMessage;
        }

        private void SetExitMessage()
        {
            this.Message = this.ExitMessage;           
        }

        private void SetErrorMessage()
        {
            this.ErrorMessage = "Thats not a number try again!";
        }
    }
}
