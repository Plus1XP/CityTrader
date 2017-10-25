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
                foreach (ProductModel product in model.Products)
                {
                    product.UpdatePrice();
                    PlayerModel.Instance.hasProductPriceUpdated = true;
                }
            }
        }

        private void SelectProduct()
        {
            input.Response("Please select a Product", null, 0, 10, "We only stock products 1-10, choose again.", "Come back soon!", out choice);
            FindProduct(choice);
        }

        public void FindProduct(int? _index)
        {
            int index = model.Products.FindIndex(products => products.ProductID == _index);
            if (model.Products.Exists(p => p.ProductID == _index))
            {
                ProductModel p = model.Products[index];
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
                //Use a larger DataType to prevent an overflow exception, if the player has a large amount of money and can purchase large amounts.                
                int maxPurchase = (int)Math.Floor(PlayerModel.Instance.Money / p.Price);
                int currentStorage = PlayerModel.Instance.MaxStorage - p.Quantity;
                int purchaseLimit = Math.Min(currentStorage, maxPurchase);
                view.Display($"\nYou can purchase {purchaseLimit} units.");
                input.Response("\nHow many would you like to purchase? \nEnter a value or Buy (a)ll available.", purchaseLimit, 0, (purchaseLimit), $"You can only purchase {purchaseLimit} units, choose again.", "Sorry you changed your mind!", out choice);              
                if (PlayerModel.Instance.Money >= (p.Price * choice))
                {
                    TransactionComplete(p);
                }
            }
            else
            {
                view.Display($"\nYou can sell {p.Quantity} units.");
                input.Response("\nHow many would you like to sell? \nEnter a value or Sell (a)ll available.", p.Quantity, 0, (p.Quantity), $"You can only sell {p.Quantity} units, choose again.", "Sorry you changed your mind!", out choice);
                if (choice <= p.Quantity)
                {
                    TransactionComplete(p);
                }
            }
        }

        public void TransactionComplete(ProductModel p)
        {
            if (PlayerModel.Instance.isBuying)
            {
                //Add method to calculate the average price to display potential profits.
                p.OldPrice = p.Price;
                p.Quantity += choice.Value;
                PlayerModel.Instance.Money -= (p.Price * choice.Value);
                if (choice == 1)
                {
                    view.Display($"A {p.ProductName} has been added to your inventory!");
                }
                else
                {
                    view.Display($"{choice} {p.ProductNamePlural} has been added to your inventory!");                    
                }
            }
            else
            {
                p.ProductExperience = PlayerModel.Instance.ExperienceReward(p.Price, p.OldPrice, choice.Value);
                PlayerModel.Instance.GainExperience(p.ProductExperience);
                PlayerModel.Instance.SetMaxStorage();

                p.Quantity -= choice.Value;
                PlayerModel.Instance.Money += ((decimal)p.Price * (decimal)choice.Value);                
                if (choice == 1)
                {
                    view.Display($"A {p.ProductName} has been sold!");
                }
                else
                {
                    view.Display($"{choice} {p.ProductNamePlural} has been sold!");
                }
            }
            view.Display("Press any key to continue.");
            choice = null;
            RefreshMenu();
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
            //Console.Clear(); //delete?
            Update();
        }

        private void Menu()
        {
            Console.Clear();

            view.Display(PlayerModel.Instance.DayDetails());

            view.Display("What would you like to trade? \n");

            foreach(ProductModel product in model.GetAllProducts())
            {
                string StockList = $"{product.ProductID} - {product.ProductName}: {product.Price:C}";
                string PriceMessage = $"{product.Message}";
                string Inventory = $"| Inventory: {product.Quantity} / {PlayerModel.Instance.MaxStorage}";
                view.Display($"{StockList,-40} {PriceMessage,-25} {Inventory,20}");
            }

            view.Display("\n0 - Cancel Transaction! \n");
        }
    }
}
