using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL
{
    public class UserNameResolver : IUserNameResolver
    {
        private readonly Func<string> _userNameFactory; 
        public UserNameResolver(Func<string> userNameFactory)
        {
            _userNameFactory = userNameFactory;
        }

        public string CurrentUserName => _userNameFactory();

    }
}
