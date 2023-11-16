using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


namespace OOO_SportProducts.Classes
{
    public class ProductExtended
    {
        public ProductExtended(Model.Product product)
        {
            this.Product = product;
        }
        public Model.Product Product { get; set; }

        //строка с заглушкой или фото+папка
        public string ProductPhotoPath
        {
            get
            {
                string temp = Environment.CurrentDirectory + "/Images/" + Product.ProductPhoto;
                if (!File.Exists(temp))
                {
                    temp = "/Resources/picture.png";

                }
                return temp;
            }

        }

        //цена со скидкой
        private double productCostWithDiscount;
        public double ProductCostWithDiscount
        {
            get
            {
                double temp = 0.0;
                double discount = (double)Product.ProductCost * (double)Product.ProductDiscount / 100;
                temp = (double)Product.ProductCost - discount;
                return temp;
            }
            set
            {
                productCostWithDiscount = value;
            }
        }
    }
}
