using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkraineRestorationArchive.BLL
{
    public interface IAccountManager
    {
        public bool UserAlreadyExist(string username);
        public bool EmailAlreadyInUse(string EmailAddres);
        public void AddUser(string username, string emailAddres, string Password);
        public bool verifyUser(string login, string password);
        public string getRole(string email);
        public string getUsername(string email);
    }
}
