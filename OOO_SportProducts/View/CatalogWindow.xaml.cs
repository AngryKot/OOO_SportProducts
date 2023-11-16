using OOO_SportProduct.Classes;
using OOO_SportProducts.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace OOO_SportProducts.View
{
    /// <summary>
    /// Логика взаимодействия для CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        public CatalogWindow()
         
        {
            InitializeComponent();
        }




        /// <summary>
        /// Возврат на авторизацию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butNavigation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MenuItemAdd.Visibility = Visibility.Hidden;
            if(Helper.User==null || Helper.User.UserRole == 1)
            {
                MenuItemAdd.Visibility = Visibility.Visible;
            }

            List<Model.Category> categories = new List<Model.Category>();
            categories = Helper.dBSportEntities.Category.ToList();
            Model.Category category = new Model.Category();
            category.CategoryID = 0;
            category.CategoryName = "Все категории";
            categories.Insert(0, category);

            cbFilterCategory.DisplayMemberPath = "CategoryName";
            cbFilterCategory.SelectedValuePath = "CategoryID";
            cbFilterCategory.ItemsSource = categories;

            cbSort.SelectedIndex = 0;
            cbFilterDiscount.SelectedIndex = 0;
            cbFilterCategory.SelectedIndex = 0;

            ShowProducts();
        }

        private void ShowProducts()
        {
            List<ProductExtended> extendedProducts =
                Helper.dBSportEntities.Product.ToList().ConvertAll<ProductExtended>(p => new ProductExtended(p));

            int totalCount = extendedProducts.Count;
            //Сортировка
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    extendedProducts = extendedProducts.OrderBy(pr => pr.Product.ProductCost).ToList();
                    break;
                case 1:
                    extendedProducts = extendedProducts.OrderByDescending(pr => pr.Product.ProductCost).ToList();
                    break;
            }
            int max = 100, min = 0;
            //Фильтр по скидке
            if (cbFilterDiscount.SelectedIndex > 0)
            {
                switch (cbFilterDiscount.SelectedIndex)
                {
                    case 1:
                        min = 0; max = 9;
                        break;
                    case 2:
                        min = 10; max = 14;
                        break;
                    case 3:
                        min = 15; max = 100;
                        break;
                }
                extendedProducts = extendedProducts.Where(pr => pr.Product.ProductDiscountMax >= min && pr.Product.ProductDiscountMax <= max).ToList();
            }
            //фильтр по категории
            if (cbFilterCategory.SelectedIndex > 0)
            {
                extendedProducts = extendedProducts.Where(pr => pr.Product.ProductCategory == (int)cbFilterCategory.SelectedValue).ToList();
            }
            //поиск по названию товара
            string search = tbSearch.Text;
            if (!string.IsNullOrEmpty(search))
            {
                //extendedProducts = extendedProducts.Where(pr => pr.Product.ProductName.Contains(search)).ToList();
                extendedProducts = extendedProducts.Where(pr => pr.Product.ProductName.ToLower().Contains(search.ToLower())).ToList();
            }


            ////Получили все данные о товарах
            //List<Model.Product> products = new List<Model.Product>();
            //products = Helper.DB.Product.ToList();
            ////listBoxProducts.ItemsSource = products;
            //List<Classes.ProductExtended> productExtendeds = new List<Classes.ProductExtended>();
            //foreach (var item in products)
            //{
            //    Classes.ProductExtended productExtended = new ProductExtended();
            //    productExtended.Product = item;
            //    productExtendeds.Add(productExtended);
            //}

            int filterCount = extendedProducts.Count;
            tbCount.Text = filterCount + " из" + totalCount;
            listBoxProducts.ItemsSource = extendedProducts;
        }

        /// <summary>
        /// Выбор направления сортировки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowProducts();
        }

        /// <summary>
        /// фильтрация по скидкам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbFilterDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowProducts();
        }

        /// <summary>
        /// фильтрация по категории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbFilterCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowProducts();
        }

        /// <summary>
        /// ввод строки поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowProducts();
        }

        private void buttonNavigation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
