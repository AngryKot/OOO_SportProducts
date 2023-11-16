using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_SportProducts.Classes
{
    public class ProductInOrder
    {
        //товар в заказе
        Classes.ProductExtended ProductExtendedInOrder { get; set; }

        //кол-во товара в заказе
        int countProductInOrder { get; set; }

        //создание нового товара в заказе
        public ProductInOrder(ProductExtended productExtended)
        {
            ProductExtendedInOrder = productExtended;
            this.countProductInOrder =1;
        }

        public ProductInOrder()
        {

        }

    }
}
