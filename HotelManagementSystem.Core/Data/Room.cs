using HotelManagementSystem.Core.Data.Additional;
using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Core.Data
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
        public RoomType Type { get; set; }
        public RoomSubtype Subtype { get; set; }
        public double Size { get; set; }

        public List<Booking> Bookings { get; set; }

        public Room() { }

        public Room(int number, int price, RoomType type, RoomSubtype subtype, double size)
        {
            Number = number;
            Price = price;
            Type = type;
            Subtype = subtype;
            Size = size;
            Bookings = new List<Booking>();
        }

        public static int CalcDays(DateTime inDate, DateTime outDate)
        {
           return (outDate.Date - inDate.Date).Days;
        }

        public static Booking FindBookingByDate(DateTime date,List<Booking> bookings)
        {
            foreach (Booking boo in bookings)
            {
                int days = CalcDays(boo.InDate,boo.OutDate);
                for (int i = 0; i < days; i++)
                    if (boo.InDate.Date.AddDays(i).Date == date.Date)
                        return boo;
            }
            return null;
        }

        public static RoomStatus GetRoomStatusForADate(DateTime date,List<Booking> bookings)
        {
            Booking b = FindBookingByDate(date,bookings);
            if (b is null)
                return RoomStatus.Available;
            else
                return b.Status;
        }

        public static RoomStatus GetRoomStatusForALongDate(DateTime inDate, DateTime outDate, List<Booking> bookings)
        {
            int days = CalcDays(inDate, outDate);
            for (int i = 0; i < days; i++)
            {
                RoomStatus roomStatus = GetRoomStatusForADate(inDate.Date.AddDays(i), bookings);
                if (roomStatus != RoomStatus.Available)
                    return roomStatus;
            }
            return RoomStatus.Available;
        }

    }
}