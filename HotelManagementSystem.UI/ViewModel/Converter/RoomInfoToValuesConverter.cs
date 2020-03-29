using HotelManagementSystem.Core.Data.Additional;
using System;
using System.Globalization;
using System.Windows.Data;

namespace HotelManagementSystem.UI.ViewModel.Converter
{
    public class RoomInfoToValuesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is RoomType)
            {
                RoomType val = (RoomType)value;
                if (val == RoomType.Single)
                    return MaterialDesignThemes.Wpf.PackIconKind.Account;
                if (val == RoomType.Double)
                    return MaterialDesignThemes.Wpf.PackIconKind.AccountMultiple;
                if (val == RoomType.Triple)
                    return MaterialDesignThemes.Wpf.PackIconKind.AccountMultiplePlus;
                if (val == RoomType.Family)
                    return MaterialDesignThemes.Wpf.PackIconKind.AccountMultiplePlusOutline;
            }

            else if (value is RoomSubtype)
            {
                RoomSubtype val = (RoomSubtype)value;
                if (val == RoomSubtype.Deluxe)
                    return MaterialDesignThemes.Wpf.PackIconKind.Numeric1Box;
                if (val == RoomSubtype.Superior)
                    return MaterialDesignThemes.Wpf.PackIconKind.Numeric2Box;
                if (val == RoomSubtype.Standard)
                    return MaterialDesignThemes.Wpf.PackIconKind.Numeric3Box;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
