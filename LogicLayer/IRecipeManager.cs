using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace LogicLayer
{
    public interface IRecipeManager
    {
        List<Recipe> RetrieveAllRecipes();

        bool AddRecipe(string recipeName);

        bool DeleteRecipe(int recipeID);
    }
}
