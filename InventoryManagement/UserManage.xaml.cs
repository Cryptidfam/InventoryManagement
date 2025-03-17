using System.Windows;
using System.Windows.Controls;

namespace InventoryManagement
{
    /// <summary>
    /// Interaction logic for UserManage.xaml
    /// </summary>
    public partial class UserManage : Window
    {
        readonly App app = (App)Application.Current;

        public UserManage()
        {
            InitializeComponent();
            DataContext = app.UserData;
        }
        private void UserGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserGrid.SelectedItem is User selectedUser)
            {
                foreach (ComboBoxItem item in RoleComboBox.Items)
                {
                    if (item.Tag.ToString() == selectedUser.Role.ToString())
                    {
                        RoleComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
        }
        private void ChangeRole_Click(object sender, RoutedEventArgs e)
        {
            if (UserGrid.SelectedItem is User selectedUser && RoleComboBox.SelectedItem is ComboBoxItem selectedRoleItem)
            {
                UserRole newRole = selectedRoleItem.Tag.ToString() == "Admin" ? UserRole.Admin : UserRole.Staff;

                selectedUser.Role = newRole;
            }
            else
            {
                MessageBox.Show("Please select a user and a role to change.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

