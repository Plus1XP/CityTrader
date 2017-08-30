using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Views;

namespace Presenters
{
    public class GamePresenter
    {
        private MenuPresenter presenter;

        private GameView view = new GameView();

        public GamePresenter()
        {
            Update();
        }

        private void Update()
        {
            Mechanics();
        }

        private void Mechanics()
        {
            for (; PlayerModel.Instance.Day <= 30; PlayerModel.Instance.Day++)
            {
                while (PlayerModel.Instance.Money >= 0 && PlayerModel.Instance.Day <= 30 && PlayerModel.Instance.hasQuitGame == false)
                {
                    presenter = new MenuPresenter();
                    break;
                }
                if (PlayerModel.Instance.Day == 30)
                {
                    view.Display("\nTimes up! \n");
                    break;
                }
                if (PlayerModel.Instance.Money < 0)
                {
                    view.Display("\nYour broke! \n");
                    break;
                }
                if (PlayerModel.Instance.hasQuitGame == true)
                {
                    view.Display("\nThanks for playing! \n");
                    break;
                }
                if (PlayerModel.Instance.isDebtPaid == false)
                {
                    PlayerModel.Instance.Loan += PlayerModel.Instance.Interest;
                }
            }
            view.Display(PlayerModel.Instance.FinalScore());
            Console.ReadKey();
        }
    }
}
