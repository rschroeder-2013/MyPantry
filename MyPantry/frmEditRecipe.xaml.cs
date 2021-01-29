using LogicLayer;
using DataTransferObjects;
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

namespace MyPantry
{
    /// <summary>
    /// Interaction logic for frmAddEditRecipe.xaml
    /// </summary>
    public partial class frmAddEditRecipe : Window
    {

        private Recipe _recipe;
        private IIngredientManager _ingredientManager = new IngredientManager();

        public frmAddEditRecipe()
        {
            InitializeComponent();
        }

        public frmAddEditRecipe(Recipe recipe)
        {
            _recipe = recipe;

            InitializeComponent();
        }

        private void showRecipeIngredientList()
        {
            tblkRecipeName.Text = _recipe.RecipeName;
            lblRecipeID.Content = _recipe.RecipeID;

            //populate ingredient list
            var ingMgr = new IngredientManager();

            int selectedRecipe = Convert.ToInt32(lblRecipeID.Content);

            dgRecipeIngredients.ItemsSource = ingMgr.RetrieveIngredientsByRecipeID(selectedRecipe);

            dgRecipeIngredients.Columns.Remove(dgRecipeIngredients.Columns[0]);
            dgRecipeIngredients.Columns.Remove(dgRecipeIngredients.Columns[1]);
            dgRecipeIngredients.Columns.Remove(dgRecipeIngredients.Columns[2]);

            dgRecipeIngredients.Columns[0].Header = "Quantity";
            dgRecipeIngredients.Columns[1].Header = "Ingredient Name";
            dgRecipeIngredients.Columns[2].Header = "Ingredient Description";
            dgRecipeIngredients.Columns[3].Header = "Measurement Type";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            showRecipeIngredientList();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnAddRecipeIngredient_Click(object sender, RoutedEventArgs e)
        {
            var detailWindow = new frmAddRecipeIngredient(_recipe);
            detailWindow.ShowDialog();
        }

        private void btnDeleteRecipeIngredient_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (RecipeIngredient)dgRecipeIngredients.SelectedItem;
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
                _ingredientManager.DeleteIngredientFromRecipe(_recipe.RecipeID, selectedItem.IngredientID, selectedItem.Quantity);
                showRecipeIngredientList();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            showRecipeIngredientList();
        }
    }
}
