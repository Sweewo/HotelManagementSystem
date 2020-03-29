using System.Data.Entity;
using HotelManagementSystem.Core.Data;

namespace HotelManagementSystem.Core.Repository
{
    public class UserRepository:Repository<User>,IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
