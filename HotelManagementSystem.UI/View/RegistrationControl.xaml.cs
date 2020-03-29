using System.Windows.Controls;

namespace HotelManagementSystem.UI.View
{
    public partial class RegistrationControl : UserControl
    {
        public RegistrationControl(object dataContext)
        {
            InitializeComponent();

            DataContext = dataContext;
        }
    }
}
