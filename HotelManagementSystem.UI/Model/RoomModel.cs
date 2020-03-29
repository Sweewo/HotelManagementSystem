using HotelManagementSystem.Core.Data;
using HotelManagementSystem.Core.Data.Additional;
using System.Collections.Generic;
using System.Windows;

namespace HotelManagementSystem.UI.Model
{
    public class RoomModel : DependencyObject
    {
        #region dependency property
        public int Id { get; private set; }


        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register("Number", typeof(int), typeof(RoomModel), new PropertyMetadata(null));

        public int Price
        {
            get { return (int)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(int), typeof(RoomModel), new PropertyMetadata(null));

        public RoomType Type
        {
            get { return (RoomType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(RoomType), typeof(RoomModel), new PropertyMetadata(null));

        public RoomSubtype Subtype
        {
            get { return (RoomSubtype)GetValue(SubtypeProperty); }
            set { SetValue(SubtypeProperty, value); }
        }

        public static readonly DependencyProperty SubtypeProperty =
            DependencyProperty.Register("Subtype", typeof(RoomSubtype), typeof(RoomModel), new PropertyMetadata(null));

        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(double), typeof(RoomModel), new PropertyMetadata(null));


        public List<Booking> Bookings
        {
            get { return (List<Booking>)GetValue(BookingsProperty); }
            set { SetValue(BookingsProperty, value); }
        }
        public static readonly DependencyProperty BookingsProperty =
            DependencyProperty.Register("Bookings", typeof(List<Booking>), typeof(RoomModel), new PropertyMetadata(null));
        #endregion

        public RoomModel(Room room)
        {
            Number = room.Number;
            Price = room.Price;
            Type = room.Type;
            Subtype = room.Subtype;
            Size = room.Size;
            Id = room.Id;

            Bookings = (room.Bookings);
        }

        public Room Room
        {
            get { return new Room(Number, Price, Type, Subtype, Size) { Id = Id, Bookings = Bookings }; }
        }
    }
}