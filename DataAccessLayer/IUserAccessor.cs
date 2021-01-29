using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataTransferObjects;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IUserAccessor
    {

        int VerifyUsernameAndPassword(string username, string passwordHash);

        User SelectUserByEmail(string email);

        int UpdatePassword(string email, string newPasswordHash, string oldPasswordHash);


    }
}
