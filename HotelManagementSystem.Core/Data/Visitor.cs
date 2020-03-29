using System;

namespace HotelManagementSystem.Core.Data
{
    public class Visitor : User
    {
        public DateTime RegisterDate { get; set; }

        public int VisitorInfoId { get; set; }
        public VisitorInfo VisitorInfo { get; set; }

        public Visitor()
        {
            RegisterDate = DateTime.Now;
            UserType = Additional.UserType.Visitor;
        }

    }
}