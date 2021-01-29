using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IUserManager
    {
        User AuthenticateUser(string username, string password);

        bool UpdatePassword(string email, string oldPassword, string NewPassword);
    }
}
