using HotelManagementSystem.Core.Data;
using HotelManagementSystem.Core.Data.Additional;
using HotelManagementSystem.Core.Service;
using HotelManagementSystem.UI.DialogService;
using HotelManagementSystem.UI.Model;
using HotelManagementSystem.UI.ViewModel.Dialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace HotelManagementSystem.UI.ViewModel
{
    public class VisitorRoomsControlModel : DependencyObject
    {
        private readonly MainController _mainController;
        private readonly IHotelSystem _hotel;
        private readonly IDialogService _dialogService;

        #region dependency properties

        public Room SelectedRoomV
        {
            get { return (Room)GetValue(SelectedRoomVProperty); }
            set { SetValue(SelectedRoomVProperty, value); }
        }

        public static readonly DependencyProperty SelectedRoomVProperty =
            DependencyProperty.Register("SelectedRoomV", typeof(Room), typeof(VisitorRoomsControlModel),
                new PropertyMetadata(null));

        public ObservableCollection<Room> RoomCollectionV
        {
            get { return (ObservableCollection<Room>)GetValue(RoomCollectionVProperty); }
            set { SetValue(RoomCollectionVProperty, value); }
        }

        public static readonly DependencyProperty RoomCollectionVProperty =
            DependencyProperty.Register("RoomCollectionV", typeof(ObservableCollection<Room>),
                typeof(VisitorRoomsControlModel), new PropertyMetadata(null));

        public DateTime ShownDateTimeV
        {
            get { return (DateTime)GetValue(ShownDateTimeVProperty); }
            set { SetValue(ShownDateTimeVProperty, value); }
        }

        public static readonly DependencyProperty ShownDateTimeVProperty =
            DependencyProperty.Register("ShownDateTimeV", typeof(DateTime), typeof(VisitorRoomsControlModel),
                new PropertyMetadata(null));

        public IList<string> SortRoomsTypesV
        {
            get { return (IList<string>)GetValue(SortRoomsTypesVProperty); }
            set { SetValue(SortRoomsTypesVProperty, value); }
        }

        public static readonly DependencyProperty SortRoomsTypesVProperty =
            DependencyProperty.Register("SortRoomsTypesV", typeof(IList<string>), typeof(VisitorRoomsControlModel),
                new PropertyMetadata(null));

        private static void SortRoomsSelectedTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VisitorRoomsControlModel model = d as VisitorRoomsControlModel;
            if (model is null)
                return;

            string value = e.NewValue as string;
            if (value is null)
                return;

            if (model.RoomCollectionViewV.GroupDescriptions.Count > 0
                || model.RoomCollectionViewV.SortDescriptions.Count > 0)
            {
                model.RoomCollectionViewV.GroupDescriptions.RemoveAt(0);
                model.RoomCollectionViewV.SortDescriptions.RemoveAt(0);
            }

            model.RoomCollectionViewV.GroupDescriptions.Add(new PropertyGroupDescription(value));
            model.RoomCollectionViewV.SortDescriptions.Add(new SortDescription(value, ListSortDirection.Ascending));

            model.RoomCollectionViewV.Refresh();
        }

        public string SortRoomsSelectedTypeV
        {
            get { return (string)GetValue(SortRoomsSelectedTypeVProperty); }
            set { SetValue(SortRoomsSelectedTypeVProperty, value); }
        }

        public static readonly DependencyProperty SortRoomsSelectedTypeVProperty =
            DependencyProperty.Register("SortRoomsSelectedTypeV", typeof(string), typeof(VisitorRoomsControlModel),
                new PropertyMetadata(SortRoomsSelectedTypeChanged));

        public CollectionView RoomCollectionViewV
        {
            get { return (CollectionView)GetValue(RoomCollectionViewVProperty); }
            set { SetValue(RoomCollectionViewVProperty, value); }
        }

        public static readonly DependencyProperty RoomCollectionViewVProperty =
            DependencyProperty.Register("RoomCollectionViewV", typeof(CollectionView), typeof(VisitorRoomsControlModel),
                new PropertyMetadata(null));

        #endregion

        #region commands
        public ICommand NextDayCommand { get; set; }
        public ICommand PrevDayCommand { get; set; }
        public ICommand AddBookingCommand { get; set; }

        private void NextDayCommandExecute(object o)
        {
            ShownDateTimeV = ShownDateTimeV.AddDays(1);
        }

        private void PrevDayCommandExecute(object o)
        {
            ShownDateTimeV = ShownDateTimeV.AddDays(-1);
        }

        private void AddBookingCommandExecute(object obj)
        {
            Booking booking = new Booking();

            booking.Visitor = _hotel.ActiveUser as Visitor;
            booking.Room = _hotel.GetRoomById((int)obj);
            booking.Status = RoomStatus.Reserved;

            var viewModel = new AddBookingDialogWindowModel(ShownDateTimeV, booking, _hotel, (int)obj);
            bool? result = _dialogService.ShowDialog(viewModel);
            if (result is true)
                _hotel.AddBooking(booking);

        }

        #endregion

        public VisitorRoomsControlModel(MainController mainController, IHotelSystem hotel, IDialogService dialogService)
        {
            _mainController = mainController;
            _hotel = hotel;
            _dialogService = dialogService;

            NextDayCommand = new RelayCommand(NextDayCommandExecute);
            PrevDayCommand = new RelayCommand(PrevDayCommandExecute);
            AddBookingCommand = new RelayCommand(AddBookingCommandExecute);

            RoomCollectionV = new ObservableCollection<Room>();
            RoomCollectionViewV = (CollectionView)CollectionViewSource.GetDefaultView(RoomCollectionV);

            SortRoomsTypesV = new List<string>()
            {
                "Type",
                "Subtype"
            };
            ShownDateTimeV = DateTime.Now.Date;
        }

        public void Refresh()
        {
            RoomCollectionV.Clear();
            foreach (var room in _hotel.GetAllRoomsWithBookings())
                RoomCollectionV.Add(room);

            SortRoomsSelectedTypeV = SortRoomsTypesV[0];
            ShownDateTimeV = DateTime.Now.Date;
        }
    }
}
