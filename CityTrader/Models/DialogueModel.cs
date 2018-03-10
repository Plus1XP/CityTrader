namespace Models
{
    public class Dialogue
    {
        public Dialogue()
        {
        }

        public string Prompt { get; set; }

        public string Message { get; private set; }

        public string ExitMessage { get; set; }

        public string ErrorMessage { get; private set; }

        public string RangeWarningMessage { get; set; }

        public string TextCommandOverride1 { get; set; }

        public string TextCommandOverride2 { get; set; }

        public int MinimumMenuChoice { get; set; }

        public int MaximumMenuChoice { get; set; }

        public int? CommandOverrideMenuChoice { get; set; }

        public string GetPrompt()
        {
            return this.Prompt;
        }

        public string GetMessage()
        {
            return this.Message;
        }

        public string GetErrorMessage()
        {
            return this.ErrorMessage;
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
