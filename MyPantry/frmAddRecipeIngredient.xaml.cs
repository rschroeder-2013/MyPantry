using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    
    public partial class frmAddRecipeIngredient : Window
    {

        private Recipe _recipe;
        private IIngredientManager _ingredientManager = new IngredientManager();
        private IRecipeManager _recipeManager = new RecipeManager();

        private List<Ingredient> _allIngredients;



        public frmAddRecipeIngredient()
        {
            InitializeComponent();
        }

        public frmAddRecipeIngredient(Recipe recipe)
        {
            _recipe = recipe;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tblkAddRecipeIngredient.Text = "Add to " + _recipe.RecipeName;

            _allIngredients = _ingredientManager.RetrieveAllIngredients();

            dgAvailableIngredients.ItemsSource = _allIngredients;
            dgAvailableIngredients.Columns.Remove(dgAvailableIngredients.Columns[0]);
            dgAvailableIngredients.Columns.Remove(dgAvailableIngredients.Columns[1]);
            dgAvailableIngredients.Columns.Remove(dgAvailableIngredients.Columns[1]);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void dgAvailableIngredients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Ingredient)dgAvailableIngredients.SelectedItem;

            tbxIngredientName.Text = selectedItem.IngredientName;
            lblIngredientMeasurementType.Content = selectedItem.MeasurementType;
            tbxIngredientQuantity.Text = "";
        }

        private void btnAddRecipeIngredient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = (Ingredient)dgAvailableIngredients.SelectedItem;
                decimal result;

                if (selectedItem == null || tbxIngredientQuantity.Text == "" || Decimal.TryParse(tbxIngredientQuantity.Text, out result) == false)
                {
                    MessageBox.Show("Invalid values.");
                    return;
                }
                else
                {
                    decimal quantity = Convert.ToDecimal(tbxIngredientQuantity.Text);
                    _ingredientManager.AddIngredientIntoRecipe(_recipe.RecipeID, selectedItem.IngredientID, quantity);
                    
                }
                this.DialogResult = true;

            }
            catch (Exception ex)
            {
                this.DialogResult = true;
            }
        }
    }
}
