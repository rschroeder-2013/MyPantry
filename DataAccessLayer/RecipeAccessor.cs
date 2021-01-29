using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace DataAccessLayer
{
    public class RecipeAccessor : IRecipeAccessor
    {
        public int DeleteRecipeAndIngredients(int recipeID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_delete_recipe_and_ingredients_from_recipe_by_recipe_id", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RecipeID", SqlDbType.Int);
            cmd.Parameters["@RecipeID"].Value = recipeID;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new ApplicationException("Recipe could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int InsertNewRecipeName(string recipeName)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_insert_new_recipe_name", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RecipeName", SqlDbType.NVarChar, 255);
            cmd.Parameters["@RecipeName"].Value = recipeName;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new ApplicationException("Recipe could not be added.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;

        }

        public List<Recipe> SelectAllRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();


            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_all_recipes", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var recipe = new Recipe()
                        {
                            RecipeID = reader.GetInt32(0),
                            RecipeName = reader.GetString(1)
                        };
                        recipes.Add(recipe);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return recipes;
        }
    }
}
