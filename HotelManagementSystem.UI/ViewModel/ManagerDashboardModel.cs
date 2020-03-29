using HotelManagementSystem.Core.Service;
using HotelManagementSystem.UI.DialogService;
using HotelManagementSystem.UI.Model;
using HotelManagementSystem.UI.View;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelManagementSystem.UI.ViewModel
{
    public class ManagerDashboardModel : DependencyObject
    {
        public static readonly string ManagerRoomsTabHeader = "Rooms";
        public static readonly string ManagerUsersTabHeader = "Users";

        private readonly MainController _mainController;
        private readonly IHotelSystem _hotel;
        private readonly IDialogService _dialogService;

        private ManagerRoomsControlModel _managerRoomsControlModel;
        private ManagerUsersControlModel _managerUsersControlModel;

        public IList<TabItem> ManagerDashboardTabItems
        {
            get { return (IList<TabItem>)GetValue(ManagerDashboardTabItemsProperty); }
            set { SetValue(ManagerDashboardTabItemsProperty, value); }
        }

        public static readonly DependencyProperty ManagerDashboardTabItemsProperty =
            DependencyProperty.Register("ManagerDashboardTabItems", typeof(IList<TabItem>),
                typeof(ManagerDashboardModel), new PropertyMetadata(null));

        public TabItem ManagerDashboardSelectedTabItem
        {
            get { return (TabItem)GetValue(ManagerDashboardSelectedTabItemProperty); }
            set { SetValue(ManagerDashboardSelectedTabItemProperty, value); }
        }

        public static readonly DependencyProperty ManagerDashboardSelectedTabItemProperty =
            DependencyProperty.Register("ManagerDashboardSelectedTabItem", typeof(TabItem),
                typeof(ManagerDashboardModel), new PropertyMetadata(ManagerDashboardSelectedTabItemPropertyChanged));

        // need reloading data
        private static void ManagerDashboardSelectedTabItemPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var item = e.NewValue as TabItem;
            var control = item?.Content as UserControl;
            var model = d as ManagerDashboardModel;
            if (item == null || control == null || model == null) return;

            if (item.Header.ToString() == ManagerRoomsTabHeader)
                model._managerRoomsControlModel.Refresh();
            else if (item.Header.ToString() == ManagerUsersTabHeader)
                model._managerUsersControlModel.Refresh();
        }

        public ICommand LogoutCommand { get; set; }

        private void LogoutCommandExecute(object obj)
        {
            _mainController.LoadLoginDashboard();
        }

        public ManagerDashboardModel(MainController mainController, IHotelSystem hotel,
            IDialogService dialogService)
        {
            _mainController = mainController;
            _hotel = hotel;
            _dialogService = dialogService;

            LogoutCommand = new RelayCommand(LogoutCommandExecute);

            InitializeTabs();
        }

        private void InitializeTabs()
        {
            ManagerDashboardTabItems = new List<TabItem>();

            var item1 = new TabItem();
            _managerRoomsControlModel = new ManagerRoomsControlModel(_mainController, _hotel, _dialogService);
            var control1 = new ManagerRoomsControl(_managerRoomsControlModel);
            item1.Content = control1;
            item1.Header = ManagerRoomsTabHeader;

            var item2 = new TabItem();
            _managerUsersControlModel = new ManagerUsersControlModel(_mainController, _hotel, _dialogService);
            var control2 = new ManagerUsersControl(_managerUsersControlModel);
            item2.Content = control2;
            item2.Header = ManagerUsersTabHeader;

            ManagerDashboardTabItems.Add(item1);
            ManagerDashboardTabItems.Add(item2);
        }

        // make first tab selected
        public void Refresh()
        {
            if (Equals(ManagerDashboardSelectedTabItem, ManagerDashboardTabItems[0]))
                _managerRoomsControlModel.Refresh();

            // it will be refreshed because of ManagerDashboardSelectedTabItemPropertyChanged
            ManagerDashboardSelectedTabItem = ManagerDashboardTabItems[0];
        }
    }
}