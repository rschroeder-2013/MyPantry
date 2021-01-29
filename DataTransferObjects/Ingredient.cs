using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string IngredientName { get; set; }
        public string IngredientType { get; set; }
        public string IngredientDescription { get; set; }
        public string MeasurementType { get; set; }
        
    }

    public class RecipeIngredient : Ingredient
    {
        public int RecipeID { get; set; }
        public decimal Quantity { get; set; }
    }
}
