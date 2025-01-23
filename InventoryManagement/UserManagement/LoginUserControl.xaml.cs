using System.Windows.Controls;

namespace InventoryManagement.UserManagement
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();

        }

    }
}
