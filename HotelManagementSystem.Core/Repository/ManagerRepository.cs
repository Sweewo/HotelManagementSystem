using HotelManagementSystem.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Core.Repository
{
    public class ManagerRepository : Repository<Manager>, IManagerRepository
    {
        public ManagerRepository(DbContext context) : base(context){}

    }
}
