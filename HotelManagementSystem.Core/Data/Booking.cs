using HotelManagementSystem.Core.Data.Additional;
using System;

namespace HotelManagementSystem.Core.Data
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime InDate { get; set; }
        public DateTime OutDate { get; set; }
        public RoomStatus Status { get; set; }

        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; }

        public Room Room { get; set; }
        public int RoomId { get; set; }

        public Booking() { }

        public Booking(DateTime inDate, DateTime outDate, RoomStatus status, Visitor visitor)
        {
            InDate = inDate;
            OutDate = outDate;
            Status = status;
            Visitor = visitor;
        }
    }
}