using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductModel
    {
        public List<ProductModel> Products = new List<ProductModel>();

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductNamePlural { get; set; }
        public int Quantity { get; set; }
        //public int MaxQuantity { get; set; } = 1000;
        public int LowPrice { get; set; }
        public int HighPrice {get; set;}
        public string LowMessage { get; set; }
        public string HighMessage { get; set; }
        public int Price { get; set; }
        public int OldPrice { get; set; }
        public string Message { get; set; }
        public long ProductExperience { get; set; }

        private int eventMultiplier = 3;
        private int eventRate = 10;
        private int minimumEventChance = 4;

        public ProductModel(int id, string name, string namePlural, int lowPrice, int highPrice)
        {
            this.ProductID = id;
            this.ProductName = name;
            this.ProductNamePlural = namePlural;
            this.LowPrice = lowPrice;
            this.HighPrice = highPrice;
        }

        public ProductModel()
        {
            AddProducts(new ProductModel(1, "Luxury Watch", "Luxury watches", 50000, 60000));
            AddProducts(new ProductModel(2, "Luxury Handbag", "Luxury Handbags", 25000, 30000));
            AddProducts(new ProductModel(3, "Luxury Shoes", "Luxury Shoes", 5000, 8000));
            AddProducts(new ProductModel(4, "Topend Electronics", "Topend Electronics", 2000, 4000));
            AddProducts(new ProductModel(5, "Flagship Cellphone", "Flagship Cellphones", 600, 1000));
            AddProducts(new ProductModel(6, "Designer Jeans", "Designer Jeans", 300, 500));
            AddProducts(new ProductModel(7, "Limited Sneakers", "Limited Sneakers", 100, 200));
            AddProducts(new ProductModel(8, "Hignhend Makeup Kit", "highend Makeup Kits", 50, 100));
            AddProducts(new ProductModel(9, "Fitted Cap", "Fitted Caps", 25, 50));
            AddProducts(new ProductModel(10, "Fashion Accessorie", "Fashion Accessories", 10, 25));
        }

        public void AddProducts(ProductModel product)
        {
            Products.Add(product);
        }

        public IEnumerable<ProductModel>GetAllProducts()
        {
            return Products;
        }

        public int SetEventChance()
        {
            int eventChance;
            int playerLevelEventRate = (this.eventRate + 1) - PlayerModel.Instance.CurrentLevel;
            return eventChance = Math.Max(minimumEventChance, playerLevelEventRate);
        }

        public void UpdatePrice()
        {            
            Price = RNGModel.RandomPrice.Next(LowPrice, HighPrice + 1);
            Message = null;

            int eventChance = SetEventChance();
            int eventOutcome = RNGModel.RandomPrice.Next(eventChance);
            //For hidden value.
            PlayerModel.Instance.EventResults(eventChance);

            if (eventOutcome == 0)
            {
                Price *= eventMultiplier;
                Message = "- Prices are high!";
            }
            else if (eventOutcome == 1)
            {
                Price /= eventMultiplier;
                Message = "- Prices are low!";
            }
        }
    }
}
