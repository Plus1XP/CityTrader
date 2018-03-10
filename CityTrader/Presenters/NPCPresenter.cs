namespace Presenters
{
    using System;

    using Models;
    using Views;

    public class NPCPresenter
    {
        private CustomsAgent customsAgent = new CustomsAgent();
        private GameView view = new GameView();

        public NPCPresenter()
        {            
        }

        public void Update()
        {            
        }

        public void CustomsNPC()
        {
            this.view.Display(this.customsAgent.Encounter());
            string playerResponse = Console.ReadLine().ToLower();
            this.view.Display(this.customsAgent.PlayerInteraction(playerResponse));
        }
    }
}
