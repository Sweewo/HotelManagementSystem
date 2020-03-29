using HotelManagementSystem.Core.Data;
using System.Data.Entity;

namespace HotelManagementSystem.Core.Repository
{
    public class HotelContext : DbContext
    {
        public HotelContext() : base("HotelContext") { } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Manager>().ToTable("Manager");
            modelBuilder.Entity<Visitor>().ToTable("Visitor");
            modelBuilder.Entity<ManagerInfo>().ToTable("ManagerInfo");
            modelBuilder.Entity<VisitorInfo>().ToTable("VisitorInfo");
            modelBuilder.Entity<Room>().ToTable("Room");
            modelBuilder.Entity<Booking>().ToTable("Booking");
            base.OnModelCreating(modelBuilder);
        }
    }
}
