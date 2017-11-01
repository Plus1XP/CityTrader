using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Views;

namespace Presenters
{
    public class MenuPresenter
    {
        private CityPresenter citypresenter = new CityPresenter();
        private ProductPresenter productpresenter = new ProductPresenter();
        private InputPresenter input = new InputPresenter();

        private DebuggingPresenter debugging = new DebuggingPresenter();

        private GameView view = new GameView();

        private int? choice;

        public MenuPresenter()
        {

        }

        public void Update()
        {
            Menu();
            do
            {
                PlayerModel.Instance.isDayOver = false;
                SelectAction();
            } while (choice != 0 && PlayerModel.Instance.isDayOver == false);
        }

        public void SelectAction()
        {
            input.Response("Please select an activity", null, 0, 6, "More features to come soon! For now choose from 1-5", null, out choice);
            switch (choice)
            {
                case 0:
                    PlayerModel.Instance.hasQuitGame = true;
                    break;
                case 1:
                    citypresenter.Update();
                    RefreshMenu();
                    break;
                case 2:
                    PlayerModel.Instance.isBuying = true;
                    productpresenter.Update();
                    RefreshMenu();
                    break;
                case 3:
                    PlayerModel.Instance.isBuying = false;
                    productpresenter.Update();
                    RefreshMenu();
                    break;
                case 4:
                    productpresenter.ProductInventory();
                    RefreshMenu();
                    break;
                case 5:
                    PayLoan();
                    RefreshMenu();
                    break;
                case 6:
                    //If disabled remove option 6 from input.response
                    debugging.Update();
                    RefreshMenu();
                    break;
            }
        }

        private void PayLoan()
        {
            Console.Clear();
            view.Display(PlayerModel.Instance.DayDetails());
            view.Display(PlayerModel.Instance.PayLoan());
            string response = Console.ReadLine().ToLower();
            PlayerModel.Instance.PayLoan(response);
        }

        private void RefreshMenu()
        {
            view.Display("Press any key to continue");
            Console.ReadKey();
            Menu();
        }

        private void Menu()
        {
            Console.Clear();

            view.Display(PlayerModel.Instance.DayDetails());

            view.Display("What would you like to do? \n");

            view.Display("1 - Change City");
            view.Display("2 - Buy merchandise");
            view.Display("3 - Sell merchandise");
            view.Display("4 - View inventory");
            view.Display("5 - Repay loan \n");
            view.Display("6 - Debugging Tools \n");

            view.Display("0 - Quit \n");
        }
    }
}
