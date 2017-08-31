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
        private GameView view = new GameView();

        private int choice;

        public ProductPresenter()
        {

        }

        public void Update()
        {
            UpdatePrice();
            Menu();
            do
            {
                SelectProduct();
            } while (choice != 0);
        }

        public void UpdatePrice()
        {
            if (!PlayerModel.Instance.hasProductPriceUpdated)
            {
                foreach (ProductModel product in model.products)
                {
                    product.UpdatePrice();
                    PlayerModel.Instance.hasProductPriceUpdated = true;
                }
            }
        }

        private void SelectProduct()
        {
            string _choice = Console.ReadLine();
            choice = int.Parse(_choice);
            if (choice == 0)
            {
                view.Display("Come back soon!");
            }
            FindProduct(choice);
        }

        public void FindProduct(int _index)
        {
            int index = model.products.FindIndex(products => products.ProductID == _index);
            if (model.products.Exists(p => p.ProductID == _index))
            {
                ProductModel p = model.products[index];
                ValidateSelection(p);
            }
        }

        private void ValidateSelection(ProductModel p)
        {
            if (PlayerModel.Instance.isBuying)
            {
                if (p.Price <= PlayerModel.Instance.Money)
                {
                    ProductQuantity(p);
                }
                else
                {
                    view.Display("You do not have enough money!");
                    RefreshMenu();
                }
            }
            else
            {
                if (p.Quantity >= 1)
                {
                    ProductQuantity(p);
                }
                else
                {
                    view.Display("You do not stock this item!");
                    RefreshMenu();
                }
            }
        }

        public void ProductQuantity(ProductModel p)
        {
            if (PlayerModel.Instance.isBuying)
            {
                int MaxPurchase = PlayerModel.Instance.Money / p.Price;
                view.Display($"\nYou can afford {MaxPurchase} units. \nHow many would you like to purchase?");
                string _choice = Console.ReadLine();
                choice = int.Parse(_choice);
                //prompt.Response(string.Format("\nYou can afford {0} units \nHow many would you like to purchase?", MaxPurchase), 1, MaxPurchase);
                if (PlayerModel.Instance.Money >= (p.Price * choice))
                {
                    TransactionComplete(p);
                }
                else
                {
                    view.Display($"you can only afford {MaxPurchase} units");
                }
            }
            else
            {
                view.Display($"You can sell {p.Quantity} units \nHow many would you like to sell?");
                string _choice = Console.ReadLine();
                choice = int.Parse(_choice);
                //prompt.Response(string.Format("You can sell {0} units \nHow many would you like to sell?", p.Quantity), 1, p.Quantity);
                if (p.Quantity <= choice)
                {
                    TransactionComplete(p);
                }
                else
                {
                    view.Display($"You can only sell {p.Quantity} units");
                }
            }
        }

        public void TransactionComplete(ProductModel p)
        {
            if (PlayerModel.Instance.isBuying)
            {
                if (choice > 1)
                {
                    MerchantExchangeBuy(p);
                    view.Display($"{p.ProductNamePlural} has been added to your inventory!");
                }
                else
                {
                    MerchantExchangeBuy(p);
                    view.Display($"{p.ProductName} has been added to your inventory!");
                }
                view.Display("Press any key to continue.");
                RefreshMenu();
            }
            else
            {
                if (choice > 1)
                {
                    MerchantExchangeSell(p);
                    view.Display($"{p.ProductNamePlural} has been sold!");
                }
                else
                {
                    MerchantExchangeSell(p);
                    view.Display($"{p.ProductName} has been sold!");
                }
                view.Display("Press any key to continue.");
                RefreshMenu();
            }
        }

        public void MerchantExchangeBuy(ProductModel p)
        {
            p.Quantity += choice;
            PlayerModel.Instance.Money -= (p.Price * choice);
        }

        public void MerchantExchangeSell(ProductModel p)
        {
            p.Quantity -= choice;
            PlayerModel.Instance.Money += (p.Price * choice);
        }

        public void ProductInventory()
        {
            Console.Clear();

            view.Display(PlayerModel.Instance.DayDetails());

            view.Display("Product Inventory \n");

            foreach (var product in model.GetAllProducts())
            {
                view.Display($"{product.ProductID} - {product.ProductName}: {product.Quantity}");
            }

            view.Display("\n0 - Exit!\n");
        }

        private void RefreshMenu()
        {
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        private void Menu()
        {
            Console.Clear();

            view.Display(PlayerModel.Instance.DayDetails());

            view.Display("What would you like to purchase? \n");

            foreach(ProductModel product in model.GetAllProducts())
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
