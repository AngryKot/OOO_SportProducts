//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOO_SportProducts.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.OrderProduct = new HashSet<OrderProduct>();
        }
    
        public string ProductArticle { get; set; }
        public string ProductName { get; set; }
        public int ProductUnit { get; set; }
        public decimal ProductCost { get; set; }
        public int ProductDiscountMax { get; set; }
        public int ProductManufacturer { get; set; }
        public int ProductProvider { get; set; }
        public int ProductCategory { get; set; }
        public int ProductDiscount { get; set; }
        public int ProductCount { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPhoto { get; set; }

        //public string ProductPhotoPath
        //{
        //    get

        //    {
        //        string temp = Environment.CurrentDirectory + "/Images/" + ProductPhoto;
        //        //if (String.IsNullOrEmpty(this.ProductPhoto))
        //        if (!File.Exists(temp))
        //        {
        //            //temp = Environment.CurrentDirectory+"/Images/" + ProductPhoto;
        //            temp = "Resources/picture.png";
        //        }
        //        //else
        //        //{
        //        //    temp = "Resources/picture.png";  
        //        //}
        //        return temp;


        //    }
        //}

        //public double ProductCostWithDiscount
        //{
        //    get

        //    {
        //        double temp = 0;
        //        double discount = (double)ProductCost * (double)ProductDiscount / 100;
        //        temp = (double)ProductCost - discount;

        //        return temp;
        //    }
             
        //}


        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
