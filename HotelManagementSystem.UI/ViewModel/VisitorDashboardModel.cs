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
    public class VisitorDashboardModel : DependencyObject
    {
        public static readonly string VisitorRoomsTabHeader = "Rooms";
        public static readonly string VisitorBookingsTabHeader = "Bookings";

        private readonly MainController _mainController;
        private readonly IHotelSystem _hotel;
        private readonly IDialogService _dialogService;

        private VisitorRoomsControlModel _roomsControlModel;
        private VisitorBookingsControlModel _bookingsControlModel;


        public ICommand LogoutCommand { get; set; }
        private void LogoutCommandExecute(object obj)
        {
            _mainController.LoadLoginDashboard();
        }

        public IList<TabItem> VisitorDashboardTabItems
        {
            get { return (IList<TabItem>)GetValue(VisitorDashboardTabItemsProperty); }
            set { SetValue(VisitorDashboardTabItemsProperty, value); }
        }
        public static readonly DependencyProperty VisitorDashboardTabItemsProperty =
            DependencyProperty.Register("VisitorDashboardTabItems", typeof(IList<TabItem>), typeof(VisitorDashboardModel), new PropertyMetadata(null));

        public TabItem VisitorDashboardSelectedTabItem
        {
            get { return (TabItem)GetValue(VisitorDashboardSelectedTabItemProperty); }
            set { SetValue(VisitorDashboardSelectedTabItemProperty, value); }
        }
        public static readonly DependencyProperty VisitorDashboardSelectedTabItemProperty =
            DependencyProperty.Register("VisitorDashboardSelectedTabItem", typeof(TabItem), typeof(VisitorDashboardModel), new PropertyMetadata(VisitorDashboardSelectedTabItemPropertyChanged));

        private static void VisitorDashboardSelectedTabItemPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var item = (e.NewValue as TabItem);
            var control = item?.Content as UserControl;
            var model = d as VisitorDashboardModel;
            if (item == null || control == null || model == null) return;

            if (item.Header.ToString() == VisitorRoomsTabHeader)
                model._roomsControlModel.Refresh();
            else if (item.Header.ToString() == VisitorBookingsTabHeader)
                model._bookingsControlModel.Refresh();
        }

        public VisitorDashboardModel(MainController mainController, IHotelSystem hotel, IDialogService dialogService)
        {
            _mainController = mainController;
            _hotel = hotel;
            _dialogService = dialogService;

            LogoutCommand = new RelayCommand(LogoutCommandExecute);

            InitializeTabs();
        }

        private void InitializeTabs()
        {
            VisitorDashboardTabItems = new List<TabItem>();

            var item1 = new TabItem();
            _roomsControlModel = new VisitorRoomsControlModel(_mainController, _hotel, _dialogService);
            var control1 = new VisitorRoomsControl(_roomsControlModel);
            item1.Content = control1;
            item1.Header = VisitorRoomsTabHeader;

            var item2 = new TabItem();
            _bookingsControlModel = new VisitorBookingsControlModel(_mainController, _hotel, _dialogService);
            var control2 = new VisitorBookingsControl(_bookingsControlModel);
            item2.Content = control2;
            item2.Header = VisitorBookingsTabHeader;

            VisitorDashboardTabItems.Add(item1);
            VisitorDashboardTabItems.Add(item2);
        }

        // make first tab selected
        public void Refresh()
        {
            if (Equals(VisitorDashboardSelectedTabItem, VisitorDashboardTabItems[0]))
                _roomsControlModel.Refresh();

            // it will be refreshed because of VisitorDashboardSelectedTabItemPropertyChanged
            VisitorDashboardSelectedTabItem = VisitorDashboardTabItems[0];
        }
    }
}
