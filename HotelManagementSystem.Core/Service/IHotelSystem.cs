using System;
using HotelManagementSystem.Core.Data;
using HotelManagementSystem.Core.Data.Additional;
using System.Collections.Generic;

namespace HotelManagementSystem.Core.Service
{
    public interface IHotelSystem
    {
        User ActiveUser { get;}

        void AddUser(User user);
        User GetUser(string login);
        User Login(string login, string password);
        IEnumerable<Visitor> GetAllVisitorsWithInfoes();

        IEnumerable<Room> GetAllRoomsWithBookings();
        void AddRoom(Room room);
        void RemoveRoom(int id);
        Room GetRoomById(int id);
        void UpdateRoom(Room room);

        IEnumerable<Booking> GetRoomBookings(int id);
        IEnumerable<Booking> GetVisitorBookingsWithRooms(int id);
        void AddBooking(Booking booking);
    }
}
