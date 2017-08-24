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

        private string choice;

        public string Message { get; private set; }

        private CityPresenter()
        {
            Update();
            SelectCity();
        }

        private void Update()
        {
            Menu();

            do
            {
                SelectCity();
            } while (!choice.Equals("Exit"));
        }

        private void SelectCity()
        {
            choice = Console.ReadLine();
            switch(choice)
            {
                case "London":
                    TravelToCity(choice);
                    break;
                case "Paris":
                    TravelToCity(choice);
                    break;
                case "Berlin":
                    TravelToCity(choice);
                    break;
                case "Madrid":
                    TravelToCity(choice);
                    break;
                case "Milan":
                    TravelToCity(choice);
                    break;
                case "New Yok":
                    TravelToCity(choice);
                    break;
                case "Tokyo":
                    TravelToCity(choice);
                    break;
                case "Hong Kong":
                    TravelToCity(choice);
                    break;
            }
        }

        private void TravelToCity(string city)
        {
            if(!choice.Equals(view.CurrentCity))
            {
                view.Display($"You have arrived at {city}");
                view.CurrentCity = choice;
            }
            else
            {
                view.Display($"You are already at {city}");
            }
        }

        private void Menu()
        {
            view.Display("Please type in the name of a city to travel to it ");

            foreach (var city in model.GetAllCities())
            {
                view.Display(city.CityName);
            }

            view.Display($"You are currently at {view.CurrentCity}");

            view.Display("Please Select a City to tavel to: ");
        }

    }
}
