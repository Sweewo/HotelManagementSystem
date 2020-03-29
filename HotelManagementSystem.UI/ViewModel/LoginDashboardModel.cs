using HotelManagementSystem.Core.Service;
using HotelManagementSystem.UI.View;
using System.Windows;
using System.Windows.Controls;

namespace HotelManagementSystem.UI.ViewModel
{
    public class LoginDashboardModel : DependencyObject
    {
        private readonly MainController _mainController;
        private readonly IHotelSystem _hotel;

        private readonly LoginControlModel _loginControlModel;

        public UserControl LoginControl
        {
            get { return (UserControl)GetValue(LoginControlProperty); }
            set { SetValue(LoginControlProperty, value); }
        }
        public static readonly DependencyProperty LoginControlProperty =
            DependencyProperty.Register("LoginControl", typeof(UserControl), typeof(LoginDashboardModel), new PropertyMetadata(null));

        public LoginDashboardModel(MainController mainController, IHotelSystem hotel)
        {
            _mainController = mainController;
            _hotel = hotel;

            _loginControlModel = new LoginControlModel(_mainController, hotel);
            LoginControl = new LoginControl(_loginControlModel);
        }

        public void Refresh()
        {
            _loginControlModel.Refresh();
        }
    }
}
