using HotelManagementSystem.Core.Data;
using HotelManagementSystem.UI.DialogService;
using HotelManagementSystem.UI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementSystem.UI.ViewModel.Dialog
{
    public class RoomBookingsDialogWindowModel : DependencyObject, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ObservableCollection<Booking> BookingCollection
        {
            get { return (ObservableCollection<Booking>)GetValue(BookingCollectionProperty); }
            set { SetValue(BookingCollectionProperty, value); }
        }
        public static readonly DependencyProperty BookingCollectionProperty =
            DependencyProperty.Register("BookingCollection", typeof(ObservableCollection<Booking>), typeof(RoomBookingsDialogWindowModel), new PropertyMetadata(null));

        public RoomBookingsDialogWindowModel(IEnumerable<Booking> bookings)
        {
            OkCommand = new RelayCommand(o => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new RelayCommand(o => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
            BookingCollection = new ObservableCollection<Booking>(bookings);
        }
    }
}
