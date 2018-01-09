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

        public void Response(string prompt, int? maxQuantity, int low, int high, string warning, string message, out int? choice)
        {
            choice = null;
            try
            {
                do
                {
                    view.Display(prompt);
                    AdaptiveUserInput(choice, maxQuantity, out choice);
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

        public void AdaptiveUserInput(int? choice, int? maxQuantity, out int? adaptiveChoice)
        {
            string userChoice = Console.ReadLine().ToLower();
            if (userChoice.Equals("a") || userChoice.Equals("all"))
            {
                adaptiveChoice = maxQuantity;
            }
            else
            {
                adaptiveChoice = int.Parse(userChoice);
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

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Views;

//namespace Presenters
//{
//    public interface IResponseOverride
//    {
//        void OverrideVariables();
//    }

//    public class InputPresenter : IResponseOverride
//    {
//        private GameView view = new GameView();

//        string OverrideVar1;
//        string OverrideVar2;

//        public void Response(string prompt, int? maxQuantity, int low, int high, string warning, string message, out int? choice)
//        {
//            choice = null;
//            try
//            {
//                do
//                {
//                    view.Display(prompt);
//                    AdaptiveUserInput(choice, maxQuantity, out choice);
//                    RangeViolation(choice, low, high, warning);
//                    Message(choice, message);
//                } while ((choice < low) || (choice > high));
//            }
//            catch
//            {
//                view.Display("Thats not a number! Try again.");
//                Response(prompt, maxQuantity, low, high, warning, message, out choice);
//            }
//        }

//        public void OverrideVariables()
//        {
//            OverrideVar1 = "a";
//            OverrideVar2 = "all";
//        }

//        public void AdaptiveUserInput(int? choice, int? executeInt, out int? adaptiveChoice)
//        {
//            string userChoice = Console.ReadLine().ToLower();
//            if (userChoice.Equals(OverrideVar1) || userChoice.Equals(OverrideVar2))
//            {
//                adaptiveChoice = executeInt;
//            }
//            else
//            {
//                adaptiveChoice = int.Parse(userChoice);
//            }
//        }

//        public void RangeViolation(int? choice, int low, int high, string warning)
//        {
//            if ((choice < low) || (choice > high))
//            {
//                view.Display(warning);
//            }
//        }

//        public void Message(int? choice, string message)
//        {
//            if (choice == 0)
//            {
//                view.Display(message);
//            }
//        }
//    }
//}