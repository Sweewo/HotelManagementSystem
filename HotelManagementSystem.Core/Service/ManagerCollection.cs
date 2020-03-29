using HotelManagementSystem.Core.Data;
using HotelManagementSystem.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Core.Service
{
    public class ManagerCollection:SaveableCollection<Manager>
    {
        public ManagerCollection(IManagerRepository managerRepository):base(managerRepository){}

    }
}
