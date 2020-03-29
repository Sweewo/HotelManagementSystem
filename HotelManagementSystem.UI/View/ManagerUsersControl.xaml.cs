using System.Windows.Controls;

namespace HotelManagementSystem.UI.View
{
    public partial class ManagerUsersControl : UserControl
    {
        public ManagerUsersControl(object o)
        {
            InitializeComponent();
            DataContext = o;
        }
    }
}
