using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserExcercise
{
    

    class Action
    {
        public User ActiveUser;
        protected IProvideData DataProvider;

        public Action(User activeUser, IProvideData dataProvider)
        {
            ActiveUser = activeUser;
            DataProvider = dataProvider;

        }
    }
}
