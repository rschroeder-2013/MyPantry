using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataTransferObjects;
using LogicLayer;

namespace MyPantry
{
    /// <summary>
    /// Interaction logic for frmUpdateUserPassword.xaml
    /// </summary>
    public partial class frmUpdateUserPassword : Window
    {
        private IUserManager _userManager;
        private User _user;
        private bool _isNewUser;

        public frmUpdateUserPassword(IUserManager userManager, User user,
            bool isNewUser = false)
        {
            _userManager = userManager;
            _user = user;
            _isNewUser = isNewUser;

            InitializeComponent();
        }

        

        private void btnSubmitPasswordChange_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = pwdNewPassword.Password;
            string oldPassword = pwdOldPassword.Password;
            string retypePassword = pwdRetypePassword.Password;
            string email = txtEmail.Text;

            // validation checks
            if (!email.isValidEmail() || email != _user.Email)
            {
                MessageBox.Show("Invalid Email");
                txtEmail.Clear();
                txtEmail.Focus();
                return;
            }

            if (!oldPassword.isValidPassword())
            {
                MessageBox.Show("Incorrect Password");
                pwdOldPassword.Clear();
                pwdOldPassword.Focus();
                return;
            }

            if (!newPassword.isValidPassword()
                || newPassword == "password")
            {
                MessageBox.Show("Invalid Password");
                pwdNewPassword.Clear();
                pwdNewPassword.Focus();
                return;
            }
            if (retypePassword != newPassword)
            {
                MessageBox.Show("Passwords must match.");
                pwdRetypePassword.Clear();
                pwdRetypePassword.Focus();
                return;
            }

            try
            {
                if (_userManager.UpdatePassword(_user.Email,
                    oldPassword, newPassword) == true)
                {
                    MessageBox.Show("Password successfully updated.");
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnCancelPasswordChange_Click(object sender, RoutedEventArgs e)
        {
            _user = null;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isNewUser)
            {
                txtEmail.Text = _user.Email;
                txtEmail.IsEnabled = false;
                pwdOldPassword.Password = "password";
                pwdOldPassword.IsEnabled = false;
                pwdNewPassword.Focus();
                btnSubmitPasswordChange.IsDefault = true;
            }
            else
            {
                txtEmail.IsEnabled = true;
                pwdOldPassword.IsEnabled = true;
                txtEmail.Focus();
            }
        }
    }
}
