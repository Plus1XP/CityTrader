namespace Models
{
    using System;
    using System.Collections.Generic;

    public class Product
    {
        // <field name="eventMultiplier">The price multiplier when an event is triggered.</field>
        // <field name="eventRate">The chance of triggering an event.</field>
        // <field name="minimumEventChance">The minimium Chance of triggering an event.</field>
        private readonly int eventMultiplier = 3;
        private readonly int eventRate = 10;
        private readonly int minimumEventChance = 4;

        private List<Product> products = new List<Product>();

        public Product()
        {
            this.AddProducts(new Product(1, "Luxury Watch", "Luxury watches", 50000, 60000));
            this.AddProducts(new Product(2, "Luxury Handbag", "Luxury Handbags", 25000, 30000));
            this.AddProducts(new Product(3, "Luxury Shoes", "Luxury Shoes", 5000, 8000));
            this.AddProducts(new Product(4, "Topend Electronics", "Topend Electronics", 2000, 4000));
            this.AddProducts(new Product(5, "Flagship Cellphone", "Flagship Cellphones", 600, 1000));
            this.AddProducts(new Product(6, "Designer Jeans", "Designer Jeans", 300, 500));
            this.AddProducts(new Product(7, "Limited Sneakers", "Limited Sneakers", 100, 200));
            this.AddProducts(new Product(8, "Hignhend Makeup Kit", "highend Makeup Kits", 50, 100));
            this.AddProducts(new Product(9, "Fitted Cap", "Fitted Caps", 25, 50));
            this.AddProducts(new Product(10, "Fashion Accessorie", "Fashion Accessories", 10, 25));
        }

        private Product(int id, string name, string namePlural, int lowPrice, int highPrice)
        {
            this.ID = id;
            this.Name = name;
            this.PluralName = namePlural;
            this.LowestSalePrice = lowPrice;
            this.HighestSalePrice = highPrice;
        }

        public string Name { get; private set; }

        public string PluralName { get; private set; }

        public string LowestSalePriceMessage { get; private set; }

        public string HighestSalePriceMessage { get; private set; }

        public string PriceGuideMessage { get; private set; }

        public int ID { get; private set; }

        public int LowestSalePrice { get; private set; }

        public int HighestSalePrice { get; private set; }

        public int CurrentSalePrice { get; private set; }

        public int PreviousSalePrice { get; set; }

        public int Quantity { get; set; }

        public long ExperiencePoints { get; set; }

        public List<Product> ProductList
        {
            get
            {
                return this.products;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return this.products;
        }

        public void UpdatePrice()
        {
            this.CurrentSalePrice = RNGModel.RandomNumber.Next(this.LowestSalePrice, this.HighestSalePrice + 1);
            this.PriceGuideMessage = null;

            int eventChance = this.SetEventChance();
            int eventOutcome = RNGModel.RandomNumber.Next(eventChance);

            if (eventOutcome == 0)
            {
                this.SetHighSalePrice();
            }
            else if (eventOutcome == 1)
            {
                this.SetLowSalePrice();
            }

            // <summary>Used for debugging, hidden value passed back to "Player.eventChanceReults".</summary>
            Player.Instance.EventResults(eventChance);
        }

        // <summary>Returns a value corresponding to the chance of an event being triggered based on the players level.</summary>
        private int SetEventChance()
        {
            int eventChance;
            int playerLevelEventRate = (this.eventRate + 1) - Player.Instance.Level;
            return eventChance = Math.Max(this.minimumEventChance, playerLevelEventRate);
        }

        private void SetHighSalePrice()
        {
            this.CurrentSalePrice *= this.eventMultiplier;
            this.PriceGuideMessage = "- Prices are high!";
        }

        private void SetLowSalePrice()
        {
            this.CurrentSalePrice /= this.eventMultiplier;
            this.PriceGuideMessage = "- Prices are low!";
        }

        private void AddProducts(Product product)
        {
            this.products.Add(product);
        }
    }
}
