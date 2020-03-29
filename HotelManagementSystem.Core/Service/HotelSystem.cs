using System;
using HotelManagementSystem.Core.Data;
using System.Collections.Generic;
using System.Linq;
using HotelManagementSystem.Core.Data.Additional;

namespace HotelManagementSystem.Core.Service
{
    public class HotelSystem : IHotelSystem
    {
        private readonly UserCollection _userCollection;
        private readonly ManagerCollection _managerCollection;
        private readonly VisitorCollection _visitorCollection;
        private readonly ManagerInfoCollection _managerInfoCollection;
        private readonly VisitorInfoCollection _visitorInfoCollection;
        private readonly RoomCollection _roomCollection;
        private readonly BookingCollection _bookingCollection;

        public User ActiveUser { get; private set; }

        public HotelSystem(UserCollection userCollection, ManagerCollection managerCollection, VisitorCollection visitorCollection, ManagerInfoCollection managerInfoCollection, 
            VisitorInfoCollection visitorInfoCollection, RoomCollection roomCollection, BookingCollection bookingCollection)
        {
            _userCollection = userCollection;
            _managerCollection = managerCollection;
            _visitorCollection = visitorCollection;
            _managerInfoCollection = managerInfoCollection;
            _visitorInfoCollection = visitorInfoCollection;
            _roomCollection = roomCollection;
            _bookingCollection = bookingCollection;
        }

        public User GetUser(string login)
        {
            return _userCollection.GetUserByLogin(login);
        }

        public void AddRoom(Room room)
        {
            _roomCollection.Add(room);
            _roomCollection.Save();
        }


        public User Login(string login, string password)
        {
            var user = GetUser(login);
            if (user is null)
                return null;

            if (user.Password != password || !user.IsActive) return null;

            switch (user.UserType)
            {
                case UserType.Manager:
                    ((Manager) user).ManagerInfo = _managerInfoCollection.GetById((user as Manager).ManagerInfoId);
                    break;
                case UserType.Visitor:
                    ((Visitor) user).VisitorInfo = _visitorInfoCollection.GetById((user as Visitor).VisitorInfoId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ActiveUser = user;

            return user;

        }

        public void AddUser(User user)
        {
            if (user is Manager manager)
            {
                _managerCollection.Add(manager);
                _managerInfoCollection.Add(manager.ManagerInfo);
                _managerCollection.Save();
                _managerInfoCollection.Save();
            }
            else if (user is Visitor visitor)
            {
                _visitorCollection.Add(visitor);
                _visitorInfoCollection.Add(visitor.VisitorInfo);
                _visitorCollection.Save();
                _visitorInfoCollection.Save();
            }
        }

        public IEnumerable<Visitor> GetAllVisitorsWithInfoes()
        {
            IEnumerable<Visitor> visitors = _visitorCollection.GetAll();
            var allVisitorsWithInfoes = visitors.ToList();
            foreach (Visitor visitor in allVisitorsWithInfoes)
                visitor.VisitorInfo = _visitorInfoCollection.GetById(visitor.VisitorInfoId);
            return allVisitorsWithInfoes;
        }

        public IEnumerable<Room> GetAllRoomsWithBookings()
        {
            List<Room> rooms = _roomCollection.GetAll().ToList();
            foreach (Room r in rooms)
            {
                r.Bookings = new List<Booking>(_bookingCollection.FindAll(b => b.RoomId == r.Id));
                foreach (var booking in r.Bookings)
                {
                    booking.Visitor = _visitorCollection.GetById(booking.VisitorId);
                    booking.Visitor.VisitorInfo = _visitorInfoCollection.GetById(booking.Visitor.VisitorInfoId);
                }
            }

            return rooms;
        }

        public void RemoveRoom(int id)
        {
            _roomCollection.Remove(_roomCollection.GetById(id));
            _roomCollection.Save();
        }

        public Room GetRoomById(int id)
        {
            return _roomCollection.GetById(id);
        }

        public void UpdateRoom(Room r)
        {
            Room room = _roomCollection.GetById(r.Id);
            room.Number = r.Number;
            room.Price = r.Price;
            room.Type = r.Type;
            room.Subtype = r.Subtype;
            room.Size = r.Size;
            _roomCollection.Save();
        }

        public IEnumerable<Booking> GetRoomBookings(int id)
        {
            return _bookingCollection.FindAll(b => b.RoomId == id);
        }

        public IEnumerable<Booking> GetVisitorBookingsWithRooms(int id)
        {
            IEnumerable<Booking> bookings = _bookingCollection.FindAll(b => b.VisitorId == id);
            var visitorBookingsWithRooms = bookings.ToList();
            foreach (var boo in visitorBookingsWithRooms)
                boo.Room = _roomCollection.GetById(boo.RoomId);
            return visitorBookingsWithRooms;
        }

        public void AddBooking(Booking b)
        {
            _bookingCollection.Add(b);
            _bookingCollection.Save();
        }
    }
}
