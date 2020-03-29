using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementSystem.Core.Data;

namespace HotelManagementSystem.Core.Repository
{
    public class VisitorInfoRepository:Repository<VisitorInfo>,IVisitorInfoRepository
    {
        public VisitorInfoRepository(DbContext context) : base(context)
        {
        }
    }
}
