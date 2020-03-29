using HotelManagementSystem.Core.Data;
using HotelManagementSystem.Core.Service;
using HotelManagementSystem.UI.DialogService;
using System.Collections.ObjectModel;
using System.Windows;

namespace HotelManagementSystem.UI.ViewModel
{
    public class VisitorBookingsControlModel : DependencyObject
    {
        private readonly MainController _mainController;
        private readonly IHotelSystem _hotel;
        private readonly IDialogService _dialogService;

        public VisitorBookingsControlModel(MainController mainController, IHotelSystem hotel, IDialogService dialogService)
        {
            _mainController = mainController;
            _hotel = hotel;
            _dialogService = dialogService;
        }

        public void Refresh()
        {
            BookingCollectionV = new ObservableCollection<Booking>(_hotel.GetVisitorBookingsWithRooms(_hotel.ActiveUser.Id));
        }

        public ObservableCollection<Booking> BookingCollectionV
        {
            get { return (ObservableCollection<Booking>)GetValue(BookingCollectionVProperty); }
            set { SetValue(BookingCollectionVProperty, value); }
        }
        public static readonly DependencyProperty BookingCollectionVProperty =
            DependencyProperty.Register("BookingCollectionV", typeof(ObservableCollection<Booking>), typeof(VisitorBookingsControlModel), new PropertyMetadata(null));



    }
}
