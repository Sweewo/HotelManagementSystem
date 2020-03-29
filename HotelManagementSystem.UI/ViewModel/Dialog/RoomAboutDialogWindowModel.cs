using HotelManagementSystem.Core.Data.Additional;
using HotelManagementSystem.UI.DialogService;
using HotelManagementSystem.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementSystem.UI.ViewModel.Dialog
{
    public class RoomAboutDialogWindowModel : DependencyObject, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }
        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register("Number", typeof(int), typeof(RoomAboutDialogWindowModel), new PropertyMetadata(null));

        public int Price
        {
            get { return (int)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }
        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(int), typeof(RoomAboutDialogWindowModel), new PropertyMetadata(null));

        public RoomType RoomType
        {
            get { return (RoomType)GetValue(RoomTypeProperty); }
            set { SetValue(RoomTypeProperty, value); }
        }
        public static readonly DependencyProperty RoomTypeProperty =
            DependencyProperty.Register("RoomType", typeof(RoomType), typeof(RoomAboutDialogWindowModel), new PropertyMetadata(null));

        public RoomSubtype RoomSubtype
        {
            get { return (RoomSubtype)GetValue(RoomSubtypeProperty); }
            set { SetValue(RoomSubtypeProperty, value); }
        }
        public static readonly DependencyProperty RoomSubtypeProperty =
            DependencyProperty.Register("RoomSubtype", typeof(RoomSubtype), typeof(RoomAboutDialogWindowModel), new PropertyMetadata(null));

        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(double), typeof(RoomAboutDialogWindowModel), new PropertyMetadata(null));


        public IList<RoomType> RoomTypes
        {
            get
            {
                return Enum.GetValues(typeof(RoomType)).Cast<RoomType>().ToList();
            }
        }

        public IList<RoomSubtype> RoomSubtypes
        {
            get
            {
                return Enum.GetValues(typeof(RoomSubtype)).Cast<RoomSubtype>().ToList();
            }
        }

        private RoomModel room;
        public RoomAboutDialogWindowModel(RoomModel room)
        {
            this.room = room;
            Number = room.Number;
            Price = room.Price;
            RoomType = room.Type;
            RoomSubtype = room.Subtype;
            Size = room.Size;

            OkCommand = new RelayCommand(OkCommandExecute, CanOkCommandExecute);
            CancelCommand = new RelayCommand(o => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        private bool CanOkCommandExecute(object arg)
        {
            return true;
        }

        private void OkCommandExecute(object obj)
        {
            room.Number = Number;
            room.Price = Price;
            room.Type = RoomType;
            room.Subtype = RoomSubtype;
            room.Size = Size;
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }
    }
}
