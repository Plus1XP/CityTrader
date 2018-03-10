namespace Presenters
{
    using System;

    using Models;
    using Views;

    public class GamePresenter
    {
        private readonly MenuPresenter menuPresenter = new MenuPresenter();
        private readonly GameView view = new GameView();

        public GamePresenter()
        {
            this.Update();
        }

        private void Update()
        {
            while (Player.Instance.Day <= 30 && Player.Instance.Money >= 0 && !Player.Instance.HasQuitGame)
            {
                this.GameChecks();
                this.GameStart();
            }

            this.GameOver();
        }

        private void GameStart()
        {
            this.menuPresenter.Update();
        }

        private void GameOver()
        {
            Console.Clear();

            if (Player.Instance.Day >= 30)
            {
                this.view.Display("\nTimes up! \n");
            }

            if (Player.Instance.Money < 0)
            {
                this.view.Display("\nYour broke! \n");
            }

            if (Player.Instance.HasQuitGame)
            {
                this.view.Display("\nThanks for playing! \n");
            }

            this.view.Display(Player.Instance.FinalScore());
            Console.ReadKey();
        }

        private void GameChecks()
        {
            if (Player.Instance.Day == 29)
            {
                Console.Clear();
                this.view.Display("\nYou have 1 day left, make it count!");
                this.view.Display("Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}