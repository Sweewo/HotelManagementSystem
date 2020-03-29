using HotelManagementSystem.Core.Data;
using HotelManagementSystem.Core.Repository;

namespace HotelManagementSystem.Core.Service
{
    public class ManagerInfoCollection:SaveableCollection<ManagerInfo>
    {
        public ManagerInfoCollection(IRepository<ManagerInfo> repository) : base(repository)
        {
        }
    }
}
