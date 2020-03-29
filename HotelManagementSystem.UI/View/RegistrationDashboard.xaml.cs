using System.Windows;
using System.Windows.Controls;

namespace HotelManagementSystem.UI.View
{
    public partial class RegistrationDashboard : UserControl
    {
        private readonly Window _window;

        public RegistrationDashboard(Window window, object dataContext)
        {
            InitializeComponent();

            _window = window;
            DataContext = dataContext;
        }

        public void Load()
        {
            _window.Hide();
            _window.Content = this;
            _window.Title = "Register";
            _window.Height = 290;
            _window.Width = 300;
            _window.MinHeight = 290;
            _window.MinWidth = 300;
            _window.Show();
            _window.WindowState = WindowState.Normal;
        }
    }
}
