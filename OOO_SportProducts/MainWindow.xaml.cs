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
using System.Windows.Threading;

namespace OOO_SportProducts
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        private bool isFirst = true;
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
            if (!isFirst)
            {
                if (CaptchaEnter.Text != captchaText)
                {
                    sb.AppendLine("Каптча введена неверно");
                    //MessageBox.Show(captcha.CaptchaText);
                }
            }
            //поиск в бд пользователя с таким логином и паролем

            if (sb.Length == 0)
            {
                List<Model.User> user = new List<Model.User>();
                //получить все данные их таблицы
                //users = Helper.DB.User.ToList();
                user = Helper.dBSportEntities.User.Where(u => u.UserLogin == login && u.UserPassword == password).ToList();
                //Model.User user = users.FirstOrDefault();
                Helper.User = user.FirstOrDefault();
                if (Helper.User != null)
                {
                    sb.AppendLine("Здравствуйте " + Helper.User.UserFullName);
                    sb.AppendLine("Вы вошли с ролью " + Helper.User.Role.RoleName);
                    MessageBox.Show(sb.ToString());
                    goCatalog();
                }
                else
                {
                    sb.AppendLine("Такой пользователь не зарегистрирован");
                }


                if (sb.Length > 0)
                {
                    MessageBox.Show(sb.ToString());
                    if (isFirst)
                    {
                        captcha.Visibility = Visibility.Visible;
                        CaptchaEnter.Visibility = Visibility.Visible;
                    }
                    if (!isFirst)
                    {
                        CaptchaEnter.IsEnabled = false;
                        captcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 4);
                        captchaText = captcha.CaptchaText;  //Сохранение строки каптчи
                        timer.Tick += new EventHandler(TimerTick);
                        timer.Interval = new TimeSpan(40000000);
                        timer.Start();
                    }
                    isFirst = false;
                    return;
                }
            }
               
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

        private void TimerTick(object sender, EventArgs e)
        {
            butEnter.IsEnabled = true;
            timer.Stop();
            CaptchaEnter.IsEnabled = true;
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


        private void goCatalog()
        {
            View.CatalogWindow catalog = new View.CatalogWindow();
            this.Hide();
            catalog.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// Переход в каталог для гостя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void butGuest_Click(object sender, RoutedEventArgs e)
        {
           
                goCatalog();
            
        }
    }
}
