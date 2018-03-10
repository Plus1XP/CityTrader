namespace Presenters
{
    using System;

    using Models;
    using Views;

    public class CityPresenter
    {       
        private City city = new City();
        private CustomsAgent customsAgent = new CustomsAgent();
        private DialoguePresenter cityChoice;
        private GameView view = new GameView();

        private bool isMenuActive = true;
        private bool isCitySelected = false;

        public CityPresenter()
        {
            EventManager.Instance.OnRandomEncounter += this.SelectNPCEvent;
        }

        public void Update()
        {
            this.Menu();
            do
            {
                this.SelectCity();
            } while (this.isMenuActive && !this.isCitySelected);
        }

        private void SelectCity()
        {
            this.cityChoice = new DialoguePresenter("Please select a city", 0, 8, "We only fly to cities 1-8, choose again.", "Welcome Back!");
            switch (this.cityChoice.ShowDialogue())
            {
                case 0:
                    this.isMenuActive = false;
                    break;
                case 1:
                    this.TravelToCity("London");
                    break;
                case 2:
                    this.TravelToCity("Paris");
                    break;
                case 3:
                    this.TravelToCity("Berlin");
                    break;
                case 4:
                    this.TravelToCity("Madrid");
                    break;
                case 5:
                    this.TravelToCity("Milan");
                    break;
                case 6:
                    this.TravelToCity("New York");
                    break;
                case 7:
                    this.TravelToCity("Tokyo");
                    break;
                case 8:
                    this.TravelToCity("Hong Kong");
                    break;
            }
        }

        private void TravelToCity(string city)
        {
            if (!city.Equals(Player.Instance.Location))
            {
                this.view.Display($"You have arrived at {city}");
                Player.Instance.Location = city;
                Player.Instance.HasProductPriceUpdated = false;
                Player.Instance.AddDailyInterest();
                Player.Instance.IsDayOver = true;
                Player.Instance.Day++;
                this.SelectNPCEvent();
                this.isCitySelected = true;
            }
            else
            {
                this.view.Display($"You are already at {city}.");
                this.RefreshMenu();
            }
        }

        private void SelectNPCEvent()
        {
            if (this.customsAgent.InteractionRate() == 0)
            {
               this.InitiateCustomsAgentEvent();
            }
        }

        private void InitiateCustomsAgentEvent()
        {
            do
            {
                this.RespondToCustomsAgent();
            } while (!this.customsAgent.IsPlayerResponseValid);           
        }

        private void RespondToCustomsAgent()
        {
            string playerResponse;
            this.view.Display(this.customsAgent.Encounter());
            playerResponse = Console.ReadLine().ToLower();
            this.view.Display(this.customsAgent.PlayerInteraction(playerResponse));
        }

        private void RefreshMenu()
        {
            this.view.Display("Press any key to continue.");
            Console.ReadKey();            
            this.Update();
        }

        private void Menu()
        {
            Console.Clear();

            this.view.Display(Player.Instance.Status());

            this.view.Display("Where would you like to travel to? \n");

            foreach (var city in city.GetAllCities())
            {
                this.view.Display($"{ city.ID} - { city.Name}");
            }

            this.view.Display("\n0 - Stay here! \n");
        }
    }
}
