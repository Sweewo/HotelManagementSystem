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
    public class ManagerRoomsControlModel : DependencyObject
    {
        private readonly MainController _mainController;
        private readonly IHotelSystem _hotel;
        private readonly IDialogService _dialogService;

        #region dependency properties
        public DateTime ShownDateTime
        {
            get { return (DateTime)GetValue(ShownDateTimeProperty); }
            set { SetValue(ShownDateTimeProperty, value); }
        }
        public static readonly DependencyProperty ShownDateTimeProperty =
            DependencyProperty.Register("ShownDateTime", typeof(DateTime), typeof(ManagerRoomsControlModel),
                new PropertyMetadata(FilterTextChanged));

        public ObservableCollection<RoomModel> RoomCollection
        {
            get { return (ObservableCollection<RoomModel>)GetValue(RoomCollectionProperty); }
            set { SetValue(RoomCollectionProperty, value); }
        }
        public static readonly DependencyProperty RoomCollectionProperty =
            DependencyProperty.Register("RoomCollection", typeof(ObservableCollection<RoomModel>),
                typeof(ManagerRoomsControlModel), new PropertyMetadata(null));

        public RoomModel SelectedRoom
        {
            get { return (RoomModel)GetValue(SelectedRoomProperty); }
            set { SetValue(SelectedRoomProperty, value); }
        }
        public static readonly DependencyProperty SelectedRoomProperty =
            DependencyProperty.Register("SelectedRoom", typeof(RoomModel), typeof(ManagerRoomsControlModel),
                new PropertyMetadata(null));

        public CollectionView RoomCollectionView
        {
            get { return (CollectionView)GetValue(RoomCollectionViewProperty); }
            set { SetValue(RoomCollectionViewProperty, value); }
        }
        public static readonly DependencyProperty RoomCollectionViewProperty =
            DependencyProperty.Register("RoomCollectionView", typeof(CollectionView), typeof(ManagerRoomsControlModel),
                new PropertyMetadata(null));

        public IList<string> SortRoomsTypes
        {
            get { return (IList<string>)GetValue(SortRoomsTypesProperty); }
            set { SetValue(SortRoomsTypesProperty, value); }
        }
        public static readonly DependencyProperty SortRoomsTypesProperty =
            DependencyProperty.Register("SortRoomsTypes", typeof(IList<string>), typeof(ManagerRoomsControlModel),
                new PropertyMetadata(null));

        public string SortRoomsSelectedType
        {
            get { return (string)GetValue(SortRoomsSelectedTypeProperty); }
            set { SetValue(SortRoomsSelectedTypeProperty, value); }
        }
        public static readonly DependencyProperty SortRoomsSelectedTypeProperty =
            DependencyProperty.Register("SortRoomsSelectedType", typeof(string), typeof(ManagerRoomsControlModel),
                new PropertyMetadata(SortRoomsSelectedTypeChanged));

        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(ManagerRoomsControlModel),
                new PropertyMetadata(FilterTextChanged));

        private static void FilterTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = d as ManagerRoomsControlModel;
            model?.RoomCollectionView.Refresh();
        }

        private static void SortRoomsSelectedTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ManagerRoomsControlModel model))
                return;

            if (!(e.NewValue is string value))
                return;

            if (model.RoomCollectionView.GroupDescriptions.Count > 0 || model.RoomCollectionView.SortDescriptions.Count > 0)
            {
                model.RoomCollectionView.GroupDescriptions.RemoveAt(0);
                model.RoomCollectionView.SortDescriptions.RemoveAt(0);
            }

            model.RoomCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(value));
            model.RoomCollectionView.SortDescriptions.Add(new SortDescription(value, ListSortDirection.Ascending));

            model.RoomCollectionView.Refresh();
        }

        #endregion

        #region commands
        public ICommand NextDayCommand { get; set; }
        public ICommand PrevDayCommand { get; set; }
        public ICommand AddRoomCommand { get; set; }
        public ICommand RemoveRoomCommand { get; set; }
        public ICommand RoomAboutCommand { get; set; }
        public ICommand RoomBookingsCommand { get; set; }

        private void NextDayCommandExecute(object o)
        {
            ShownDateTime = ShownDateTime.AddDays(1);
        }

        private void PrevDayCommandExecute(object o)
        {
            ShownDateTime = ShownDateTime.AddDays(-1);
        }

        private void AddRoomCommandExecute(object o)
        {
            var rnd = new Random();
            var room = new Room(rnd.Next(0, 100), rnd.Next(50, 200), (RoomType)rnd.Next(0, 4),
                (RoomSubtype)rnd.Next(0, 3), rnd.Next(20, 80));

            _hotel.AddRoom(room);
            RoomCollection.Add(new RoomModel(room));
            RoomCollectionView.Refresh();
        }

        private void RemoveRoomCommandExecute(object o)
        {
            _hotel.RemoveRoom(SelectedRoom.Id);
            RoomCollection.Remove(SelectedRoom);
            RoomCollectionView.Refresh();
        }

        private bool CanRemoveRoomCommand(object o)
        {
            return SelectedRoom != null;
        }

        private void RoomAboutCommandExecute(object obj)
        {
            RoomModel roomModel = null;
            foreach (var r in RoomCollection)
                if (r.Id == (int)obj)
                    roomModel = r;

            var viewModel = new RoomAboutDialogWindowModel(roomModel);
            var result = _dialogService.ShowDialog(viewModel);

            if (!(result is true)) return;
            {
                if (roomModel != null)
                {
                    var r = roomModel.Room;
                    _hotel.UpdateRoom(r);
                }

                RoomCollectionView.Refresh();
            }
        }

        private void RoomBookingsCommandExecute(object obj)
        {
            var bookings = _hotel.GetRoomBookings((int)obj);

            var viewModel = new RoomBookingsDialogWindowModel(bookings);
            _dialogService.ShowDialog(viewModel);
        }
        #endregion

        private bool RoomModelFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText))
                return true;

            if (!(obj is RoomModel room) || room.Bookings.Count == 0) return false;
            foreach (var boo in room.Bookings)
            {
                if (ShownDateTime.Date < boo.InDate.Date || ShownDateTime.Date >= boo.OutDate.Date) continue;
                if ((string.Concat(boo.Visitor.VisitorInfo.Name, " ", boo.Visitor.VisitorInfo.Surname).ToLower()
                        .Contains(FilterText)) ||
                    (string.Concat(boo.Visitor.VisitorInfo.Name, " ", boo.Visitor.VisitorInfo.Surname).ToUpper()
                        .Contains(FilterText)) ||
                    (string.Concat(boo.Visitor.VisitorInfo.Name, " ", boo.Visitor.VisitorInfo.Surname)
                        .Contains(FilterText)))
                    return true;
            }
            return false;
        }

        public ManagerRoomsControlModel(MainController mainController, IHotelSystem hotel, IDialogService dialogService)
        {
            _mainController = mainController;
            _hotel = hotel;
            _dialogService = dialogService;

            NextDayCommand = new RelayCommand(NextDayCommandExecute);
            PrevDayCommand = new RelayCommand(PrevDayCommandExecute);
            AddRoomCommand = new RelayCommand(AddRoomCommandExecute);
            RemoveRoomCommand = new RelayCommand(RemoveRoomCommandExecute, CanRemoveRoomCommand);
            RoomAboutCommand = new RelayCommand(RoomAboutCommandExecute);
            RoomBookingsCommand = new RelayCommand(RoomBookingsCommandExecute);

            RoomCollection = new ObservableCollection<RoomModel>();
            RoomCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(RoomCollection);
            RoomCollectionView.Filter = RoomModelFilter;

            SortRoomsTypes = new List<string>()
            {
                "Type",
                "Subtype"
            };
            ShownDateTime = DateTime.Now.Date;
        }

        public void Refresh()
        {
            RoomCollection.Clear();
            foreach (var room in _hotel.GetAllRoomsWithBookings())
                RoomCollection.Add(new RoomModel(room));

            SortRoomsSelectedType = SortRoomsTypes[0];
            ShownDateTime = DateTime.Now.Date;
        }
    }
}
