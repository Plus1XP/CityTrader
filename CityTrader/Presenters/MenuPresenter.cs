namespace Presenters
{
    using System;

    using Models;
    using Views;

    public class MenuPresenter
    {
        private readonly CityPresenter cityPresenter = new CityPresenter();
        private readonly ProductPresenter productPresenter = new ProductPresenter();
        private readonly DebuggingPresenter debugPresenter = new DebuggingPresenter();
        private readonly GameView view = new GameView();
        private DialoguePresenter menuChoice;

        public void Update()
        {
            this.Menu();
            do
            {
                Player.Instance.IsDayOver = false;
                this.SelectAction();
            } while (!Player.Instance.HasQuitGame && !Player.Instance.IsDayOver);
        }

        private void SelectAction()
        {
            this.menuChoice = new DialoguePresenter("Please select an activity", 0, 6, "More features to come soon!For now choose from 1 - 5", null, 6, "let me in");
            switch (this.menuChoice.ShowDialogue())
            {
                case 0:
                    Player.Instance.HasQuitGame = true;
                    break;
                case 1:
                    this.cityPresenter.Update();
                    this.RefreshMenu();
                    break;
                case 2:
                    Player.Instance.IsBuying = true;
                    this.productPresenter.Update();
                    this.RefreshMenu();
                    break;
                case 3:
                    Player.Instance.IsBuying = false;
                    this.productPresenter.Update();
                    this.RefreshMenu();
                    break;
                case 4:
                    this.productPresenter.ProductInventory();
                    this.RefreshMenu();
                    break;
                case 5:
                    this.PayLoan();
                    this.RefreshMenu();
                    break;
                case 6:
                    // <switch name="case 6">Used for debug menu, if disabled remove override choice 6 from "menuChoice.ShowDialogue".</switch>
                    this.debugPresenter.Update();
                    this.RefreshMenu();
                    break;
            }
        }

        private void PayLoan()
        {
            Console.Clear();
            this.view.Display(Player.Instance.Status());
            this.view.Display(Player.Instance.PayLoan());
            var playerResponse = Console.ReadLine().ToLower();
            Player.Instance.PayLoan(playerResponse);
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

            this.view.Display(Player.Instance.Status());

            this.view.Display("What would you like to do? \n");

            this.view.Display("1 - Change City");
            this.view.Display("2 - Buy merchandise");
            this.view.Display("3 - Sell merchandise");
            this.view.Display("4 - View inventory");
            this.view.Display("5 - Repay loan \n");
            ////view.Display("6 - Debugging Tools \n");

            this.view.Display("0 - Quit \n");
        }
    }
}