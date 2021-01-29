using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataTransferObjects;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        public User SelectUserByEmail(string email)
        {
            User user = null;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_mpuser_by_email", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    var userID = reader.GetInt32(0);
                    var firstName = reader.GetString(2);
                    var lastName = reader.GetString(3);
                    var phoneNumber = reader.GetString(4);
                    var active = reader.GetBoolean(5);
                    reader.Close();

                    IEmployeeAccessor employeeAccessor = new EmployeeAccessor();
                    var roles = employeeAccessor.SelectRolesByEmployeeID(userID);

                    user = new User(userID, firstName, lastName, phoneNumber, email,
                         active, roles);
                }
                else
                {
                    throw new ApplicationException("User not found.");
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
            return user;
        }

        public int VerifyUsernameAndPassword(string username, string passwordHash)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_authenticate_mpuser", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = username;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            
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

        public int UpdatePassword(string email, string newPasswordHash, string oldPasswordHash)
        {
            int result = 0;

           
            var conn = DBConnection.GetDBConnection();
            
            var cmd = new SqlCommand("sp_update_passwordHash", conn);
            
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
            
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            
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
