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
    public class EmployeeAccessor : IEmployeeAccessor
    {
        public int DeactivateEmployee(int employeeID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_safely_deactivate_mpuser", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MPUserID", employeeID);

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

        public int DeleteEmployeeRole(int employeeID, string roleID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_safely_remove_mpuser_role", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MPUserID", employeeID);
            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@RoleID"].Value = roleID;

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

        public int InsertEmployee(Employee employee)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_new_mpuser", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 15);

            cmd.Parameters["@Email"].Value = employee.Email;
            cmd.Parameters["@FirstName"].Value = employee.FirstName;
            cmd.Parameters["@LastName"].Value = employee.LastName;
            cmd.Parameters["@PhoneNumber"].Value = employee.PhoneNumber;

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

        public int InsertEmployeeRole(int employeeID, string roleID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_add_mpuser_role", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MPUserID", employeeID);
            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@RoleID"].Value = roleID;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new ApplicationException("Role could not be added.");
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

        public int ReactivateEmployee(int employeeID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_reactivate_mpuser", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MPUserID", employeeID);

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

        public List<string> SelectAllRoles()
        {
            List<string> roles = new List<string>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_all_mpuser_roles", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return roles;
        }

        public List<EmployeeVM> SelectEmployeesByActive(bool active = true)
        {
            List<EmployeeVM> employees = new List<EmployeeVM>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_mpusers_by_active", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var employee = new EmployeeVM()
                        {
                            EmployeeID = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            Active = reader.GetBoolean(5),
                        };
                        employees.Add(employee);
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
            return employees;
        }

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<string>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_mpuser_roles_by_mpuser_id", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@MPUserID", SqlDbType.Int);

            cmd.Parameters["@MPUserID"].Value = employeeID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return roles;
        }

        public int UpdateEmployeeData(Employee oldEmployee, Employee newEmployee)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_mpuser_profile_data", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@MPUserID", SqlDbType.Int);
            cmd.Parameters.Add("@OldEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldLastName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldPhoneNumber", SqlDbType.NVarChar, 15);

            cmd.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewLastName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPhoneNumber", SqlDbType.NVarChar, 15);

            cmd.Parameters["@MPUserID"].Value = oldEmployee.EmployeeID;
            cmd.Parameters["@OldEmail"].Value = oldEmployee.Email;
            cmd.Parameters["@OldFirstName"].Value = oldEmployee.FirstName;
            cmd.Parameters["@OldLastName"].Value = oldEmployee.LastName;
            cmd.Parameters["@OldPhoneNumber"].Value = oldEmployee.PhoneNumber;

            cmd.Parameters["@NewEmail"].Value = newEmployee.Email;
            cmd.Parameters["@NewFirstName"].Value = newEmployee.FirstName;
            cmd.Parameters["@NewLastName"].Value = newEmployee.LastName;
            cmd.Parameters["@NewPhoneNumber"].Value = newEmployee.PhoneNumber;

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
