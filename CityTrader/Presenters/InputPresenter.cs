using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Views;

namespace Presenters
{
    public class InputPresenter
    {
        private GameView view = new GameView();

        private bool hasOptionToTradeAll;

        public void Response(string prompt, int? maxQuantity, int low, int high, string warning, string message, out int? choice)
        {          
            choice = null;
            try
            {
                do
                {
                    view.Display(prompt);
                    //choice = int.Parse(Console.ReadLine());
                    //string userChoice = Console.ReadLine().ToLower();
                    //if (userChoice.Equals("a") || userChoice.Equals("all"))
                    //{
                    //    choice = maxQuantity;
                    //}
                    //else
                    //{
                    //    choice = int.Parse(userChoice);
                    //}
                    MaxQuantity(choice, maxQuantity, out choice);
                    RangeViolation(choice, low, high, warning);
                    Message(choice, message);
                } while ((choice < low) || (choice > high));
            }
            catch
            {
                view.Display("Thats not a number! Try again.");
                Response(prompt, maxQuantity, low, high, warning, message, out choice);
            }
        }

        public void LimitedUserInput(int? choice, int? maxQuantity)
        {
            if(hasOptionToTradeAll)
            {
                MaxQuantity(choice, maxQuantity, out choice);
            }
            else
            {
                choice = int.Parse(Console.ReadLine());
            }
        }

        public void MaxQuantity(int ? choice, int ? maxQuantity, out int? choice1)
        {
            string userChoice = Console.ReadLine().ToLower();
            if (userChoice.Equals("a") || userChoice.Equals("all"))
            {
                choice1 = maxQuantity;
            }
            else
            {
                choice1 = int.Parse(userChoice);
            }
        }

        public void RangeViolation(int? choice, int low, int high, string warning)
        {
            if ((choice < low) || (choice > high))
            {
                view.Display(warning);
            }
        }

        public void Message(int? choice, string message)
        {
            if (choice == 0)
            {
                view.Display(message);
            }
        }
    }
}