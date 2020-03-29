using HotelManagementSystem.Core.Service;
using HotelManagementSystem.UI.View;
using System.Windows;
using System.Windows.Controls;

namespace HotelManagementSystem.UI.ViewModel
{
    public class RegistrationDashboardModel : DependencyObject
    {
        private readonly MainController _mainController;
        private readonly IHotelSystem _hotel;

        private readonly RegistrationControlModel _registrationControlModel;

        public UserControl RegistrationControl
        {
            get { return (UserControl)GetValue(RegistrationControlProperty); }
            set { SetValue(RegistrationControlProperty, value); }
        }
        public static readonly DependencyProperty RegistrationControlProperty =
            DependencyProperty.Register("RegistrationControl", typeof(UserControl), typeof(RegistrationDashboardModel), new PropertyMetadata(null));


        public RegistrationDashboardModel(MainController mainController, IHotelSystem hotel)
        {
            _mainController = mainController;
            _hotel = hotel;

            _registrationControlModel = new RegistrationControlModel(_mainController, _hotel);
            RegistrationControl = new RegistrationControl(_registrationControlModel);
        }

        public void Refresh()
        {
            _registrationControlModel.Refresh();
        }

    }
}
