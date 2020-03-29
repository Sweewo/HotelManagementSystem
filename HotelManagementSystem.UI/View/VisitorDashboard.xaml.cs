using System.Windows;
using System.Windows.Controls;

namespace HotelManagementSystem.UI.View
{
    public partial class VisitorDashboard : UserControl
    {
        private readonly Window _window;

        public VisitorDashboard(Window window, object dataContext)
        {
            InitializeComponent();
            _window = window;
            DataContext = dataContext;
        }

        public void Load()
        {
            _window.Hide();
            _window.Content = this;
            _window.Title = "Visitor";
            _window.MinHeight = 450;
            _window.WindowState = WindowState.Maximized;
            _window.MinWidth = 800;
            _window.Show();
        }
    }
}
