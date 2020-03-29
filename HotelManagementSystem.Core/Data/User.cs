using HotelManagementSystem.Core.Data.Additional;

namespace HotelManagementSystem.Core.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public UserType UserType { get; set; }

        public User()
        {
            IsActive = true;
        }
    }
}