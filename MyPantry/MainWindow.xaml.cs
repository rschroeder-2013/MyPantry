using DataTransferObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace MyPantry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IIngredientManager _ingredientManager = new IngredientManager();
        private IUserManager _userManager = new UserManager();
        private IRecipeManager _recipeManager = new RecipeManager();
        private User _user = null;

        private List<Recipe> _sundayList = new List<Recipe>();
        private List<Recipe> _mondayList = new List<Recipe>();
        private List<Recipe> _tuesdayList = new List<Recipe>();
        private List<Recipe> _wednesdayList = new List<Recipe>();
        private List<Recipe> _thursdayList = new List<Recipe>();
        private List<Recipe> _fridayList = new List<Recipe>();
        private List<Recipe> _saturdayList = new List<Recipe>();
        private List<Recipe> _combinedWeek = new List<Recipe>();
        private List<RecipeIngredient> _tempGroceryList = new List<RecipeIngredient>();
        private List<RecipeIngredient> _myGroceryList = new List<RecipeIngredient>();

        private bool _sundaySelected;
        private bool _mondaySelected;
        private bool _tuesdaySelected;
        private bool _wednesdaySelected;
        private bool _thursdaySelected;
        private bool _fridaySelected;
        private bool _saturdaySelected;

        public MainWindow()
        {
            InitializeComponent();
        }

        //UI updater methods
        private void resetWindow()
        {
            mnuMain.IsEnabled = false;
            btnLogin.IsDefault = true;
            btnAdminMenu.Visibility = Visibility.Hidden;
            btnMyGroceries.Visibility = Visibility.Hidden;
            btnMyIngredients.Visibility = Visibility.Hidden;
            btnMyPlan.Visibility = Visibility.Hidden;
            btnMyProfile.Visibility = Visibility.Hidden;
            btnMyRecipes.Visibility = Visibility.Hidden;
            pwdPassword.Password = "";
            txtUsername.Text = "";
            btnLogin.Content = "Login";
            txtMyProfileUserRole.Text = "Role Not Assigned";
            lblLogInMessage.Content = "Please Log In To Continue!";
            txtUsername.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            sbarItemMessage.Content = "Not Logged In";
            txtUsername.Focus();
            hideMyProfile();
            hideAdminMenu();
            hideMyIngredients();
            hideMyRecipes();
            hideMyPlan();
            hideMyGroceries();
        }
        private void showMyProfile()
        {
            //change colors and visibility
            btnMyProfile.Background = Brushes.Orange;
            txtMyProfileUserFirstName.Visibility = Visibility.Visible;
            txtMyProfileUserLastName.Visibility = Visibility.Visible;
            txtMyProfileUserEmail.Visibility = Visibility.Visible;
            txtMyProfileUserPhoneNumber.Visibility = Visibility.Visible;
            txtMyProfileUserRole.Visibility = Visibility.Visible;
            txtMyProfileIngredientCount.Visibility = Visibility.Visible;
            txtMyProfileRecipeCount.Visibility = Visibility.Visible;

            //hide all other menus
            hideAdminMenu();
            hideMyIngredients();
            hideMyRecipes();
            hideMyGroceries();
            hideMyPlan();
        }
        private void hideMyProfile()
        {
            btnMyProfile.Background = Brushes.LightBlue;
            txtMyProfileUserFirstName.Visibility = Visibility.Hidden;
            txtMyProfileUserLastName.Visibility = Visibility.Hidden;
            txtMyProfileUserEmail.Visibility = Visibility.Hidden;
            txtMyProfileUserPhoneNumber.Visibility = Visibility.Hidden;
            txtMyProfileUserRole.Visibility = Visibility.Hidden;
            txtMyProfileIngredientCount.Visibility = Visibility.Hidden;
            txtMyProfileRecipeCount.Visibility = Visibility.Hidden;
        }
        private void showMyIngredients()
        {
            btnMyIngredients.Background = Brushes.Orange;
            dgIngredientList.Visibility = Visibility.Visible;
            btnAddIngredient.Visibility = Visibility.Visible;
            btnEditIngredient.Visibility = Visibility.Visible;
            btnDeleteIngredient.Visibility = Visibility.Visible;

            hideAdminMenu();
            hideMyProfile();
            hideMyRecipes();
            hideMyPlan();
            hideMyGroceries();
        }
        private void hideMyIngredients()
        {
            btnMyIngredients.Background = Brushes.LightBlue;
            dgIngredientList.Visibility = Visibility.Hidden;
            btnAddIngredient.Visibility = Visibility.Hidden;
            btnEditIngredient.Visibility = Visibility.Hidden;
            btnDeleteIngredient.Visibility = Visibility.Hidden;
        }
        private void showAdminMenu()
        {
            //change visibility
            btnAdminMenu.Background = Brushes.Orange;
            dgUserList.Visibility = Visibility.Visible;
            chkShowActive.Visibility = Visibility.Visible;
            btnAddUser.Visibility = Visibility.Visible;
            btnEditUser.Visibility = Visibility.Visible;
            try
            {
                if (dgUserList.ItemsSource == null)
                {
                    showUserList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }

            //hide all other menus
            hideMyProfile();
            hideMyIngredients();
            hideMyRecipes();
            hideMyPlan();
            hideMyGroceries();
        }
        private void hideAdminMenu()
        {
            btnAdminMenu.Background = Brushes.LightBlue;
            dgUserList.Visibility = Visibility.Hidden;
            chkShowActive.Visibility = Visibility.Hidden;
            btnAddUser.Visibility = Visibility.Hidden;
            btnEditUser.Visibility = Visibility.Hidden;
            try
            {
                if (dgUserList.ItemsSource == null)
                {
                    showUserList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }
        private void showMyRecipes()
        {
            btnMyRecipes.Background = Brushes.Orange;
            dgRecipeList.Visibility = Visibility.Visible;
            btnAddRecipe.Visibility = Visibility.Visible;
            btnEditRecipe.Visibility = Visibility.Visible;
            btnDeleteRecipe.Visibility = Visibility.Visible;
            hideAdminMenu();
            hideMyIngredients();
            hideMyProfile();
            hideMyPlan();
            hideMyGroceries();
            showRecipeList();
        }
        private void hideMyRecipes()
        {
            btnMyRecipes.Background = Brushes.LightBlue;
            dgRecipeList.Visibility = Visibility.Hidden;
            btnAddRecipe.Visibility = Visibility.Hidden;
            btnEditRecipe.Visibility = Visibility.Hidden;
            btnDeleteRecipe.Visibility = Visibility.Hidden;
        }
        private void showMyPlan()
        {
            hideAdminMenu();
            hideMyIngredients();
            hideMyProfile();
            hideMyRecipes();
            hideMyGroceries();

            btnMyPlan.Background = Brushes.Orange;
            btnSelectSundayList.Visibility = Visibility.Visible;
            btnSelectMondayList.Visibility = Visibility.Visible;
            btnSelectTuesdayList.Visibility = Visibility.Visible;
            btnSelectWednesdayList.Visibility = Visibility.Visible;
            btnSelectThursdayList.Visibility = Visibility.Visible;
            btnSelectFridayList.Visibility = Visibility.Visible;
            btnSelectSaturdayList.Visibility = Visibility.Visible;
            btnAddToSelectedDay.Visibility = Visibility.Visible;
            btnClearSelectedDay.Visibility = Visibility.Visible;
            btnRemoveFromSelectedDay.Visibility = Visibility.Visible;
            lstSundayPlan.Visibility = Visibility.Visible;
            lstMondayPlan.Visibility = Visibility.Visible;
            lstTuesdayPlan.Visibility = Visibility.Visible;
            lstWednesdayPlan.Visibility = Visibility.Visible;
            lstThursdayPlan.Visibility = Visibility.Visible;
            lstFridayPlan.Visibility = Visibility.Visible;
            lstSaturdayPlan.Visibility = Visibility.Visible;
            dgMyPlanRecipeList.Visibility = Visibility.Visible;
        }
        private void hideMyPlan()
        {
            btnMyPlan.Background = Brushes.LightBlue;
            btnSelectSundayList.Visibility = Visibility.Hidden;
            btnSelectMondayList.Visibility = Visibility.Hidden;
            btnSelectTuesdayList.Visibility = Visibility.Hidden;
            btnSelectWednesdayList.Visibility = Visibility.Hidden;
            btnSelectThursdayList.Visibility = Visibility.Hidden;
            btnSelectFridayList.Visibility = Visibility.Hidden;
            btnSelectSaturdayList.Visibility = Visibility.Hidden;
            btnAddToSelectedDay.Visibility = Visibility.Hidden;
            btnClearSelectedDay.Visibility = Visibility.Hidden;
            btnRemoveFromSelectedDay.Visibility = Visibility.Hidden;
            dgMyPlanRecipeList.Visibility = Visibility.Hidden;
            lstSundayPlan.Visibility = Visibility.Hidden;
            lstMondayPlan.Visibility = Visibility.Hidden;
            lstTuesdayPlan.Visibility = Visibility.Hidden;
            lstWednesdayPlan.Visibility = Visibility.Hidden;
            lstThursdayPlan.Visibility = Visibility.Hidden;
            lstFridayPlan.Visibility = Visibility.Hidden;
            lstSaturdayPlan.Visibility = Visibility.Hidden;
        }

        private void showMyGroceries()
        {
            btnMyGroceries.Background = Brushes.Orange;
            hideAdminMenu();
            hideMyIngredients();
            hideMyProfile();
            hideMyRecipes();
            hideMyPlan();
            btnExportGroceries.Visibility = Visibility.Visible;
            dgMyGroceriesList.Visibility = Visibility.Visible;
        }
        private void hideMyGroceries()
        {
            btnMyGroceries.Background = Brushes.LightBlue;
            btnExportGroceries.Visibility = Visibility.Hidden;
            dgMyGroceriesList.Visibility = Visibility.Hidden;
        }
        //End UI updater methods

        //login click event
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnLogin.Content == "Login")
            {

                try
                {
                    _user = _userManager.AuthenticateUser(txtUsername.Text, pwdPassword.Password);

                    
                    if (pwdPassword.Password == "password")
                    {
                        var updatePassword = new frmUpdateUserPassword(_userManager,
                            _user, true);
                        if (!updatePassword.ShowDialog() == true)
                        {
                            _user = null;
                            resetWindow();
                            MessageBox.Show("You must change your password" +
                                "\n on first login.",
                                "Password Change Required", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                            return;
                        }
                    }

                    //display sidebar buttons
                    btnMyGroceries.Visibility = Visibility.Visible;
                    btnMyIngredients.Visibility = Visibility.Visible;
                    btnMyPlan.Visibility = Visibility.Visible;
                    btnMyProfile.Visibility = Visibility.Visible;
                    btnMyRecipes.Visibility = Visibility.Visible;

                    //display user info btnMyProfile
                    txtMyProfileUserFirstName.Text = _user.FirstName;
                    txtMyProfileUserLastName.Text = _user.LastName;
                    txtMyProfileUserPhoneNumber.Text = _user.PhoneNumber;
                    txtMyProfileUserEmail.Text = _user.Email;
                    

                    //Display user access level in sbar & myprofile
                    foreach (var getRole in _user.Roles)
                    {
                        switch (getRole)
                        {
                            case "Admin":
                                btnAdminMenu.Visibility = Visibility.Visible;
                                sbarItemMessage.Content = "Logged in at: " + DateTime.Now.ToString();
                                txtMyProfileUserRole.Text = getRole.ToString();
                                break;
                            case "Standard User":
                                btnAdminMenu.Visibility = Visibility.Hidden;
                                sbarItemMessage.Content = "Logged in at: " + DateTime.Now.ToString();
                                txtMyProfileUserRole.Text = getRole.ToString();
                                break;
                            case "Premium User":
                                btnAdminMenu.Visibility = Visibility.Hidden;
                                sbarItemMessage.Content = "Logged in at: " + DateTime.Now.ToString();
                                txtMyProfileUserRole.Text = getRole.ToString();
                                break;
                            default:
                                break;
                        }
                    }

                    //update login UI and greet user
                    mnuMain.IsEnabled = true;
                    btnLogin.Content = "Logout";
                    pwdPassword.Password = "";
                    txtUsername.Text = "";
                    txtUsername.Visibility = Visibility.Hidden;
                    pwdPassword.Visibility = Visibility.Hidden;
                    btnLogin.IsDefault = false;
                    lblLogInMessage.Content = "Welcome back, " + _user.FirstName + " " + _user.LastName + "!";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            else // logout
            {
                _user = null;

                resetWindow();
            }

        }
        //End login click event
        private void btnAdminMenu_Click(object sender, RoutedEventArgs e)
        {
            showAdminMenu();
            hideMyProfile();
            hideMyIngredients();
            hideMyPlan();
            hideMyRecipes();
        }
        private void mnuItemUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            var updateForm = new frmUpdateUserPassword(_userManager, _user);
            updateForm.ShowDialog();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            resetWindow();
        }
        private void mnuItemLogOut_Click(object sender, RoutedEventArgs e)
        {
            _user = null;

            resetWindow();
        }
        private void dgUserList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            editUserData();
        }
        private void editUserData()
        {
            var selectedItem = (EmployeeVM)dgUserList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Must select user to edit.");
                return;
            }

            var detailWindow = new frmAddEditUserInfo(selectedItem);
            if (detailWindow.ShowDialog() == true)
            {
                showUserList();
            }
        }
        private void showUserList()
        {
            var empMgr = new EmployeeManager();
            dgUserList.ItemsSource = empMgr.RetrieveEmployeesByActive((bool)chkShowActive.IsChecked);

            dgUserList.Columns.Remove(dgUserList.Columns[0]);

            dgUserList.Columns[0].Header = "User ID";
            dgUserList.Columns[1].Header = "Email Address";
            dgUserList.Columns[2].Header = "First Name";
            dgUserList.Columns[3].Header = "Last Name";
            dgUserList.Columns[4].Header = "Phone Number";
        }
        private void chkShowActive_Click(object sender, RoutedEventArgs e)
        {
            showUserList();
        }
        private void btnMyProfile_Click(object sender, RoutedEventArgs e)
        {
            showMyProfile();
            hideAdminMenu();
            hideMyIngredients();
            hideMyRecipes();
            hideMyPlan();
        }
        private void btnMyIngredients_Click(object sender, RoutedEventArgs e)
        {
            showIngredientList();
            showMyIngredients();
            hideMyProfile();
            hideAdminMenu();
            hideMyRecipes();
            hideMyPlan();
        }
        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            editUserData();
        }
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            var detailWindow = new frmAddEditUserInfo();
            if (detailWindow.ShowDialog() == true)
            {
                showUserList();
            }
        }
        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            var detailWindow = new frmAddEditIngredient();
            if (detailWindow.ShowDialog() == true)
            {
                showIngredientList();
            }
        }
        private void btnEditIngredient_Click(object sender, RoutedEventArgs e)
        {
            editSelectedIngredient();
        }
        private void btnDeleteIngredient_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Ingredient)dgIngredientList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("You need to select an ingredient to delete.",
                    "Deletion Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            else
            {
                _ingredientManager.DeleteIngredient(selectedItem);
                showIngredientList();
            }
        }
        private void editSelectedIngredient()
        {
            var selectedItem = (Ingredient)dgIngredientList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("You need to select an ingredient to edit.", 
                    "Edit Operation Not Available", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
                return;
            }

            var detailWindow = new frmAddEditIngredient(selectedItem);
            if (detailWindow.ShowDialog() == true)
            {
                showIngredientList();
            }
        }
        private void showRecipeList()
        {
            //populate recipe list

            var recipeMgr = new RecipeManager();

            dgRecipeList.ItemsSource = recipeMgr.RetrieveAllRecipes();
        }
        private void showIngredientList()
        {
            //populate ingredient list

            var ingMgr = new IngredientManager();

            dgIngredientList.ItemsSource = ingMgr.RetrieveAllIngredients();

            dgIngredientList.Columns[0].Header = "Ingredient ID";
            dgIngredientList.Columns[1].Header = "Name";
            dgIngredientList.Columns[2].Header = "Type";
            dgIngredientList.Columns[3].Header = "Description";
            dgIngredientList.Columns[4].Header = "Measurement";
        }
        private void dgIngredientList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            editSelectedIngredient();
        }
        private void btnMyRecipes_Click(object sender, RoutedEventArgs e)
        {
            showMyRecipes();
        }
        private void dgRecipeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Recipe)dgRecipeList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Must select recipe.");
                return;
            }

            var detailWindow = new frmAddEditRecipe(selectedItem);
            detailWindow.ShowDialog();
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            var detailWindow = new frmAddRecipe();
            if (detailWindow.ShowDialog() == true)
            {
                showRecipeList();
            }
        }

        private void btnEditRecipe_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Recipe)dgRecipeList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Must select recipe.");
                return;
            }

            var detailWindow = new frmAddEditRecipe(selectedItem);
            detailWindow.ShowDialog();
        }

        private void btnDeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Recipe)dgRecipeList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Must select recipe.");
                return;
            }

            _recipeManager.DeleteRecipe(selectedItem.RecipeID);
            showRecipeList();
        }

        private void btnMyPlan_Click(object sender, RoutedEventArgs e)
        {
            dgMyPlanRecipeList.ItemsSource = _recipeManager.RetrieveAllRecipes();
            lstSundayPlan.ItemsSource = _sundayList;
            lstMondayPlan.ItemsSource = _mondayList;
            lstTuesdayPlan.ItemsSource = _tuesdayList;
            lstWednesdayPlan.ItemsSource = _wednesdayList;
            lstThursdayPlan.ItemsSource = _thursdayList;
            lstFridayPlan.ItemsSource = _fridayList;
            lstSaturdayPlan.ItemsSource = _saturdayList;
            showMyPlan();
            hideMyRecipes();
            hideMyProfile();
            hideAdminMenu();
            hideMyIngredients();
        }

        //list highlight helper methods
        private void unselectSundayList()
        {
            btnSelectSundayList.Background = Brushes.LightBlue;
            lstSundayPlan.BorderThickness = new Thickness(1);
            lstSundayPlan.BorderBrush = Brushes.Gray;
            _sundaySelected = false;
        }
        private void unselectMondayList()
        {
            btnSelectMondayList.Background = Brushes.LightBlue;
            lstMondayPlan.BorderThickness = new Thickness(1);
            lstMondayPlan.BorderBrush = Brushes.Gray;
            _mondaySelected = false;
        }
        private void unselectTuesdayList()
        {
            btnSelectTuesdayList.Background = Brushes.LightBlue;
            lstTuesdayPlan.BorderThickness = new Thickness(1);
            lstTuesdayPlan.BorderBrush = Brushes.Gray;
            _tuesdaySelected = false;
        }
        private void unselectWednesdayList()
        {
            btnSelectWednesdayList.Background = Brushes.LightBlue;
            lstWednesdayPlan.BorderThickness = new Thickness(1);
            lstWednesdayPlan.BorderBrush = Brushes.Gray;
            _wednesdaySelected = false;
        }
        private void unselectThursdayList()
        {
            btnSelectThursdayList.Background = Brushes.LightBlue;
            lstThursdayPlan.BorderThickness = new Thickness(1);
            lstThursdayPlan.BorderBrush = Brushes.Gray;
            _thursdaySelected = false;
        }
        private void unselectFridayList()
        {
            btnSelectFridayList.Background = Brushes.LightBlue;
            lstFridayPlan.BorderThickness = new Thickness(1);
            lstFridayPlan.BorderBrush = Brushes.Gray;
            _fridaySelected = false;
        }
        private void unselectSaturdayList()
        {
            btnSelectSaturdayList.Background = Brushes.LightBlue;
            lstSaturdayPlan.BorderThickness = new Thickness(1);
            lstSaturdayPlan.BorderBrush = Brushes.Gray;
            _saturdaySelected = false;
        }
        //end list highlight helper methods

        private void btnSelectSundayList_Click(object sender, RoutedEventArgs e)
        {
            btnSelectSundayList.Background = Brushes.Orange;
            lstSundayPlan.BorderThickness = new Thickness(3);
            lstSundayPlan.BorderBrush = Brushes.Orange;
            _sundaySelected = true;
            unselectMondayList();
            unselectTuesdayList();
            unselectWednesdayList();
            unselectThursdayList();
            unselectFridayList();
            unselectSaturdayList();

        }

        private void btnSelectMondayList_Click(object sender, RoutedEventArgs e)
        {
            btnSelectMondayList.Background = Brushes.Orange;
            lstMondayPlan.BorderThickness = new Thickness(3);
            lstMondayPlan.BorderBrush = Brushes.Orange;
            _mondaySelected = true;
            unselectSundayList();
            unselectTuesdayList();
            unselectWednesdayList();
            unselectThursdayList();
            unselectFridayList();
            unselectSaturdayList();

        }

        private void btnSelectTuesdayList_Click(object sender, RoutedEventArgs e)
        {
            btnSelectTuesdayList.Background = Brushes.Orange;
            lstTuesdayPlan.BorderThickness = new Thickness(3);
            lstTuesdayPlan.BorderBrush = Brushes.Orange;
            _tuesdaySelected = true;
            unselectSundayList();
            unselectMondayList();
            unselectWednesdayList();
            unselectThursdayList();
            unselectFridayList();
            unselectSaturdayList();
        }

        private void btnSelectWednesdayList_Click(object sender, RoutedEventArgs e)
        {
            btnSelectWednesdayList.Background = Brushes.Orange;
            lstWednesdayPlan.BorderThickness = new Thickness(3);
            lstWednesdayPlan.BorderBrush = Brushes.Orange;
            _wednesdaySelected = true;
            unselectSundayList();
            unselectMondayList();
            unselectTuesdayList();
            unselectThursdayList();
            unselectFridayList();
            unselectSaturdayList();

        }

        private void btnSelectThursdayList_Click(object sender, RoutedEventArgs e)
        {
            btnSelectThursdayList.Background = Brushes.Orange;
            lstThursdayPlan.BorderThickness = new Thickness(3);
            lstThursdayPlan.BorderBrush = Brushes.Orange;
            _thursdaySelected = true;
            unselectSundayList();
            unselectMondayList();
            unselectTuesdayList();
            unselectWednesdayList();
            unselectFridayList();
            unselectSaturdayList();

        }

        private void btnSelectFridayList_Click(object sender, RoutedEventArgs e)
        {
            btnSelectFridayList.Background = Brushes.Orange;
            lstFridayPlan.BorderThickness = new Thickness(3);
            lstFridayPlan.BorderBrush = Brushes.Orange;
            _fridaySelected = true;
            unselectSundayList();
            unselectMondayList();
            unselectTuesdayList();
            unselectWednesdayList();
            unselectThursdayList();
            unselectSaturdayList();

        }

        private void btnSelectSaturdayList_Click(object sender, RoutedEventArgs e)
        {
            btnSelectSaturdayList.Background = Brushes.Orange;
            lstSaturdayPlan.BorderThickness = new Thickness(3);
            lstSaturdayPlan.BorderBrush = Brushes.Orange;
            _saturdaySelected = true;
            unselectSundayList();
            unselectMondayList();
            unselectTuesdayList();
            unselectWednesdayList();
            unselectThursdayList();
            unselectFridayList();

        }

        private void addSelectedRecipeToDay()
        {
            var selectedItem = (Recipe)dgMyPlanRecipeList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Must select a recipe.");
                return;
            } else if (_sundaySelected == false &&
                _mondaySelected == false &&
                _tuesdaySelected == false &&
                _wednesdaySelected == false &&
                _thursdaySelected == false &&
                _fridaySelected == false &&
                _saturdaySelected == false)
            {
                MessageBox.Show("Must select a day.");
                return;
            }
            try
            {
                if (_sundaySelected == true)
                {
                    _sundayList.Add(selectedItem);
                    lstSundayPlan.Items.Refresh();
                }
                else if (_mondaySelected == true)
                {
                    _mondayList.Add(selectedItem);
                    lstMondayPlan.Items.Refresh();
                }
                else if (_tuesdaySelected == true)
                {
                    _tuesdayList.Add(selectedItem);
                    lstTuesdayPlan.Items.Refresh();
                }
                else if (_wednesdaySelected == true)
                {
                    _wednesdayList.Add(selectedItem);
                    lstWednesdayPlan.Items.Refresh();
                }
                else if (_thursdaySelected == true)
                {
                    _thursdayList.Add(selectedItem);
                    lstThursdayPlan.Items.Refresh();
                }
                else if (_fridaySelected == true)
                {
                    _fridayList.Add(selectedItem);
                    lstFridayPlan.Items.Refresh();
                }
                else if(_saturdaySelected == true)
                {
                    _saturdayList.Add(selectedItem);
                    lstSaturdayPlan.Items.Refresh();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private void btnAddToSelectedDay_Click(object sender, RoutedEventArgs e)
        {
            addSelectedRecipeToDay();
        }

        private void btnClearSelectedDay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_sundaySelected == true)
                {
                    _sundayList.Clear();
                    lstSundayPlan.Items.Refresh();
                }
                else if (_mondaySelected == true)
                {
                    _mondayList.Clear();
                    lstMondayPlan.Items.Refresh();
                }
                else if (_tuesdaySelected == true)
                {
                    _tuesdayList.Clear();
                    lstTuesdayPlan.Items.Refresh();
                }
                else if (_wednesdaySelected == true)
                {
                    _wednesdayList.Clear();
                    lstWednesdayPlan.Items.Refresh();
                }
                else if (_thursdaySelected == true)
                {
                    _thursdayList.Clear();
                    lstThursdayPlan.Items.Refresh();
                }
                else if (_fridaySelected == true)
                {
                    _fridayList.Clear();
                    lstFridayPlan.Items.Refresh();
                }
                else if (_saturdaySelected == true)
                {
                    _saturdayList.Clear();
                    lstSaturdayPlan.Items.Refresh();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private void removeFromSelectedDayList(List<Recipe> dayList, ListBox item)
        {
            var chosenItem = item.SelectedItem;

            dayList.Remove((Recipe)chosenItem);
        }

        private void btnRemoveFromSelectedDay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_sundaySelected == true)
                {
                    removeFromSelectedDayList(_sundayList, lstSundayPlan);
                    lstSundayPlan.Items.Refresh();
                }
                else if (_mondaySelected == true)
                {
                    removeFromSelectedDayList(_mondayList, lstMondayPlan);
                    lstMondayPlan.Items.Refresh();
                }
                else if (_tuesdaySelected == true)
                {
                    removeFromSelectedDayList(_tuesdayList, lstTuesdayPlan);
                    lstTuesdayPlan.Items.Refresh();
                }
                else if (_wednesdaySelected == true)
                {
                    removeFromSelectedDayList(_wednesdayList, lstWednesdayPlan);
                    lstWednesdayPlan.Items.Refresh();
                }
                else if (_thursdaySelected == true)
                {
                    removeFromSelectedDayList(_thursdayList, lstThursdayPlan);
                    lstThursdayPlan.Items.Refresh();
                }
                else if (_fridaySelected == true)
                {
                    removeFromSelectedDayList(_fridayList, lstFridayPlan);
                    lstFridayPlan.Items.Refresh();
                }
                else if (_saturdaySelected == true)
                {
                    removeFromSelectedDayList(_saturdayList, lstSaturdayPlan);
                    lstSaturdayPlan.Items.Refresh();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private List<RecipeIngredient> getIngredientsFromPlanRecipes(List<Recipe> day1, 
                                                                    List<Recipe> day2, 
                                                                    List<Recipe> day3, 
                                                                    List<Recipe> day4, 
                                                                    List<Recipe> day5,
                                                                    List<Recipe> day6, 
                                                                    List<Recipe> day7)
        {
            List<RecipeIngredient> myGroceryIngredients = new List<RecipeIngredient>();

            foreach(var r in day1)
            {
                var temp = _ingredientManager.RetrieveIngredientsByRecipeID(r.RecipeID);
                foreach(var i in temp)
                {
                    myGroceryIngredients.Add(i);
                }
            }
            foreach (var r in day2)
            {
                var temp = _ingredientManager.RetrieveIngredientsByRecipeID(r.RecipeID);
                foreach (var i in temp)
                {
                    myGroceryIngredients.Add(i);
                }
            }
            foreach (var r in day3)
            {
                var temp = _ingredientManager.RetrieveIngredientsByRecipeID(r.RecipeID);
                foreach (var i in temp)
                {
                    myGroceryIngredients.Add(i);
                }
            }
            foreach (var r in day4)
            {
                var temp = _ingredientManager.RetrieveIngredientsByRecipeID(r.RecipeID);
                foreach (var i in temp)
                {
                    myGroceryIngredients.Add(i);
                }
            }
            foreach (var r in day5)
            {
                var temp = _ingredientManager.RetrieveIngredientsByRecipeID(r.RecipeID);
                foreach (var i in temp)
                {
                    myGroceryIngredients.Add(i);
                }
            }
            foreach (var r in day6)
            {
                var temp = _ingredientManager.RetrieveIngredientsByRecipeID(r.RecipeID);
                foreach (var i in temp)
                {
                    myGroceryIngredients.Add(i);
                }
            }
            foreach (var r in day7)
            {
                var temp = _ingredientManager.RetrieveIngredientsByRecipeID(r.RecipeID);
                foreach (var i in temp)
                {
                    myGroceryIngredients.Add(i);
                }
            }

            return myGroceryIngredients;
        }

        private List<RecipeIngredient> combineAmountsAndRemoveDuplicates(List<RecipeIngredient> list)
        {
            //finds duplicates and counts how many, returns duplicated ID and reccurance count
            var q = from x in list
                    group x by x.IngredientID into g
                    let count = g.Count()
                    select new { Value = g.Key, Count = count };

            //copies original list and groups by IngredientID, removing duplicates
            List<RecipeIngredient> noDupes = list.GroupBy(x => x.IngredientID).Select(g => g.First()).ToList();

            
            foreach(var i in noDupes)
            {
                foreach(var x in q)
                {
                    if (i.IngredientID == x.Value)
                    {
                        i.Quantity = i.Quantity * x.Count;
                    }
                }
            }

            return noDupes;
        }

        private void btnMyGroceries_Click(object sender, RoutedEventArgs e)
        {
            showMyGroceries();

            _tempGroceryList = getIngredientsFromPlanRecipes(_sundayList,
                                                            _mondayList,
                                                            _tuesdayList,
                                                            _wednesdayList,
                                                            _thursdayList,
                                                            _fridayList,
                                                            _saturdayList);


            _myGroceryList = combineAmountsAndRemoveDuplicates(_tempGroceryList);
            dgMyGroceriesList.ItemsSource = _myGroceryList;

            dgMyGroceriesList.Columns.Remove(dgMyGroceriesList.Columns[0]);
            dgMyGroceriesList.Columns.Remove(dgMyGroceriesList.Columns[1]);
            dgMyGroceriesList.Columns.Remove(dgMyGroceriesList.Columns[2]);
        }

        private void btnExportGroceries_Click(object sender, RoutedEventArgs e)
        {
            if (_myGroceryList.Count == 0)
            {
                MessageBox.Show("No plan created!");
                return;
            }
            else
            {
                string dateTime = DateTime.Now.ToShortDateString();
                string path = "H:\\Kirkwood\\5_2020FL\\.NET Development II\\Final\\Ver 2\\MyPantry\\MyGroceries.txt";
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("MyPantry Grocery List: " + dateTime);
                    sw.WriteLine("-------------------------------------------------------------");
                    sw.WriteLine("MyPantry Plan\n");
                    sw.Write("Sunday: ");
                    foreach (var r in _sundayList)
                    {
                        sw.Write(r.RecipeName + ", ");
                    }
                    sw.Write("\n");
                    sw.Write("Monday: ");
                    foreach (var r in _mondayList)
                    {
                        sw.Write(r.RecipeName + ", ");
                    }
                    sw.Write("\n");
                    sw.Write("Tuesday: ");
                    foreach (var r in _tuesdayList)
                    {
                        sw.Write(r.RecipeName + ", ");
                    }
                    sw.Write("\n");
                    sw.Write("Wednesday: ");
                    foreach (var r in _wednesdayList)
                    {
                        sw.Write(r.RecipeName + ", ");
                    }
                    sw.Write("\n");
                    sw.Write("Thursday: ");
                    foreach (var r in _thursdayList)
                    {
                        sw.Write(r.RecipeName + ", ");
                    }
                    sw.Write("\n");
                    sw.Write("Friday: ");
                    foreach (var r in _fridayList)
                    {
                        sw.Write(r.RecipeName + ", ");
                    }
                    sw.Write("\n");
                    sw.Write("Saturday: ");
                    foreach (var r in _saturdayList)
                    {
                        sw.Write(r.RecipeName + ", ");
                    }
                    sw.Write("\n");
                    sw.Write("\n");
                    sw.WriteLine("-------------------------------------------------------------\n");
                    
                    foreach (var line in _myGroceryList)
                    {
                        RecipeIngredient ri = (RecipeIngredient)line; // unbox once
                        sw.WriteLine("[ ] " + ri.IngredientName + ":");
                        sw.Write("\tQuantity: " + ri.Quantity + " ");
                        sw.WriteLine(ri.MeasurementType + " ");
                        sw.WriteLine("\tDescription: " + ri.IngredientDescription + " ");
                        sw.WriteLine(" ");
                    }
                    sw.WriteLine("-------------------------------------------------------------");
                    sw.WriteLine("***End of File***");
                    MessageBox.Show("Groceries exported to: " + path);
                }
            }

        }
    }
}
