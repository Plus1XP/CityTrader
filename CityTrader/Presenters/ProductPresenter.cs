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
        private InputPresenter input = new InputPresenter();

        private int? choice;

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
            input.Response("Please select a Product", 0, 10, "We only stock products 1-10, choose again.", "Come back soon!", out choice);
            FindProduct(choice);
        }

        public void FindProduct(int? _index) //Converting int? to int using .Value is this correct??
        {
            int index = model.products.FindIndex(products => products.productID == _index);
            if (model.products.Exists(p => p.productID == _index))
            {
                ProductModel p = model.products[index];
                ValidateSelection(p);
            }
        }

        private void ValidateSelection(ProductModel p)
        {
            if (PlayerModel.Instance.isBuying)
            {
                if (p.price <= PlayerModel.Instance.money)
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
                if (p.quantity >= 1)
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
                int maxPurchase = PlayerModel.Instance.money / p.price;
                view.Display($"\nYou can afford {maxPurchase} units.");
                input.Response("\nHow many would you like to purchase?", 0, (maxPurchase), $"You can only afford {maxPurchase} units, choose again.", "Sorry you changed your mind!", out choice);              
                if (PlayerModel.Instance.money >= (p.price * choice))
                {
                    TransactionComplete(p);
                }
            }
            else
            {
                view.Display($"\nYou can sell {p.quantity} units.");
                input.Response("\nHow many would you like to sell?", 0, (p.quantity), $"You can only sell {p.quantity} units, choose again.", "Sorry you changed your mind!", out choice);
                if (choice <= p.quantity)
                {
                    TransactionComplete(p);
                }
            }
        }

        public void TransactionComplete(ProductModel p)
        {
            if (PlayerModel.Instance.isBuying)
            {
                p.quantity += choice.Value; //Converting int? to int using .Value is this correct??
                PlayerModel.Instance.money -= (p.price * choice.Value); //Converting int? to int using .Value is this correct??
                if (choice == 1)
                {
                    view.Display($"A {p.productName} has been added to your inventory!");
                }
                else
                {
                    view.Display($"{choice} {p.productNamePlural} has been added to your inventory!");                    
                }
            }
            else
            {
                p.quantity -= choice.Value; //Converting int? to int using .Value is this correct??
                PlayerModel.Instance.money += (p.price * choice.Value); //Converting int? to int using .Value is this correct??
                if (choice == 1)
                {
                    view.Display($"A {p.productName} has been sold!");
                }
                else
                {
                    view.Display($"{choice} {p.productNamePlural} has been sold!");
                }
            }
            view.Display("Press any key to continue.");
            choice = null; //Converting int? to int using .Value is this correct??
            RefreshMenu();
        }

        public void ProductInventory()
        {
            Console.Clear();

            view.Display(PlayerModel.Instance.DayDetails());

            view.Display("Product Inventory \n");

            foreach (var product in model.GetAllProducts())
            {
                view.Display($"{product.productID} - {product.productName}: {product.quantity}");
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
                string StockList = $"{product.productID} - {product.productName}: {product.price:C}";
                string PriceMessage = $"{product.message}";
                string Inventory = $"| Inventory: {product.quantity}";
                view.Display($"{StockList,-40} {PriceMessage,-25} {Inventory,20}");
            }

            view.Display("\n0 - Cancel Transaction! \n");

            //view.Display("\nPlease select a Product");
        }
    }
}
