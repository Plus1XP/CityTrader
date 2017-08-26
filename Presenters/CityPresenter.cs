﻿using System;
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

        private void Update()
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
            if(!city.Equals(view.CurrentCity))
            {
                view.Display($"You have arrived at {city}");
                view.CurrentCity = city;
                //UpdatePrices Method
                isChoiceConfirmed = true;
            }
            else
            {
                view.Display($"You are already at {city}, press any key to continue.");
                RefreshMenu();
            }
        }

        private void RefreshMenu()
        {
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        private void Menu()
        {
            //Day Details will replace the line below
            view.Display($"Day Details | City:{view.CurrentCity} \n");

            view.Display("Where would you like to travel to? \n");

            foreach (var city in model.GetAllCities())
            {
                view.Display($"{ city.CityID} - { city.CityName}");
            }

            view.Display("\n0 - Stay here!");

            view.Display("\nPlease Select a City: ");
        }

    }
}
