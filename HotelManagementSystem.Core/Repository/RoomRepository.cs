using HotelManagementSystem.Core.Data;
using System.Collections.Generic;
using System.Data.Entity;

namespace HotelManagementSystem.Core.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(DbContext context) : base(context)
        {
        }
    }
}