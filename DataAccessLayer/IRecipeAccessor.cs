using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace DataAccessLayer
{
    public interface IRecipeAccessor
    {
        List<Recipe> SelectAllRecipes();

        int InsertNewRecipeName(string recipeName);

        int DeleteRecipeAndIngredients(int recipeID);

    }
}
