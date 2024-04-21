using QuotationDAL.Repositories;
using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationBAL.Services
{
    public class LoginService
    {
        LoginRepository login = new LoginRepository();
        public UserMasterModelViewModel AddUser(UserMasterModel user)
        {
            return login.AddUser(user);
        }

        public UserMasterModelViewModel CheckUser(UserMasterModel user)
        {
            return login.CheckUser(user);
        }

        /*public UserMasterModelViewModel LoginUser(UserMasterModel user)
        {
            return login.LoginUser(user);
        }*/

        public UserMasterModelViewModel AddToken(UserMasterModelViewModel token)
        {
            return login.AddToken(token);
        }
    }
}
