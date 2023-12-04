using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pro_EntityLayer;
namespace Pro_DAL.Repo
{
    public interface IUser
    {
        void AddUser(UserModel NewUser);
        UserModel GetUser(UserModel user);



    }
}
