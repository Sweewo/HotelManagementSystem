using HotelManagementSystem.Core.Data;
using HotelManagementSystem.Core.Service;
using HotelManagementSystem.UI.Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementSystem.UI.ViewModel
{
    public class RegistrationControlModel : DependencyObject
    {
        private MainController mainController;
        private IHotelSystem hotel;

        #region dependency properties
        public string RLogin
        {
            get { return (string)GetValue(RLoginProperty); }
            set { SetValue(RLoginProperty, value); }
        }
        public static readonly DependencyProperty RLoginProperty =
            DependencyProperty.Register("RLogin", typeof(string), typeof(RegistrationControlModel), new PropertyMetadata(""));

        public string RPassword
        {
            get { return (string)GetValue(RPasswordProperty); }
            set { SetValue(RPasswordProperty, value); }
        }
        public static readonly DependencyProperty RPasswordProperty =
            DependencyProperty.Register("RPassword", typeof(string), typeof(RegistrationControlModel), new PropertyMetadata(""));

        public string RFirstName
        {
            get { return (string)GetValue(RFirstNameProperty); }
            set { SetValue(RFirstNameProperty, value); }
        }
        public static readonly DependencyProperty RFirstNameProperty =
            DependencyProperty.Register("RFirstName", typeof(string), typeof(RegistrationControlModel), new PropertyMetadata(""));

        public string RSecondName
        {
            get { return (string)GetValue(RSecondNameProperty); }
            set { SetValue(RSecondNameProperty, value); }
        }
        public static readonly DependencyProperty RSecondNameProperty =
            DependencyProperty.Register("RSecondName", typeof(string), typeof(RegistrationControlModel), new PropertyMetadata(""));

        public string REmail
        {
            get { return (string)GetValue(REmailProperty); }
            set { SetValue(REmailProperty, value); }
        }
        public static readonly DependencyProperty REmailProperty =
            DependencyProperty.Register("REmail", typeof(string), typeof(RegistrationControlModel), new PropertyMetadata(""));
        #endregion

        #region commands
        public ICommand RegisterCommand { get; set; }
        public ICommand BackToLoginCommand { get; set; }

        private bool RegisterCanExecute(object o)
        {
            if (string.IsNullOrWhiteSpace(RLogin) || string.IsNullOrWhiteSpace(RPassword) ||
                string.IsNullOrWhiteSpace(RFirstName) || string.IsNullOrWhiteSpace(RSecondName) ||
                string.IsNullOrWhiteSpace(REmail)) return false;
            return hotel.GetUser(RLogin) == null;
        }

        private void RegisterExecute(object o)
        {
            var visitor = new Visitor
            {
                Login = RLogin,
                Password = RPassword,
                VisitorInfo = new VisitorInfo
                {
                    Name = RFirstName,
                    Surname = RSecondName,
                    Email = REmail
                },
                RegisterDate = DateTime.Now
            };

            hotel.AddUser(visitor);
            mainController.LoadLoginDashboard();
        }

        private void BackToLoginCommandExecute(object o)
        {
            mainController.LoadLoginDashboard();
        }
        #endregion


        public RegistrationControlModel(MainController mainController, IHotelSystem hotel)
        {
            this.mainController = mainController;
            this.hotel = hotel;
            RegisterCommand = new RelayCommand(RegisterExecute, RegisterCanExecute);
            BackToLoginCommand = new RelayCommand(BackToLoginCommandExecute);
        }

        public void Refresh()
        {
            RLogin = string.Empty;
            RPassword = string.Empty;
            RFirstName = string.Empty;
            RSecondName = string.Empty;
            REmail = string.Empty;
        }
    }
}
