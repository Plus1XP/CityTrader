using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductModel
    {
        public List<ProductModel> products = new List<ProductModel>();

        private int eventRate = 3;
        private int eventChance = 10;

        public int productID { get; set; }
        public string productName { get; set; }
        public string productNamePlural { get; set; }
        public int quantity { get; set; }
        public int lowPrice { get; set; }
        public int highPrice {get; set;}
        public string lowMessage { get; set; }
        public string highMessage { get; set; }
        public int price { get; set; }
        public int oldPrice { get; set; }
        public string message { get; set; }
        public int ProductExperience { get; set; }

        public ProductModel(int id, string name, string nameplural, int lowprice, int highprice)
        {
            this.productID = id;
            this.productName = name;
            this.productNamePlural = nameplural;
            this.lowPrice = lowprice;
            this.highPrice = highprice;
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
            products.Add(product);
        }

        public IEnumerable<ProductModel>GetAllProducts()
        {
            return products;
        }

        public void UpdatePrice()
        {
            price = RNGModel.RandomPrice.Next(lowPrice, highPrice + 1);
            message = null;

            int EventChance = RNGModel.RandomPrice.Next(this.eventChance);

            if (EventChance == 0)
            {
                price *= eventRate;
                message = "- Prices are high!";
            }
            else if (EventChance == 1)
            {
                price /= eventRate;
                message = "- Prices are low!";
            }
        }
    }
}
