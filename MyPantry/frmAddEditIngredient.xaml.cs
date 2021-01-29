using DataTransferObjects;
using LogicLayer;
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
    /// Interaction logic for frmAddEditIngredient.xaml
    /// </summary>
    public partial class frmAddEditIngredient : Window
    {
        private Ingredient _ingredient;
        private bool _addIngredient = false;
        private IIngredientManager _ingredientManager = new IngredientManager();

        public frmAddEditIngredient()
        {
            _ingredient = new Ingredient();
            _addIngredient = true;

            InitializeComponent();
        }

        public frmAddEditIngredient(Ingredient ingredient)
        {
            _ingredient = ingredient;

            InitializeComponent();
        }

        private void setupEdit()
        {
            btnEditSave.Content = "Save";
            txtIngredientName.IsReadOnly = false;
            txtIngredientName.BorderBrush = Brushes.Black;

            txtIngredientType.IsReadOnly = false;
            txtIngredientType.BorderBrush = Brushes.Black;

            txtIngredientMeasurementType.IsReadOnly = false;
            txtIngredientMeasurementType.BorderBrush = Brushes.Black;

            tbxIngredientDescription.IsReadOnly = false;
            tbxIngredientDescription.BorderBrush = Brushes.Black;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnEditSave.Content == "Edit")
            {
                tblkTitle.Text = "Edit Ingredient";
                this.Title = "Edit Ingredient";
                setupEdit();
            }
            else // save operation
            {
                if (txtIngredientName.Text.isValidIngredientName() &&
                    txtIngredientType.Text.isValidIngredientType() &&
                    txtIngredientMeasurementType.Text.isValidIngredientMeasurement())
                {
                    try
                    {
                        var newIngredient = new Ingredient()
                        {
                            IngredientName = txtIngredientName.Text,
                            IngredientType = txtIngredientType.Text,
                            MeasurementType = txtIngredientMeasurementType.Text,
                            IngredientDescription = tbxIngredientDescription.Text
                        };

                        if (!_addIngredient)
                        {
                            _ingredientManager.EditIngredientData(_ingredient, newIngredient);
                        }
                        else
                        {
                            _ingredientManager.AddIngredient(newIngredient);
                        }

                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        new ApplicationException("Something went wrong." + ex.InnerException.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please check your data.");
                }
            }     
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addIngredient)
            {
                this.Title = "Add Ingredient";

                // display ingredient template
                tblkTitle.Text = "Add Ingredient";
                lblIngredientID.Content = "Assigned Automatically";
                txtIngredientName.Text = "";
                txtIngredientType.Text = "";
                txtIngredientMeasurementType.Text = "";
                tbxIngredientDescription.Text = "";

                setupEdit();
            }
            else
            { 
                try
                {
                    
                    lblIngredientID.Content = _ingredient.IngredientID;
                    txtIngredientName.Text = _ingredient.IngredientName;
                    txtIngredientType.Text = _ingredient.IngredientType;
                    txtIngredientMeasurementType.Text = _ingredient.MeasurementType;
                    tbxIngredientDescription.Text = _ingredient.IngredientDescription;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n"
                        + ex.InnerException.Message);
                }
            }
        }
    }
}

