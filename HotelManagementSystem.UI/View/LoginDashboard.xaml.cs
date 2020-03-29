using System.Windows;
using System.Windows.Controls;

namespace HotelManagementSystem.UI.View
{
    public partial class LoginDashboard : UserControl
    {
        private readonly Window _window;

        public LoginDashboard(Window window, object dataContext)
        {
            InitializeComponent();

            _window = window;
            DataContext = dataContext;

        }

        public void Load()
        {
            _window.Hide();
            _window.Content = this;
            _window.Title = "Login";
            _window.Height = 290;
            _window.Width = 300;
            _window.MinHeight = 290;
            _window.MinWidth = 300;
            _window.Show();
            _window.WindowState = WindowState.Normal;
        }
    }
}