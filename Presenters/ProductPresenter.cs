using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Views;

namespace Presenters
{
    public class ProductPresenter
    {
        private ProductModel model = new ProductModel();
        private ProductView view = new ProductView();

        private int choice;
        //private int index;

        public ProductPresenter()
        {
            UpdatePrice();
            Update();
        }

        private void Update()
        {
            Menu();
            do
            {
                SelectProduct();
            } while (choice != 0);
        }

        public void UpdatePrice()
        {
            if (PlayerModel.Instance.hasProductPriceUpdated == false)
            {
                foreach (ProductModel product in model.GetAllProducts())
                {
                    product.UpdatePrice();
                    PlayerModel.Instance.hasProductPriceUpdated = true;
                }
            }
        }

        //private void SelectProduct()
        //{
        //    string _choice = Console.ReadLine();
        //    choice = int.Parse(_choice);
        //    index = choice;
        //    switch (choice)
        //    {
        //        case 0:
        //            choice = 0;
        //            view.Display("Come back soon!");
        //            break;
        //        case 1:                    
        //            VerifyProduct(index);
        //            break;

        //    }

        //}

        private void SelectProduct()
        {
            string _choice = Console.ReadLine();
            choice = int.Parse(_choice);
            model.FindProduct(choice);
            view.Display(model.Message);

        }

        private void RefreshMenu()
        {
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        private void Menu()
        {
            view.Display(PlayerModel.Instance.DayDetails());

            view.Display("What would you like to purchase? \n");

            foreach(var product in model.GetAllProducts())
            {
                string StockList = $"{product.ProductID} - {product.ProductName}: {product.Price:C}";
                string PriceMessage = $"{product.Message}";
                string Inventory = $"| Inventory: {product.Quantity}";
                view.Display($"{StockList,-40} {PriceMessage,-25} {Inventory,20}");
            }

            view.Display("\n0 - Cancel Transaction!");

            view.Display("\nPlease select a Product");
        }
    }
}
