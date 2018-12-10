using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise1
{
    class Validlogin
    {
        public User ValidLogin(IProvideData dataprovider,string username, string password)
        { 
            List<User> currentlist = dataprovider.ReadUsers();
            foreach (User user in currentlist)
            {
                if (username == user.Username && password == user.Password)
                {
                    return user;
                }

            }
            return null;
        }
    }
}
