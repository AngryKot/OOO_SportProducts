using EasyCaptcha.Wpf;
using OOO_SportProduct.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOO_SportProducts
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string captchaText;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Helper.dBSportEntities = new Model.DBsport403Entities();
                MessageBox.Show("Связались с БД");
            }
            catch (Exception)
            {
                MessageBox.Show("Проблемы связи с базой данных");
                return;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            createCaptcha(4);
        }


        /// <summary>
        /// Метод создания каптчи
        /// </summary>
        /// <param name="lenghtCaptcha"></param>
        private void createCaptcha(int lenghtCaptcha)
        {
            lenghtCaptcha = 4;    // Длина каптчи


            // Формирование строки каптчи
            captcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, lenghtCaptcha);
            captchaText = captcha.CaptchaText;   // Сохранение строки каптчи

        }


        /// <summary>
        /// Завершение работы приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butNavigation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Переход в каталог при авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = tbPassword.Password;
            StringBuilder sb = new StringBuilder();
            if (String.IsNullOrEmpty(login))
            {
                sb.AppendLine("Вы забыли ввести логин");

            }
            if (String.IsNullOrEmpty(password))
            {
                sb.AppendLine("Вы забыли ввести пароль");

            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return;
            }
            //поиск в бд пользователя с таким логином и паролем
            List<Model.User> users = new List<Model.User>();
            //Получить данные из таблицы
            users = Helper.dBSportEntities.User.ToList();

            bool flag = false;
            foreach (Model.User user in users)
            {
                if (string.Equals(user.UserLogin, login, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(user.UserPassword, password, StringComparison.Ordinal) &&
                    string.Equals(captcha.CaptchaText, CaptchaEnter.Text, StringComparison.OrdinalIgnoreCase))
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                MessageBox.Show("Вход выполнен успешно.");
            }
            else
            {
                MessageBox.Show("Вход не выполнен. Попробуйте еще раз.");
            }


        }

        private void isVisiblePassword_Checked(object sender, RoutedEventArgs e)
        {
           
            tbPasswordDisplay.Text = tbPassword.Password;
            tbPasswordDisplay.Visibility = Visibility.Visible; 
            tbPassword.Visibility = Visibility.Collapsed; 

        }

        private void isVisiblePassword_Unchecked(object sender, RoutedEventArgs e)
        {
            tbPassword.Password = tbPasswordDisplay.Text;
            tbPasswordDisplay.Visibility = Visibility.Collapsed;
            tbPassword.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Переход в каталог для гостя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butGuest_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
