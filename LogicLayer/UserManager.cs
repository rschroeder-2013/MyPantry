using DataAccessLayer;
using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        private IUserAccessor _userAccessor;

        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }

        public UserManager(IUserAccessor uAccessor)
        {
            this._userAccessor = uAccessor;
        }

        public User AuthenticateUser(string username, string password)
        {
            User user = null;


            password = password.hashSHA256();

            try
            {
                if (1 == _userAccessor.VerifyUsernameAndPassword(username, password))
                {
                    user = _userAccessor.SelectUserByEmail(username);
                }
                else
                {
                    throw new ApplicationException("Bad Username or Password");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Login Failed.", ex);
            }

            return user;
        }

        public bool UpdatePassword(string email, string oldPassword, string newPassword)
        {
            bool result = false;

            try
            {
                oldPassword = oldPassword.hashSHA256();
                newPassword = newPassword.hashSHA256();
                result = (1 == _userAccessor.UpdatePassword(
                    email, newPassword, oldPassword));
                if (!result)
                {
                    throw new ApplicationException("Password Update Failed.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Password not changed.", ex);
            }

            return result;
        }
    }
}
