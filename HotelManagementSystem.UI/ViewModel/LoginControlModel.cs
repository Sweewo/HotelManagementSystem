using HotelManagementSystem.Core.Data.Additional;
using HotelManagementSystem.Core.Service;
using HotelManagementSystem.UI.Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementSystem.UI.ViewModel
{
    public class LoginControlModel : DependencyObject
    {
        private MainController mainController;
        private IHotelSystem hotel;

        #region dependency properties
        public string Login
        {
            get { return (string)GetValue(LoginProperty); }
            set { SetValue(LoginProperty, value); }
        }
        public static readonly DependencyProperty LoginProperty =
            DependencyProperty.Register("Login", typeof(string), typeof(LoginControlModel), new PropertyMetadata(""));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(LoginControlModel), new PropertyMetadata(""));
        #endregion

        #region commands
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        private bool LoginCanExecute(object o)
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
                return false;
            return true;
        }

        private void LoginExecute(object o)
        {
            var user = hotel.Login(Login, Password);
            if (user is null)
            {
                Console.WriteLine(@"Wrong username or password");
                return;
            }

            switch (user.UserType)
            {
                case UserType.Manager:
                    mainController.LoadManagerDashboard();
                    break;
                case UserType.Visitor:
                    mainController.LoadVisitorDashboard();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void RegisterExecute(object o)
        {
            mainController.LoadRegistrationDashboard();
        }
        #endregion


        public LoginControlModel(MainController mainController, IHotelSystem hotel)
        {
            this.mainController = mainController;
            this.hotel = hotel;
            LoginCommand = new RelayCommand(LoginExecute, LoginCanExecute);
            RegisterCommand = new RelayCommand(RegisterExecute);
        }

        public void Refresh()
        {
            Login = string.Empty;
            Password = string.Empty;
        }
    }
}
