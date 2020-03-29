using System.Windows.Controls;

namespace HotelManagementSystem.UI.View
{
    public partial class VisitorBookingsControl : UserControl
    {
        public VisitorBookingsControl(object dataContext)
        {
            InitializeComponent();

            DataContext = dataContext;
        }
    }
}
