using HotelManagementSystem.Core.Data;
using HotelManagementSystem.Core.Service;
using HotelManagementSystem.UI.DialogService;
using HotelManagementSystem.UI.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementSystem.UI.ViewModel.Dialog
{
    public class AddBookingDialogWindowModel : DependencyObject, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public Booking Booking
        {
            get { return (Booking)GetValue(BookingProperty); }
            set { SetValue(BookingProperty, value); }
        }
        public static readonly DependencyProperty BookingProperty =
            DependencyProperty.Register("Booking", typeof(Booking), typeof(AddBookingDialogWindowModel), new PropertyMetadata(null));

        public DateTime InDate
        {
            get { return (DateTime)GetValue(InDateProperty); }
            set { SetValue(InDateProperty, value); }
        }
        public static readonly DependencyProperty InDateProperty =
            DependencyProperty.Register("InDate", typeof(DateTime), typeof(AddBookingDialogWindowModel), new PropertyMetadata(null));

        public DateTime OutDate
        {
            get { return (DateTime)GetValue(OutDateProperty); }
            set { SetValue(OutDateProperty, value); }
        }
        public static readonly DependencyProperty OutDateProperty =
            DependencyProperty.Register("OutDate", typeof(DateTime), typeof(AddBookingDialogWindowModel), new PropertyMetadata(null));

        private IHotelSystem hotel;
        private int roomId;
        public AddBookingDialogWindowModel(DateTime dateTime, Booking booking, IHotelSystem hotel, int roomId)
        {
            Booking = booking;
            this.hotel = hotel;
            this.roomId = roomId;
            InDate = dateTime;
            OutDate = InDate.AddDays(1);

            OkCommand = new RelayCommand(OkCommandExecute, CanOkCommandExecute);
            CancelCommand = new RelayCommand(o => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        private void OkCommandExecute(object obj)
        {
            Booking.InDate = InDate;
            Booking.OutDate = OutDate;
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        private bool CanOkCommandExecute(object arg)
        {
            if (InDate.Date < OutDate.Date)
            {
                List<Booking> bookings = new List<Booking>(hotel.GetRoomBookings(roomId));
                if (Room.GetRoomStatusForALongDate(InDate, OutDate, bookings) == Core.Data.Additional.RoomStatus.Available)
                    return true;
            }
            return false;
        }
    }
}
