using Pro_EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro_DAL.Repo
{
    public class UserDataService : IUser
    {
        DecDb23NewEntities ProDb;
        UserModel UModel;
        UserProfile UProf;
        List<UserModel> UserList;
        public UserDataService()
        {
            ProDb = new DecDb23NewEntities();
            UModel = new UserModel();
            UProf = new UserProfile();
        }
       
        // user registration 
        public void AddUser(UserModel NewUser)
        {
            try
            {
                // code to copy usermodel data to userprofile model
                // you can use automapper also here

                UProf.User_Id = NewUser.User_Id;
                UProf.UserName = NewUser.UserName;
                UProf.Password = NewUser.Password;
                UProf.Role = NewUser.Role;

                ProDb.UserProfiles.Add(UProf);
                ProDb.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // user login
        public UserModel GetUser(UserModel user)
        {
            try
            {
                var dat = ProDb.UserProfiles.Where(u => u.UserName == user.UserName && u.Password == user.Password).SingleOrDefault();

                // copying data from userprofile to usermodel
                // you can also use automapper tool
              
                UModel.User_Id = dat.User_Id;
                UModel.UserName = dat.UserName;
                UModel.Password = dat.Password;
                UModel.Role = dat.Role;

                return UModel;
            }
           catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
