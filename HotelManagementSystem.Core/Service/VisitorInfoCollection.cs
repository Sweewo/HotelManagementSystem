using HotelManagementSystem.Core.Data;
using HotelManagementSystem.Core.Repository;

namespace HotelManagementSystem.Core.Service
{
    public class VisitorInfoCollection:SaveableCollection<VisitorInfo>
    {
        public VisitorInfoCollection(IRepository<VisitorInfo> repository) : base(repository)
        {
        }
    }
}
