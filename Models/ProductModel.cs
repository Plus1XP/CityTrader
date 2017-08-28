﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductModel
    {
        public List<ProductModel> products = new List<ProductModel>();

        private int EventRate = 3;
        private int EventChance = 10;

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductNamePlural { get; set; }
        public int Quantity { get; set; }
        public int LowPrice { get; set; }
        public int HighPrice {get; set;}
        public string LowMessage { get; set; }
        public string HighMessage { get; set; }
        public int Price { get; set; }
        public int UpdatedPrice { get; set; }
        public string Message { get; set; }

        public ProductModel(int id, string name, string nameplural, int lowprice, int highprice)
        {
            this.ProductID = id;
            this.ProductName = name;
            this.ProductNamePlural = nameplural;
            this.LowPrice = lowprice;
            this.HighPrice = highprice;
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

        public void AddProducts(ProductModel Product)
        {
            products.Add(Product);
        }

        public IEnumerable<ProductModel>GetAllProducts()
        {
            return products;
        }

        public void FindProduct(int _index)
        {
            int index = products.FindIndex(products => products.ProductID == _index);
            if (products.Exists(p => p.ProductID == _index))
            {
                ProductModel p = products[index];
                VerifyProduct(p);
            }            
        }

        private void VerifyProduct(ProductModel p)
        {
            if (PlayerModel.Instance.isBuying)
            {
                if (p.Price <= PlayerModel.Instance.Money)
                {
                    ProductQuantity(p);
                }
                else
                {
                    Message = "You do not have enough money!";
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
                    Message = "You do not stock this item!";
                }
            }
        }

        public void ProductQuantity(ProductModel p)
        {
            if (PlayerModel.Instance.isBuying)
            {
                int MaxPurchase = PlayerModel.Instance.Money / p.Price;
                prompt.Response(string.Format("\nYou can afford {0} units \nHow many would you like to purchase?", MaxPurchase), 1, MaxPurchase);
                if (PlayerModel.Instance.Money >= (p.Price * prompt.intResponse))
                {
                    TransactionComplete(p);
                }
                else
                {
                    Message = string.Format("you can only afford {0} units", MaxPurchase);
                }
            }
            else
            {
                prompt.Response(string.Format("You can sell {0} units \nHow many would you like to sell?", p.Quantity), 1, p.Quantity);
                if (p.Quantity <= prompt.intResponse)
                {
                    TransactionComplete(p);
                }
                else
                {
                    Message = string.Format("You can only sell {0} units", p.Quantity);
                }
            }
        }

        public void TransactionComplete(ProductModel p)
        {
            if (PlayerModel.Instance.isBuying)
            {
                if (prompt.intResponse > 1)
                {
                    MerchantExchangeBuy(p);                    
                    Message = $"{prompt} {p.ProductNamePlural} has been added to your inventory!";
                }
                else
                {
                    MerchantExchangeBuy(p);
                    Message = $"{prompt} {p.ProductName} has been added to your inventory!";
                }
            }
            else
            {
                if (prompt.intResponse > 1)
                {
                    MerchantExchangeSell(p);
                    Message = $"{prompt} {p.ProductNamePlural} has been sold!";
                }
                else
                {
                    MerchantExchangeSell(p);
                    Message = $"{prompt} {p.ProductName} has been sold!";
                }
            }
        }

        public void MerchantExchangeBuy(ProductModel p)
        {
            p.Quantity += prompt.intResponse;
            PlayerModel.Instance.Money -= (p.Price * prompt.intResponse);
        }

        public void MerchantExchangeSell(ProductModel p)
        {
            p.Quantity -= prompt.intResponse;
            PlayerModel.Instance.Money += (p.Price * prompt.intResponse);
        }

        public void Refresh()
        {
            Console.ReadLine();
            Console.Clear();
            
        }

        public void UpdatePrice()
        {
            Price = RNGModel.RandomPrice.Next(LowPrice, HighPrice + 1);
            Message = null;

            int eventchace = RNGModel.RandomPrice.Next(EventChance);

            if (eventchace == 0)
            {
                Price *= EventRate;
                Message = "- Prices are high!";
            }
            else if (eventchace == 1)
            {
                Price /= EventRate;
                Message = "- Prices are low!";
            }
        }
    }
}
