using System.Collections.Generic;
using firstHWwithSingleton.Model;

namespace firstHWwithSingleton.Data
{
    // Singleton Fourth Version from By Jon Skeet
    public sealed class MyData
    {
        private static readonly MyData _instance = new MyData();

        static MyData(){}

        private MyData()
        {
            forDataPut();
        }

        public static MyData Instance
        {
            get
            {
                return _instance;
            }
        }

        public List<ProductModel> Products = new List<ProductModel>();

        private void forDataPut()
        {
            for (int i = 1; i <= 7; i++)
            {
                Products.Add(new ProductModel()
                {
                    Id = i,
                    Name = "Product name <-> " + i,
                    Price = 1000 + (i * 7)
                });
            }
        }
    }
}