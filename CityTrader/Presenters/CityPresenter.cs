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
        private CityModel model = new CityModel();
        private GameView view = new GameView();
        private InputPresenter input = new InputPresenter();

        private int? choice; //Converting int? to int using .Value is this correct??

        private bool isChoiceConfirmed = false;

        public CityPresenter()
        {
            
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
            input.Response("Please select a city", 0, 8, "We only fly to cities 1-8, choose again.", "Welcome Back!", out choice);
            switch(choice)
            {
                case 0:
                    choice = 0;
                    //view.Display("Welcome back!");
                    break;
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
                default: //Not needed anymore!
                    view.Display("\nYou have entered an incorrect choice, press any key to continue.");
                    RefreshMenu();
                    break;
            }
        }

        private void TravelToCity(string city)
        {
            if(!city.Equals(PlayerModel.Instance.locationName))
            {
                view.Display($"You have arrived at {city}");
                PlayerModel.Instance.locationID = choice.Value; //Converting int? to int using .Value is this correct??
                PlayerModel.Instance.locationName = city;
                PlayerModel.Instance.hasProductPriceUpdated = false;
                PlayerModel.Instance.AddInterest();
                PlayerModel.Instance.isDayOver = true;
                PlayerModel.Instance.day++;
                isChoiceConfirmed = true;
            }
            else
            {
                view.Display($"You are already at {city}.");
                RefreshMenu();
            }
        }

        private void RefreshMenu()
        {

            view.Display("Press any key to continue.");
            Console.ReadKey();            
            Menu();
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

            //view.Display("\nPlease select a City: ");
        }

    }
}
