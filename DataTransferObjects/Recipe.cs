using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
    }

    public class RecipeVM : Recipe
    {
        public decimal Quantity { get; set; }
        public int IngredientID { get; set; }
    }

}
