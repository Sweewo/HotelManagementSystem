using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementSystem.Core.Data;
using HotelManagementSystem.Core.Repository;

namespace HotelManagementSystem.Core.Service
{
    public class UserCollection:SaveableCollection<User>
    {
        public UserCollection(IRepository<User> repository) : base(repository)
        {
        }

        public User GetUserByLogin(string login)
        {
            return Repository.Find(u => u.Login == login);
        }
    }
}
