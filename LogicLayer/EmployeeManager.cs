using DataAccessLayer;
using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class EmployeeManager : IEmployeeManager
    {
        private IEmployeeAccessor _employeeAccessor;

        public EmployeeManager()
        {
            _employeeAccessor = new EmployeeAccessor();
        }
        public EmployeeManager(IEmployeeAccessor employeeAccessor)
        {
            _employeeAccessor = employeeAccessor;
        }

        public bool AddEmployee(EmployeeVM employee)
        {
            bool result = false;

            try
            {
                int employeeID = 0;
                employeeID = _employeeAccessor.InsertEmployee(employee);

                if (employeeID == 0)
                {
                    throw new ApplicationException("Employee not added");
                }

                //fix next class after working out combo box with Jim

                //foreach (var role in employee.Roles)
                //{
                //    _employeeAccessor.InsertEmployeeRole(employeeID, role);
                //}
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Insert Failed.", ex);
            }
            return result;
        }

        public bool EditEmployeeData(EmployeeVM oldEmployee, EmployeeVM newEmployee)
        {
            bool result = false;

            try
            {
                result = (1 == _employeeAccessor.UpdateEmployeeData(oldEmployee, newEmployee));

                if (result == false)
                {
                    throw new ApplicationException("Data not updated.");
                }

                foreach (var role in newEmployee.Roles)
                {
                    if (!oldEmployee.Roles.Contains(role))
                    {
                        _employeeAccessor.InsertEmployeeRole(oldEmployee.EmployeeID, role);
                    }
                }


                foreach (var role in oldEmployee.Roles)
                {
                    if (!newEmployee.Roles.Contains(role))
                    {
                        _employeeAccessor.DeleteEmployeeRole(oldEmployee.EmployeeID, role);
                    }
                }


                if (oldEmployee.Active != newEmployee.Active)
                {
                    if (newEmployee.Active)
                    {
                        _employeeAccessor.ReactivateEmployee(oldEmployee.EmployeeID);
                    }
                    else
                    {
                        if (_employeeAccessor.DeactivateEmployee(oldEmployee.EmployeeID) != 1)
                        {
                            throw new ApplicationException("The employee could not be deactivated.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }
            return result;
        }

        public List<string> RetrieveAllRoles()
        {
            List<string> roles = null;

            try
            {
                roles = _employeeAccessor.SelectAllRoles();

                if (roles == null)
                {
                    roles = new List<string>();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }
            return roles;
        }

        public List<EmployeeVM> RetrieveEmployeesByActive(bool active = true)
        {
            List<EmployeeVM> employees = null;

            try
            {
                employees = _employeeAccessor.SelectEmployeesByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("List not available", ex);
            }

            return employees;
        }

        public List<string> RetrieveRolesByEmployeeID(int employeeID)
        {
            List<string> roles = null;

            try
            {
                roles = _employeeAccessor.SelectRolesByEmployeeID(employeeID);

                if (roles == null)
                {
                    roles = new List<string>();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }
            return roles;
        }
    }
}
