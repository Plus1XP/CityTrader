namespace Presenters
{
    using System;

    using Models;
    using Views;

    public class DebuggingPresenter
    {
        private Debug debug = new Debug();
        private ProductPresenter productPresenter = new ProductPresenter();
        private NPCPresenter npcPresenter = new NPCPresenter();
        private DialoguePresenter menuChoice;
        private DialoguePresenter npcChoice;
        private GameView view = new GameView();
        
        private bool isMenuActive = true;

        public DebuggingPresenter()
        {
        }

        public void Update()
        {
            this.Menu();
            do
            {                
                this.SelectAction();
            } while (this.isMenuActive);
        }

        private void SelectAction()
        {
            this.menuChoice = new DialoguePresenter("select an option", 0, 10, "Invalid", "Exiting");
            switch (this.menuChoice.ShowDialogue())
            {
                case 0:
                    this.isMenuActive = false;
                    break;
                case 1:
                    this.debug.RefreshProductPrices();
                    this.RefreshMenu();
                    break;
                case 2:
                    this.debug.RemoveDebtFlag();
                    this.RefreshMenu();
                    break;
                case 3:
                    this.menuChoice = new DialoguePresenter("Enter amount between 1 - 30", 1, 30, "invalid");
                    this.debug.Day = this.menuChoice.ShowDialogue();
                    this.RefreshMenu();
                    break;
                case 4:
                    this.menuChoice = new DialoguePresenter("Enter amount between 0 - 1,000,000,000 \nA - Max", 0, 1000000000, "invalid", null, 1000000000, "a", "all");
                    this.debug.Money = this.menuChoice.ShowDialogue();
                    this.RefreshMenu();
                    break;
                case 5:
                    this.menuChoice = new DialoguePresenter("Enter amount between 0 - 1,000,000,000", 0, 1000000000, "invalid");
                    this.view.Display(this.debug.AdjustEXP(this.menuChoice.ShowDialogue()));
                    this.RefreshMenu();
                    break;
                case 6:
                    this.menuChoice = new DialoguePresenter("Enter amount between 0 - 50", 0, 50, "invalid");
                    this.debug.Level = this.menuChoice.ShowDialogue();
                    this.RefreshMenu();
                    break;
                case 7:
                    this.menuChoice = new DialoguePresenter("Enter amount between 0 - 50000", 0, 50000, "invalid");
                    this.debug.Storage = this.menuChoice.ShowDialogue();
                    this.RefreshMenu();
                    break;
                case 8:
                    this.debug.SetBuyingFlag();
                    this.productPresenter.Update();
                    this.RefreshMenu();
                    break;
                case 9:
                    this.debug.SetSellingFlag();
                    this.productPresenter.Update();
                    this.RefreshMenu();
                    break;
                case 10:
                    this.NPCMenu();
                    this.RefreshMenu();
                    break;
            }
        }

        private void NPCMenu()
        {
            Console.Clear();
            this.view.Display("1 - Customs NPC");
            this.view.Display("0 - Exit \n");
            this.SelectNPC();
        }

        private void SelectNPC()
        {
            this.npcChoice = new DialoguePresenter("Select an NPC", 0, 1, "Invalid", "Exiting");
            switch (this.npcChoice.ShowDialogue())
            {
                case 1:
                    this.npcPresenter.CustomsNPC();
                    break;
            }
        }

        private void RefreshMenu()
        {
            this.view.Display("Press any key to continue");
            Console.ReadKey();
            this.Menu();
        }

        private void Menu()
        {
            Console.Clear();

            this.view.Display(this.debug.Status);

            this.view.Display("Debugging Tools \n");

            this.view.Display("1 - Refresh Prices");
            this.view.Display("2 - Remove Debt Flag");
            this.view.Display("3 - Adjust Day");
            this.view.Display("4 - Adjust Money");
            this.view.Display("5 - Adjust Exp (+)");
            this.view.Display("6 - Adjust Level");
            this.view.Display("7 - Adjust Storage");
            this.view.Display("8 - Test Buying");
            this.view.Display("9 - Test Selling");
            this.view.Display("10 - Encounter NPC");

            this.view.Display("0 - Exit \n");
        }
    }
}