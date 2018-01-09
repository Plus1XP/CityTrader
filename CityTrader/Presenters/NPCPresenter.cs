using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Views;

namespace Presenters
{
    public class NPCPresenter
    {
        private CustomsAgent customs = new CustomsAgent();
        private GameView view = new GameView();
        private InputPresenter input = new InputPresenter();

        public NPCPresenter()
        {
            
        }

        public void Update()
        {
            
        }

        public void CustomsNPC()
        {
            view.Display(customs.Encounter());
            string response = Console.ReadLine().ToLower();
            view.Display(customs.PlayerInteraction(response));
        }
    }
}
