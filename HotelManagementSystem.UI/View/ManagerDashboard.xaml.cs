using System.Windows;
using System.Windows.Controls;

namespace HotelManagementSystem.UI.View
{
    public partial class ManagerDashboard : UserControl
    {
        private readonly Window _window;

        public ManagerDashboard(Window window, object dataContext)
        {
            InitializeComponent();

            _window = window;
            DataContext = dataContext;
        }

        public void Load()
        {
            _window.Hide();
            _window.Content = this;
            _window.Title = "Manager";
            _window.MinHeight = 450;
            _window.MinWidth = 800;
            _window.WindowState = WindowState.Maximized;
            _window.Show();
        }
    }
}

