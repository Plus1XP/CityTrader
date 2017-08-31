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
        private MenuPresenter presenter = new MenuPresenter();

        private GameView view = new GameView();

        public GamePresenter()
        {
            Update();
        }

        private void Update()
        {
            while (PlayerModel.Instance.Day <= 30 && PlayerModel.Instance.Money >= 0 && !PlayerModel.Instance.hasQuitGame)
            {
                GameStart();
            }
            GameOver();
        }

        private void GameStart()
        {
            presenter.Update();
            AddInterest();
        }

        private void GameOver()
        {
            Console.Clear();
            if (PlayerModel.Instance.Day >= 30)
            {                
                view.Display("\nTimes up! \n");
            }
            if (PlayerModel.Instance.Money < 0)
            {
                view.Display("\nYour broke! \n");
            }
            if (PlayerModel.Instance.hasQuitGame)
            {
                view.Display("\nThanks for playing! \n");
            }
            view.Display(PlayerModel.Instance.FinalScore());
            Console.ReadKey();
        }
        private void AddInterest()
        {
            if (!PlayerModel.Instance.isDebtPaid && PlayerModel.Instance.Day <= 30)
            {
                PlayerModel.Instance.AddInterest();
            }
        }
        private void LastDayWarning()
        {
            if (PlayerModel.Instance.Day == 29)
            {
                Console.Clear();
                view.Display("\nYou have 1 day left, make it count!");
                view.Display("Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}
