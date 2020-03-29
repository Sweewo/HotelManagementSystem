using System.Windows.Controls;

namespace HotelManagementSystem.UI.View
{
    public partial class VisitorRoomsControl : UserControl
    {
        public VisitorRoomsControl(object o)
        {
            InitializeComponent();

            DataContext = o;
        }
    }
}
