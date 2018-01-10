using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models;
using Views;

namespace Presenters
{
    public class CityPresenter
    {
        private InputPresenter input = new InputPresenter();
        private GameView view = new GameView();

        private CityModel model = new CityModel();
        //private NPCModel customs = new CustomsAgent();
        private CustomsAgent customs = new CustomsAgent();

        private int? choice;

        private bool isChoiceConfirmed = false;

        public CityPresenter()
        {
            EventManager.Instance.OnRandomEncounter += NPCRNGSelection;
        }

        public void Update()
        {
            Menu();
            do
            {
                SelectCity();
            } while (choice != 0 && !isChoiceConfirmed);
        }

        private void SelectCity()
        {
            input.Response("Please select a city", null, 0, 8, "We only fly to cities 1-8, choose again.", "Welcome Back!", out choice);
            switch(choice)
            {
                case 1:
                    TravelToCity("London");
                    break;
                case 2:
                    TravelToCity("Paris");
                    break;
                case 3:
                    TravelToCity("Berlin");
                    break;
                case 4:
                    TravelToCity("Madrid");
                    break;
                case 5:
                    TravelToCity("Milan");
                    break;
                case 6:
                    TravelToCity("New York");
                    break;
                case 7:
                    TravelToCity("Tokyo");
                    break;
                case 8:
                    TravelToCity("Hong Kong");
                    break;
            }
        }

        private void TravelToCity(string city)
        {
            if(!city.Equals(PlayerModel.Instance.LocationName))
            {
                view.Display($"You have arrived at {city}");
                PlayerModel.Instance.LocationID = choice.Value;
                PlayerModel.Instance.LocationName = city;
                PlayerModel.Instance.hasProductPriceUpdated = false;
                PlayerModel.Instance.AddInterest();
                PlayerModel.Instance.isDayOver = true;
                PlayerModel.Instance.Day++;
                NPCRNGSelection();
                isChoiceConfirmed = true;
            }
            else
            {
                view.Display($"You are already at {city}.");
                RefreshMenu();
            }
        }

        public void NPCRNGSelection()
        {
            if (customs.NPCInteractionRate() == 0)
            {
                CustomsNPCEvent();
            }
        }

        public void CustomsNPCEvent()
        {
            do
            {
                CustomsNPCUserInteraction();
            } while (!customs.isUserInputValid);           
        }

        public void CustomsNPCUserInteraction()
        {
            string response;
            view.Display(customs.Encounter());
            response = Console.ReadLine().ToLower();
            view.Display(customs.PlayerInteraction(response));
        }

        private void RefreshMenu()
        {

            view.Display("Press any key to continue.");
            Console.ReadKey();            
            Update();
        }

        private void Menu()
        {
            Console.Clear();

            view.Display(PlayerModel.Instance.DayDetails());
            
            view.Display("Where would you like to travel to? \n");

            foreach (var city in model.GetAllCities())
            {
                view.Display($"{ city.CityID} - { city.CityName}");
            }

            view.Display("\n0 - Stay here! \n");
        }

    }
}
