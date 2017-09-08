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

        public void Response(string prompt, int low, int high, string warning, string message, out int? choice)
        {
            choice = null; //Is a null ok??
            try
            {
                do
                {
                    view.Display(prompt);
                    choice = int.Parse(Console.ReadLine());
                    RangeViolation(choice, low, high, warning);
                    Message(choice, message);
                } while ((choice < low) || (choice > high));
            }
            catch
            {
                view.Display("Thats not a number! Try again.");
                Response(prompt, low, high, warning, message, out choice);
            }
        }

        public void Message(int? choice, string message)
        {
            if (choice == 0)
            {
                view.Display(message);
            }
        }

        public void RangeViolation(int? choice, int low, int high, string warning)
        {
            if ((choice < low) || (choice > high))
            {
                view.Display(warning);
            }
        }
    }
}