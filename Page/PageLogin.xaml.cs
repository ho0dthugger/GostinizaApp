using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using GostinizaApp.File;

namespace GostinizaApp.Page
{
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
            //dbContext = new ;

            // Подписываемся на событие Click для кнопки "Закрыть приложение"
            btnClose.Click += btnClose_Click;

            // Подписываемся на событие Click для кнопки "Свернуть окно"
            btnMinimize.Click += btnMinimize_Click;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.WindowState = WindowState.Minimized;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем приложение
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (IsValidLogin(username, password, out string userRole))
            {
                txtStatus.Text = "Успешная авторизация!";
                txtStatus.Foreground = System.Windows.Media.Brushes.Green;

                if (userRole == "Администратор")
                {
                    NavigationService.Navigate(new PageAdmin());
                }
                else if (userRole == "Сотрудник")
                {
                    NavigationService.Navigate(new PageMain());
                }
            }
            else
            {
                txtStatus.Text = "Неверный логин или пароль!";
                txtStatus.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private bool IsValidLogin(string username, string password, out string userRole)
        {
            // Ищем пользователя в базе данных
            var user = dbContext.User.FirstOrDefault(u => u.Ulogin == username && u.UPassword == password);

            if (user != null)
            {
                userRole = user.Роль;
                return true;
            }

            userRole = null;
            return false;
        }
    }
}
