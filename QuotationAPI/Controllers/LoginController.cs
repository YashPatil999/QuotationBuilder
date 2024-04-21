using QuotationBAL.Security;
using QuotationBAL.Services;
using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace QuotationAPI.Controllers
{
    public class LoginController : ApiController
    {
        LoginService login = new LoginService();
        LoginModelViewModel emvm = new LoginModelViewModel();
        AesGcmExample gcm = new AesGcmExample();
        ExpirationTimeFormat expire = new ExpirationTimeFormat();

        [Route("AddUser")]
        [HttpPost]
        public ResponseStatusModel AddUser(UserMasterModel user)
        {
            UserMasterModelViewModel status = new UserMasterModelViewModel();
            ResponseStatusModel responseStatus = new ResponseStatusModel();
            /*            UserMasterModel user = new UserMasterModel();
                        user.IV = Data.Substring(0, 24);
                        int length = 64;
                        int startIndex = Data.Length - length;
                        user.EncryptedKey = Data.Substring(startIndex, length);
                        user.EncryptedPassword = Data.Substring(24, Data.Length - 88);*/
            /*status.masterModel.EncryptedPassword = EData;
            status.masterModel.EncryptedKey = Key;
            status.masterModel.IV = IV;*/
            try
            {
                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.EncryptedPassword))
                {

                    status.response.n = 0;
                    status.response.status = "Failed";
                    status.response.message = "Username and password is mandatory";
                }
                else
                {
                    status = login.AddUser(user);

                }
            }
            catch (Exception ex)
            {
                responseStatus = ExceptionHandler.ExceptionSave(this.ControllerContext.RouteData.Values, ex);
            }
            return responseStatus;
        }

        [Route("CheckUser")]
        [HttpPost]
        public ResponseStatusModel CheckUser(UserMasterModel user)
        {
            UserMasterModelViewModel userModel = new UserMasterModelViewModel();
            TokenResponse response = new TokenResponse();
            ResponseStatusModel statusModel = new ResponseStatusModel();
            try
            {
                if (string.IsNullOrEmpty(user.Email))
                {
                    statusModel.n = 0;
                    statusModel.Status = "Failed";
                    statusModel.StatusMessage = "Username Not Found!!!";
                }
                else
                {
                    userModel = login.CheckUser(user);

                    if (userModel.masterModel == null)
                    {
                        statusModel.StatusMessage = "Invalid Email";
                        statusModel.Status = "Login Failed";
                        statusModel.n = 404;

                    }
                    else
                    {
                        //string DData = gcm.DeData(userModel.masterModel);
                        if (userModel.masterModel.EncryptedPassword != user.EncryptedPassword)
                        {
                            statusModel.StatusMessage = "Invalid Password";
                            statusModel.Status = "Login Failed";
                            statusModel.n = 404;
                        }
                        else
                        {
                            statusModel.StatusMessage = "Login Successful";
                            statusModel.Status = "Successful";   
                            statusModel.n = 1;
                            /*UserMasterModel userlogin = new UserMasterModel();
                            if (userModel.masterModel.Token != null)
                            {
                                DateTime Expire_DateTime = userModel.masterModel.TKExpireTime;
                                DateTime currentDateTime = DateTime.Now;

                                int comparisonResult = DateTime.Compare(Expire_DateTime, currentDateTime);

                                if (comparisonResult > 0)
                                {
                                    userModel.response.message = "Token is live!!";
                                    userModel.response.status = "Live";
                                    userModel.response.n = 1;
                                }
                                else if (comparisonResult < 0)
                                {
                                    userlogin.Email = EncryptDecrypt.Encrypt(userModel.masterModel.Id + "|" + DateTime.Now.ToString());
                                    userlogin.EncryptedPassword = EncryptDecrypt.Encrypt(userModel.masterModel.EncryptedPassword + "|" + DateTime.Now.ToString());
                                    response = TokenReqService.GetToken(userlogin);
                                    userModel.Token = response;
                                    if (userModel.Token != null)
                                    {
                                        string formattedResult = expire.TimeFormat(userModel.Token);
                                        userModel.Token.expires_in = formattedResult;
                                        login.AddToken(userModel);
                                    }
                                }
                            }
                            else
                            {
                                userlogin.Email = EncryptDecrypt.Encrypt(userModel.masterModel.Id + "|" + DateTime.Now.ToString());
                                userlogin.EncryptedPassword = EncryptDecrypt.Encrypt(userModel.masterModel.EncryptedPassword + "|" + DateTime.Now.ToString());
                                response = TokenReqService.GetToken(userlogin);
                                userModel.Token = response;
                                if (userModel.Token != null)
                                {
                                    string formattedResult = expire.TimeFormat(userModel.Token);
                                    userModel.Token.expires_in = formattedResult;
                                    login.AddToken(userModel);
                                }
                            }*/
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                statusModel = ExceptionHandler.ExceptionSave(this.ControllerContext.RouteData.Values, ex);
            }
            return statusModel;
        }
    }
}
