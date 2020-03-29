using HotelManagementSystem.Core.Service;
using HotelManagementSystem.UI.DialogService;
using HotelManagementSystem.UI.View;
using HotelManagementSystem.UI.ViewModel;
using System.Windows;


namespace HotelManagementSystem.UI
{
    public class MainController
    {
        private readonly LoginDashboard _loginDashboard;
        private readonly RegistrationDashboard _registrationDashboard;
        private readonly ManagerDashboard _managerDashboard;
        private readonly VisitorDashboard _visitorDashboard;

        private readonly LoginDashboardModel _loginDashboardModel;
        private readonly RegistrationDashboardModel _registrationDashboardModel;
        private readonly ManagerDashboardModel _managerDashboardModel;
        private readonly VisitorDashboardModel _visitorDashboardModel;

        public MainController(IHotelSystem hotel, Window mainWindow, IDialogService dialogService)
        {
            _loginDashboardModel = new LoginDashboardModel(this, hotel);
            _loginDashboard = new LoginDashboard(mainWindow, _loginDashboardModel);

            _registrationDashboardModel = new RegistrationDashboardModel(this, hotel);
            _registrationDashboard = new RegistrationDashboard(mainWindow, _registrationDashboardModel);

            _managerDashboardModel = new ManagerDashboardModel(this, hotel, dialogService);
            _managerDashboard = new ManagerDashboard(mainWindow, _managerDashboardModel);

            _visitorDashboardModel = new VisitorDashboardModel(this, hotel, dialogService);
            _visitorDashboard = new VisitorDashboard(mainWindow, _visitorDashboardModel);
        }

        public void LoadManagerDashboard()
        {
            _managerDashboard.Load();
            _managerDashboardModel.Refresh();
        }

        public void LoadVisitorDashboard()
        {
            _visitorDashboard.Load();
            _visitorDashboardModel.Refresh();
        }

        public void LoadLoginDashboard()
        {
            _loginDashboard.Load();
            _loginDashboardModel.Refresh();
        }

        public void LoadRegistrationDashboard()
        {
            _registrationDashboard.Load();
            _registrationDashboardModel.Refresh();
        }
    }
}