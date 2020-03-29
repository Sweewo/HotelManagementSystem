using System.Windows.Controls;

namespace HotelManagementSystem.UI.View
{
    public partial class LoginControl : UserControl
    {
        public LoginControl(object dataContext)
        {
            InitializeComponent();

            DataContext = dataContext;
        }
    }
}
