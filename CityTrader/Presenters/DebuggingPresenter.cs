using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Views;

namespace Presenters
{
    class DebuggingPresenter
    {
        private InputPresenter input = new InputPresenter();
        private GameView view = new GameView();

        private DebuggingModel debuggingmodel = new DebuggingModel();
        private ProductPresenter productpresenter = new ProductPresenter();

        private int? choice;

        public DebuggingPresenter()
        {

        }

        public void Update()
        {
            Menu();
            do
            {                
                SelectAction();
            } while (choice != 0);
        }

        public void SelectAction()
        {
            input.Response("Select an option", null, 0, 10, "Invalid", "Exiting", out choice);
            switch (choice)
            {
                case 1:
                    PlayerModel.Instance.hasProductPriceUpdated = false;
                    RefreshMenu();
                    break;
                case 2:
                    PlayerModel.Instance.isDebtPaid = true;
                    RefreshMenu();
                    break;
                case 3:
                    debuggingmodel.AdjustDay();
                    RefreshMenu();
                    break;
                case 4:
                    debuggingmodel.AdjustMoney();
                    RefreshMenu();
                    break;
                case 5:
                    view.Display(debuggingmodel.AdjustEXP());
                    RefreshMenu();
                    break;
                case 6:
                    debuggingmodel.AdjustLevel();
                    RefreshMenu();
                    break;
                case 7:
                    debuggingmodel.AdjustStorage();
                    RefreshMenu();
                    break;
                case 8:
                    PlayerModel.Instance.isBuying = true;
                    productpresenter.Update();
                    RefreshMenu();
                    break;
                case 9:
                    PlayerModel.Instance.isBuying = false;
                    productpresenter.Update();
                    RefreshMenu();
                    break;
                case 10:
                    
                    RefreshMenu();
                    break;

            }
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

            view.Display(debuggingmodel.DebugDetails());

            view.Display("Debugging Tools \n");

            view.Display("1 - Refresh Prices");
            view.Display("2 - Remove Debt Flag");
            view.Display("3 - Adjust Day");
            view.Display("4 - Adjust Money");
            view.Display("5 - Adjust Exp (+)");
            view.Display("6 - Adjust Level");
            view.Display("7 - Adjust Storage");
            view.Display("8 - Buy");
            view.Display("9 - Sell");
            view.Display("10 - Test Method");

            view.Display("0 - Exit \n");
        }
    }
}
