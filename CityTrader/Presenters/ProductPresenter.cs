namespace Presenters
{
    using System;

    using Models;
    using Views;

    public class ProductPresenter
    {
        private Product product = new Product();
        private DialoguePresenter productChoice;
        private DialoguePresenter buyQuantityChoice;
        private DialoguePresenter sellQuantityChoice;
        private GameView view = new GameView();

        private bool isMenuActive = true;

        public ProductPresenter()
        {
        }

        public void Update()
        {
            this.UpdatePrice();
            this.Menu();
            do
            {
                this.SelectProduct();
            } while (this.isMenuActive);
        }

        public void ProductInventory()
        {
            Console.Clear();

            this.view.Display(Player.Instance.Status);

            this.view.Display("Product Inventory \n");

            foreach (var product in this.product.GetAllProducts())
            {
                this.view.Display($"{product.ID} - {product.Name}: {product.Quantity}");
            }

            this.view.Display("\n0 - Exit!\n");
        }

        private void UpdatePrice()
        {
            if (!Player.Instance.HasProductPriceUpdated)
            {
                foreach (Product product in product.ProductList)
                {
                    product.UpdatePrice();
                    Player.Instance.HasProductPriceUpdated = true;
                }
            }
        }

        private void SelectProduct()
        {
            this.productChoice = new DialoguePresenter("Please select a Product", 0, 10, "We only stock products 1-10, choose again.", "Come back soon!");
            this.FindProduct(this.productChoice.ShowDialogue());
        }

        private void FindProduct(int? productChoice)
        {
            int index = this.product.ProductList.FindIndex(products => products.ID == productChoice);
            if (this.product.ProductList.Exists(p => p.ID == productChoice))
            {
                Product p = this.product.ProductList[index];
                this.ValidateSelection(p);
            }
            else if (productChoice == 0)
            {
                this.isMenuActive = false;
            }
        }

        private void ValidateSelection(Product p)
        {
            if (Player.Instance.IsBuying)
            {
                if (p.CurrentPrice <= Player.Instance.Money)
                {
                    this.ChooseQuantity(p);
                }
                else
                {
                    this.view.Display("You do not have enough money!");
                    this.RefreshMenu();
                }
            }
            else
            {
                if (p.Quantity >= 1)
                {
                    this.ChooseQuantity(p);
                }
                else
                {
                    this.view.Display("You do not stock this item!");
                    this.RefreshMenu();
                }
            }            
        }

        private void ChooseQuantity(Product p)
        {
            int? productQuantity = null;

            if (Player.Instance.IsBuying)
            {
                int purchaseLimit = this.CalculatePurchaseLimit(p);

                this.view.Display($"\nYou can purchase {purchaseLimit} units.");

                this.buyQuantityChoice = new DialoguePresenter("\nHow many would you like to purchase? \nEnter a value or buy (a)ll available.", 0, purchaseLimit, $"You can only purchase {purchaseLimit} units, choose again.", "Sorry you changed your mind!", purchaseLimit, "a", "all");
                productQuantity = this.buyQuantityChoice.ShowDialogue();

                if (Player.Instance.Money >= (p.CurrentPrice * productQuantity))
                {
                    this.TransactionComplete(p, productQuantity);
                }
            }
            else
            {
                this.view.Display($"\nYou can sell {p.Quantity} units.");
                this.sellQuantityChoice = new DialoguePresenter("\nHow many would you like to sell? \nEnter a value or Sell (a)ll available.", 0, p.Quantity, $"You can only sell {p.Quantity} units, choose again.", "Sorry you changed your mind!", p.Quantity, "a", "all");
                productQuantity = this.sellQuantityChoice.ShowDialogue();
                if (productQuantity <= p.Quantity)
                {
                    this.TransactionComplete(p, productQuantity);
                }
            }
        }

        private void TransactionComplete(Product p, int? productQuantity)
        {
            if (Player.Instance.IsBuying)
            {
                // Add method to calculate the average price to display potential profits.
                p.PurchasePrice = p.CurrentPrice;
                p.Quantity += productQuantity.Value;
                Player.Instance.Money -= p.CurrentPrice * productQuantity.Value;

                if (productQuantity == 1)
                {
                    this.view.Display($"A {p.Name} has been added to your inventory!");
                }
                else
                {
                    this.view.Display($"{productQuantity} {p.PluralName} has been added to your inventory!");                    
                }
            }
            else
            {
                this.view.Display(Player.Instance.AddExperiencePoints(p.CurrentPrice, p.PurchasePrice, productQuantity.Value));
                p.Quantity -= productQuantity.Value;
                Player.Instance.Money += (decimal)p.CurrentPrice * (decimal)productQuantity.Value;                
                if (productQuantity == 1)
                {
                    this.view.Display($"A {p.Name} has been sold!");
                }
                else
                {
                    this.view.Display($"{productQuantity} {p.PluralName} has been sold!");
                }
            }

            this.view.Display("Press any key to continue.");
            this.RefreshMenu();
        }

        private int CalculatePurchaseLimit(Product p)
        {
            // <field name="maxPurchase">If this value exceeds the data type range, an overflow exception will occur.</field>
            int maxPurchase = (int)Math.Floor(Player.Instance.Money / p.CurrentPrice);
            int currentStorage = Player.Instance.Storage - p.Quantity;
            int purchaseLimit = Math.Min(currentStorage, maxPurchase);
            return purchaseLimit;
        }

        private void RefreshMenu()
        {
            Console.ReadKey();
            this.Update();
        }

        private void Menu()
        {
            Console.Clear();

            this.view.Display(Player.Instance.Status);

            this.view.Display("What would you like to trade? \n");

            foreach (Product product in this.product.GetAllProducts())
            {
                string stockList = $"{product.ID} - {product.Name}: {product.CurrentPrice:C}";
                string priceMessage = $"{product.PriceGuideMessage}";
                string inventory = $"| Inventory: {product.Quantity} / {Player.Instance.Storage}";
                this.view.Display($"{stockList,-40} {priceMessage,-25} {inventory,20}");
            }

            this.view.Display("\n0 - Cancel Transaction! \n");
        }
    }
}
