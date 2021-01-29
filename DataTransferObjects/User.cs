using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class User
    {
        public int UserID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public bool Active { get; private set; }

        public List<string> Roles { get; private set; }

        public User(int userID, string firstName, string lastName,
            string phoneNumber, string email, bool active,
            List<string> roles)
        {
            this.UserID = userID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.Active = active;
            this.Roles = roles;
        }
    }
}
