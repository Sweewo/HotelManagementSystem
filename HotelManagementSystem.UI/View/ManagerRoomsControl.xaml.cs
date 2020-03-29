using System.Windows.Controls;

namespace HotelManagementSystem.UI.View
{
    public partial class ManagerRoomsControl : UserControl
    {
        public ManagerRoomsControl(object o)
        {
            InitializeComponent();
            DataContext = o;
        }
    }
}
