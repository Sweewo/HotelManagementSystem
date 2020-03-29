using HotelManagementSystem.Core.Data;
using HotelManagementSystem.Core.Repository;
using HotelManagementSystem.Core.Service;
using HotelManagementSystem.UI.DialogService;
using HotelManagementSystem.UI.View.Dialog;
using HotelManagementSystem.UI.ViewModel.Dialog;
using System;
using System.Windows;

namespace HotelManagementSystem.UI
{
    public partial class App
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var context = new HotelContext();

            IUserRepository userRepository = new UserRepository(context);
            IManagerRepository managerRepository = new ManagerRepository(context);
            IVisitorRepository visitorRepository = new VisitorRepository(context);
            IManagerInfoRepository managerInfoRepository = new ManagerInfoRepository(context);
            IVisitorInfoRepository visitorInfoRepository = new VisitorInfoRepository(context);
            IRoomRepository roomRepository = new RoomRepository(context);
            IBookingRepository bookingRepository = new BookingRepository(context);

            UserCollection userCollection = new UserCollection(userRepository);
            ManagerCollection managerCollection = new ManagerCollection(managerRepository);
            VisitorCollection visitorCollection = new VisitorCollection(visitorRepository);
            ManagerInfoCollection managerInfoCollection = new ManagerInfoCollection(managerInfoRepository);
            VisitorInfoCollection visitorInfoCollection = new VisitorInfoCollection(visitorInfoRepository);
            RoomCollection roomCollection = new RoomCollection(roomRepository);
            BookingCollection bookingCollection = new BookingCollection(bookingRepository);

            IHotelSystem hotel = new HotelSystem(userCollection, managerCollection, visitorCollection,
                managerInfoCollection, visitorInfoCollection, roomCollection, bookingCollection);

            if (hotel.GetUser("root") is null)
            {
                hotel.AddUser(new Manager
                {
                    Login = "root",
                    Password = "root",
                    Salary = 123,
                    ManagerInfo = new ManagerInfo { Name = "lola", Surname = "brown", Email = "www@fw", StartDate = new DateTime(1999, 12, 12) }
                });
            }

            Window mainWindow = new MainWindow();

            IDialogService dialogService = new DialogService.DialogService(mainWindow);
            dialogService.Register<RoomAboutDialogWindowModel, RoomAboutDialogWindow>();
            dialogService.Register<RoomBookingsDialogWindowModel, RoomBookingsDialogWindow>();
            dialogService.Register<AddBookingDialogWindowModel, AddBookingDialogWindow>();

            MainController mainController = new MainController(hotel, mainWindow, dialogService);
            mainController.LoadLoginDashboard();
        }
    }
}
