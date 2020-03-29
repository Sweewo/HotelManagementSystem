using HotelManagementSystem.Core.Data;
using HotelManagementSystem.Core.Repository;
using System.Collections.Generic;

namespace HotelManagementSystem.Core.Service
{
    public class RoomCollection:SaveableCollection<Room>
    {
        public RoomCollection(IRoomRepository roomRepository):base(roomRepository){}
    }
}
