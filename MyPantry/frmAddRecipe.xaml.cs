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
    /// Interaction logic for frmAddRecipe.xaml
    /// </summary>
    public partial class frmAddRecipe : Window
    {
        private IRecipeManager _recipeManager = new RecipeManager();

        public frmAddRecipe()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _recipeManager.AddRecipe(tbxRecipeName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check recipe name. " + ex.InnerException.Message);
            }
            this.DialogResult = true;
        }
    }
}
