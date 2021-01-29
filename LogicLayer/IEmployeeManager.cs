using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IEmployeeManager
    {
        List<EmployeeVM> RetrieveEmployeesByActive(bool active = true);

        List<string> RetrieveRolesByEmployeeID(int employeeID);

        List<string> RetrieveAllRoles();

        bool EditEmployeeData(EmployeeVM oldEmployee, EmployeeVM newEmployee);

        bool AddEmployee(EmployeeVM employee);
    }
}
