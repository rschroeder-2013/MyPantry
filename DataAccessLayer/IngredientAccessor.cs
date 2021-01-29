using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class IngredientAccessor : IIngredientAccessor
    {
        public int DeleteIngredient(Ingredient ingredient)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_delete_ingredient", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IngredientName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@IngredientType", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@IngredientDescription", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@MeasurementType", SqlDbType.NVarChar, 255);

            cmd.Parameters["@IngredientName"].Value = ingredient.IngredientName;
            cmd.Parameters["@IngredientType"].Value = ingredient.IngredientType;
            cmd.Parameters["@IngredientDescription"].Value = ingredient.IngredientDescription;
            cmd.Parameters["@MeasurementType"].Value = ingredient.MeasurementType;

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
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

        public int DeleteIngredientFromRecipe(int recipeID, int ingredientID, decimal quantity)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_delete_ingredient_from_recipe", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RecipeID", SqlDbType.Int);
            cmd.Parameters.Add("@IngredientID", SqlDbType.Int);
            cmd.Parameters.Add("@Quantity", SqlDbType.Decimal);

            cmd.Parameters["@RecipeID"].Value = recipeID;
            cmd.Parameters["@IngredientID"].Value = ingredientID;
            cmd.Parameters["@Quantity"].Value = quantity;

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
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

        public int InsertIngredient(Ingredient ingredient)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_new_ingredient", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IngredientName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@IngredientType", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@IngredientDescription", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@MeasurementType", SqlDbType.NVarChar, 255);

            cmd.Parameters["@IngredientName"].Value = ingredient.IngredientName;
            cmd.Parameters["@IngredientType"].Value = ingredient.IngredientType;
            cmd.Parameters["@IngredientDescription"].Value = ingredient.IngredientDescription;
            cmd.Parameters["@MeasurementType"].Value = ingredient.MeasurementType;

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
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

        public int InsertIngredientIntoRecipe(int recipeID, int ingredientID, decimal quantity)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_ingredient_into_recipe", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RecipeID", SqlDbType.Int);
            cmd.Parameters.Add("@IngredientID", SqlDbType.Int);
            cmd.Parameters.Add("@Quantity", SqlDbType.Decimal);

            cmd.Parameters["@RecipeID"].Value = recipeID;
            cmd.Parameters["@IngredientID"].Value = ingredientID;
            cmd.Parameters["@Quantity"].Value = quantity;

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
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

        public List<Ingredient> SelectAllIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            
            var conn = DBConnection.GetDBConnection();
            
            var cmd = new SqlCommand("sp_select_all_ingredients", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var ingredient = new Ingredient()
                        {
                            IngredientID = reader.GetInt32(0),
                            IngredientName = reader.GetString(1),
                            IngredientDescription = reader.GetString(2),
                            IngredientType = reader.GetString(3),
                            MeasurementType = reader.GetString(4)
                        };
                        ingredients.Add(ingredient);
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
            return ingredients;
        }

        public List<RecipeIngredient> SelectIngredientQuantityByRecipeID(int recipeID)
        {
            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_ingredient_quantity_by_recipe_id", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RecipeID", SqlDbType.Int);

            cmd.Parameters["@RecipeID"].Value = recipeID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var recipeIngredient = new RecipeIngredient()
                        {
                            IngredientID = reader.GetInt32(0),
                            IngredientName = reader.GetString(1),
                            Quantity = reader.GetDecimal(2),
                            MeasurementType = reader.GetString(3),
                            IngredientDescription = reader.GetString(4)
                        };
                        recipeIngredients.Add(recipeIngredient);
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

            return recipeIngredients;
        }

        public int UpdateIngredientData(Ingredient oldIngredient, Ingredient newIngredient)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_ingredient_data", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IngredientID", SqlDbType.Int);
            cmd.Parameters.Add("@OldIngredientName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldIngredientType", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldIngredientDescription", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@OldMeasurementType", SqlDbType.NVarChar, 255);

            cmd.Parameters.Add("@NewIngredientName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewIngredientType", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewIngredientDescription", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@NewMeasurementType", SqlDbType.NVarChar, 255);

            cmd.Parameters["@IngredientID"].Value = oldIngredient.IngredientID;
            cmd.Parameters["@OldIngredientName"].Value = oldIngredient.IngredientName;
            cmd.Parameters["@OldIngredientType"].Value = oldIngredient.IngredientType;
            cmd.Parameters["@OldIngredientDescription"].Value = oldIngredient.IngredientDescription;
            cmd.Parameters["@OldMeasurementType"].Value = oldIngredient.MeasurementType;

            cmd.Parameters["@NewIngredientName"].Value = newIngredient.IngredientName;
            cmd.Parameters["@NewIngredientType"].Value = newIngredient.IngredientType;
            cmd.Parameters["@NewIngredientDescription"].Value = newIngredient.IngredientDescription;
            cmd.Parameters["@NewMeasurementType"].Value = newIngredient.MeasurementType;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
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

    }
}
