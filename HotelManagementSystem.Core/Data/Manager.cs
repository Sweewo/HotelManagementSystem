namespace HotelManagementSystem.Core.Data
{
    public class Manager:User
    {
        public int Salary { get; set; }

        public int ManagerInfoId { get; set; }
        public ManagerInfo ManagerInfo { get; set; }

        public Manager()
        {
            UserType = Additional.UserType.Manager;
        }
    }
}
