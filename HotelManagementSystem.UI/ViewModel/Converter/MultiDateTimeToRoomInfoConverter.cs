using HotelManagementSystem.Core.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace HotelManagementSystem.UI.ViewModel.Converter
{
    public class MultiDateTimeToRoomInfoConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2)
                return null;

            DateTime date = (DateTime)values[0];
            List<Booking> bookings = values[1] as List<Booking>;

            if (date == null || bookings is null)
                return null;

            // room status text
            if (parameter.ToString() == "statusText")
            {
                return Room.GetRoomStatusForADate(date, bookings).ToString();
            }

            // room status icon
            else if (parameter.ToString() == "statusIcon")
            {
                if (Room.GetRoomStatusForADate(date, bookings) == Core.Data.Additional.RoomStatus.Available)
                    return MaterialDesignThemes.Wpf.PackIconKind.Check;
                return MaterialDesignThemes.Wpf.PackIconKind.Account;
            }

            // room status days left
            else if (parameter.ToString() == "statusDays")
            {
                Booking b = Room.FindBookingByDate(date, bookings);
                if (b != null)
                {
                    return (b.OutDate.Date - date.Date).Days.ToString();
                }
                return 0.ToString();
            }

            // room status by
            else if (parameter.ToString() == "statusBy")
            {
                Booking b = Room.FindBookingByDate(date, bookings);
                if (b != null)
                {
                    return b.Visitor.VisitorInfo.Name + " " + b.Visitor.VisitorInfo.Surname;
                }
                return "Free room";
            }

            return null;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
