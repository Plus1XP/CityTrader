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
        private CityView view = new CityView();

        private int choice;

        private bool isChoiceConfirmed = false;

        public CityPresenter()
        {
            Update();            
        }

        public void Update()
        {
            Menu();

            do
            {
                SelectCity();
            } while (choice != 0 && isChoiceConfirmed == false);
        }

        private void SelectCity()
        {
            string _choice = Console.ReadLine();
            choice = int.Parse(_choice);
            switch(choice)
            {
                case 0:
                    choice = 0;
                    view.Display("Welcome back!");
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
                default:
                    view.Display("\nYou have entered an incorrect choice, press any key to continue.");
                    RefreshMenu();
                    break;
            }
        }

        private void TravelToCity(string city)
        {
            if(!city.Equals(PlayerModel.Instance.LocationName))
            {
                view.Display($"You have arrived at {city}");
                PlayerModel.Instance.LocationID = choice;
                PlayerModel.Instance.LocationName = city;
                PlayerModel.Instance.hasProductPriceUpdated = false;
                PlayerModel.Instance.isDayOver = true;
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

            view.Display("\n0 - Stay here!");

            view.Display("\nPlease select a City: ");
        }

    }
}
