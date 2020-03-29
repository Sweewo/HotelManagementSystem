using HotelManagementSystem.Core.Data;
using HotelManagementSystem.Core.Service;
using HotelManagementSystem.UI.DialogService;
using System.Collections.ObjectModel;
using System.Windows;

namespace HotelManagementSystem.UI.ViewModel
{
    public class ManagerUsersControlModel : DependencyObject
    {
        private readonly MainController _mainController;
        private readonly IHotelSystem _hotel;
        private readonly IDialogService _dialogService;

        public ObservableCollection<Visitor> VisitorCollection
        {
            get { return (ObservableCollection<Visitor>)GetValue(VisitorCollectionProperty); }
            set { SetValue(VisitorCollectionProperty, value); }
        }
        public static readonly DependencyProperty VisitorCollectionProperty =
            DependencyProperty.Register("VisitorCollection", typeof(ObservableCollection<Visitor>), typeof(ManagerUsersControlModel), new PropertyMetadata(null));

        public ManagerUsersControlModel(MainController mainController, IHotelSystem hotel, IDialogService dialogService)
        {
            _mainController = mainController;
            _hotel = hotel;
            _dialogService = dialogService;
        }

        public void Refresh()
        {
            VisitorCollection = new ObservableCollection<Visitor>(_hotel.GetAllVisitorsWithInfoes());
        }
    }
}
