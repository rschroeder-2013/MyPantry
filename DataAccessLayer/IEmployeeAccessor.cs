using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IEmployeeAccessor
    {
        List<EmployeeVM> SelectEmployeesByActive(bool active = true);

        List<string> SelectRolesByEmployeeID(int employeeID);

        List<string> SelectAllRoles();

        int UpdateEmployeeData(Employee oldEmployee, Employee newEmployee);

        int DeactivateEmployee(int employeeID);

        int ReactivateEmployee(int employeeID);

        int InsertEmployeeRole(int employeeID, string roleID);

        int DeleteEmployeeRole(int employeeID, string roleID);

        int InsertEmployee(Employee employee);
    }
}
