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
    /// Interaction logic for frmAddEditUserInfo.xaml
    /// </summary>
    public partial class frmAddEditUserInfo : Window
    {
        private EmployeeVM _employee;
        private bool _addEmployee = false;
        private List<string> _originalUnassignedRoles = new List<string>();
        private IEmployeeManager _employeeManager = new EmployeeManager();
        private List<string> _assignedRoles;
        private List<string> _unassignedRoles;


        public frmAddEditUserInfo()
        {
            _employee = new EmployeeVM();
            _addEmployee = true;

            InitializeComponent();
        }

        public frmAddEditUserInfo(EmployeeVM employee)
        {
            _employee = employee;

            InitializeComponent();
        }


        private void setupEdit()
        {
            btnEditSave.Content = "Save";
            txtUserFirstName.IsReadOnly = false;
            txtUserFirstName.BorderBrush = Brushes.Black;

            txtUserLastName.IsReadOnly = false;
            txtUserLastName.BorderBrush = Brushes.Black;

            txtUserEmail.IsReadOnly = false;
            txtUserEmail.BorderBrush = Brushes.Black;

            txtUserPhoneNumber.IsReadOnly = false;
            txtUserPhoneNumber.BorderBrush = Brushes.Black;

            lstAssignedRoles.IsEnabled = true;
            lstUnassignedRoles.IsEnabled = true;

            chkActiveStatus.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addEmployee)
            {
                this.Title = "Add New Employee";

                // display blank user
                tbkTitle.Text = "Add New User";
                lblUserID.Content = "Assigned Automatically";
                txtUserFirstName.Text = "";
                txtUserLastName.Text = "";
                txtUserEmail.Text = "";
                txtUserPhoneNumber.Text = "";
                chkActiveStatus.IsChecked = true;
                lstAssignedRoles.IsEnabled = true;
                lstUnassignedRoles.IsEnabled = true;

                setupEdit();
                chkActiveStatus.IsEnabled = false;
            }
            else
            {
                lblUserID.Content = _employee.EmployeeID;
                txtUserFirstName.Text = _employee.FirstName;
                txtUserLastName.Text = _employee.LastName;
                txtUserEmail.Text = _employee.Email;
                txtUserPhoneNumber.Text = _employee.PhoneNumber;
                chkActiveStatus.IsChecked = _employee.Active;
                try
                {

                    // populate the _assignedRoles and save them
                    _assignedRoles =
                        _employeeManager.RetrieveRolesByEmployeeID(_employee.EmployeeID);

                    //make a copy of the _assignedRoles that will be preserved
                    _employee.Roles = new List<string>();
                    foreach (var r in _assignedRoles)
                    {
                        _employee.Roles.Add(r); //holds the original state
                    }

                    //use the dynamically edited _assignedRoles as the list data source
                    lstAssignedRoles.ItemsSource = _assignedRoles;

                    //create the dynamic list of _unassignedRoles
                    _unassignedRoles = _employeeManager.RetrieveAllRoles();
                    foreach (var role in _assignedRoles)
                    {
                        _unassignedRoles.Remove(role);
                    }

                    //make a seperate copy of _unassignedRoles to preserve the original state
                    foreach (var r in _unassignedRoles)
                    {
                        _originalUnassignedRoles.Add(r);
                    }

                    //use the dynamic _unassignedRoles as the list items source
                    lstUnassignedRoles.ItemsSource = _unassignedRoles;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n"
                        + ex.InnerException.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnEditSave.Content == "Edit")
            {
                tbkTitle.Text = "Edit User";
                this.Title = "Edit Employee Data";
                setupEdit();
            }
            else // save operation
            {
                // input validation
                if (txtUserFirstName.Text.isValidFirstName() &&
                    txtUserLastName.Text.isValidLastName() &&
                    txtUserEmail.Text.isValidEmail() &&
                    txtUserPhoneNumber.Text.isValidPhoneNumber())
                {
                    try
                    { 
                        var newEmployee = new EmployeeVM()
                        {
                            FirstName = txtUserFirstName.Text,
                            LastName = txtUserLastName.Text,
                            Email = txtUserEmail.Text,
                            PhoneNumber = txtUserPhoneNumber.Text,
                            Active = (bool)chkActiveStatus.IsChecked,
                            Roles = _assignedRoles
                        };

                        if (!_addEmployee)//edit user
                        {
                            _employeeManager.EditEmployeeData(_employee, newEmployee);
                        }
                        else //add user
                        {
                            _employeeManager.AddEmployee(newEmployee);
                        }

                        this.DialogResult = true;
                }
                    catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("deactivated"))
                    {
                        chkActiveStatus.IsChecked = true;
                    }
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
                else
                {
                    MessageBox.Show("Please check your data.");
                }
            }
        }

        private void lstAssignedRoles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRole = lstAssignedRoles.SelectedItem;
            if (selectedRole == null)
            {
                return;
            }
            _assignedRoles.Remove((string)selectedRole);
            _unassignedRoles.Add((string)selectedRole);
            lstUnassignedRoles.ItemsSource = null;
            lstAssignedRoles.ItemsSource = null;
            lstAssignedRoles.ItemsSource = _assignedRoles;
            lstUnassignedRoles.ItemsSource = _unassignedRoles;
        }

        private void lstUnassignedRoles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRole = lstUnassignedRoles.SelectedItem;
            if (selectedRole == null)
            {
                return;
            }
            _unassignedRoles.Remove((string)selectedRole);
            _assignedRoles.Add((string)selectedRole);
            lstUnassignedRoles.ItemsSource = null;
            lstAssignedRoles.ItemsSource = null;
            lstAssignedRoles.ItemsSource = _assignedRoles;
            lstUnassignedRoles.ItemsSource = _unassignedRoles;
        }
    }
}
