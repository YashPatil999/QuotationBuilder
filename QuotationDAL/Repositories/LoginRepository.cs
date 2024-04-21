using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace QuotationDAL.Repositories
{
    public class LoginRepository
    {
        public UserMasterModelViewModel AddUser(UserMasterModel user)
        {
            UserMasterModelViewModel status = new UserMasterModelViewModel();
            string storepro = "[SP_AddNewUser]";
            using (IDbConnection con = new SqlConnection(DataConnection.GetConnection().ConnectionString))
            {
                var input = con.QueryMultiple(storepro, new
                {
                    Email = user.Email,
                    EncryptedPassword = user.EncryptedPassword,
                    EncryptedKey = user.EncryptedKey,
                    IV = user.IV,
                }, commandType: CommandType.StoredProcedure);
                status.response = input.Read<LoginResponseStatusModel>().SingleOrDefault();
            }
            return status;
        }

        public UserMasterModelViewModel CheckUser(UserMasterModel user)
        {
            UserMasterModelViewModel status = new UserMasterModelViewModel();
            string storepro = "[SP_CheckUser]";
            using (IDbConnection con = new SqlConnection(DataConnection.GetConnection().ConnectionString))
            {
                var input = con.QueryMultiple(storepro, new
                {
                    Email = user.Email,
                }, commandType: CommandType.StoredProcedure);
                status.masterModel = input.Read<UserMasterModel>().SingleOrDefault();
                status.response = input.Read<LoginResponseStatusModel>().SingleOrDefault();
            }
            return status;
        }

        /*public UserMasterModelViewModel LoginUser(UserMasterModel user) 
        {
            UserMasterModelViewModel userMaster = new UserMasterModelViewModel();
            string storepro = "[SP_LoginUser]";
            using(IDbConnection con = new SqlConnection(DataConnection.GetConnection().ConnectionString))
            {
                var input = con.QueryMultiple(storepro, new
                {
                    Email = user.Email,
                    EncryptedPassword = user.EncryptedPassword,
                }, commandType: CommandType.StoredProcedure);
                userMaster.masterModel= input.Read<UserMasterModel>().SingleOrDefault();
                userMaster.response = input.Read<LoginResponseStatusModel>().SingleOrDefault();
            }
            return userMaster;
        }*/

        public UserMasterModelViewModel AddToken(UserMasterModelViewModel token)
        {
            UserMasterModelViewModel userMasterModelViewModel = new UserMasterModelViewModel();
            string storepro = "[SP_AddToken]";
            using (IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString))
            {
                var input = conn.QueryMultiple(storepro, new
                {
                    Id = token.masterModel.Id,
                    Token = token.Token.access_token,
                    TKExpireTime = token.Token.expires_in

                }, commandType: CommandType.StoredProcedure);
                userMasterModelViewModel.response = input.Read<LoginResponseStatusModel>().SingleOrDefault();
            }
            return userMasterModelViewModel;
        }
    }
}

