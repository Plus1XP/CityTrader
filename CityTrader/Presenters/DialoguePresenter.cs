namespace Presenters
{
    using System;

    using Models;
    using Views;

    public class DialoguePresenter
    {
        private Dialogue dialogue = new Dialogue();
        private GameView view = new GameView();

        private int? dialogueResult;

        public DialoguePresenter(string prompt, int minimumChoice, int maximumChoice, string rangeWarning, string exitMessage = null, int? overrideChoice = null, string overrideString1 = null, string overrideString2 = null)
        {
            this.dialogue.Prompt = prompt;
            this.dialogue.MinimumMenuChoice = minimumChoice;
            this.dialogue.MaximumMenuChoice = maximumChoice;
            this.dialogue.RangeWarningMessage = rangeWarning;
            this.dialogue.ExitMessage = exitMessage;
            this.dialogue.CommandOverrideMenuChoice = overrideChoice;
            this.dialogue.TextCommandOverride1 = overrideString1;
            this.dialogue.TextCommandOverride2 = overrideString2;          
        }

        public DialoguePresenter()
        {
        }

        public int ShowDialogue()
        {
            try
            {
                do
                {
                    this.view.Display(this.dialogue.GetPrompt());
                    string userResponse = Console.ReadLine().ToLower();
                    this.dialogueResult = this.dialogue.SortResponse(userResponse);
                    this.DisplayMessage();
                } while ((this.dialogueResult < this.dialogue.MinimumMenuChoice) || (this.dialogueResult > this.dialogue.MaximumMenuChoice));
            }
            catch
            {
                this.view.Display(this.dialogue.GetErrorMessage());
                this.ShowDialogue();
            }

            return this.dialogueResult.Value;
        }

        public void DisplayMessage()
        {
            if (this.dialogue.GetMessage() != null)
            {
                this.view.Display(this.dialogue.GetMessage());
            }
        }
    }
}