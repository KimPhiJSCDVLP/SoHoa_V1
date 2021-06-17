using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamMobile.EntityModels;
using XamMobile.Services.Models;

namespace XamMobile.Services
{
    public class UserService : BaseService, IUserService
    {
        public async Task<List<NhanKhauEntity>> GetNhanKhau(int hoGiaDinhId)
        {
            try
            {
                var userResponse = await GetRequestWithHandleErrorAsync<OdataResult<List<NhanKhauEntity>>>($"{AppConstant.AppConstant.APINhanKhau}?$filter=HoGiaDinhId eq {hoGiaDinhId}");
                if (userResponse.Message.Code == ResponseCode.SUCCESS)
                {
                    return userResponse.Result.Results;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserInfo> GetUserInfo()
        {
            try
            {
                var userResponse = await GetRequestAsync<UserInfo>(AppConstant.AppConstant.APIUserInfo);
                return userResponse;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserEntity> GetUserById(int userId)
        {
            try
            {
                var userResponse = await GetRequestWithHandleErrorAsync<OdataResult<UserEntity>>($"{AppConstant.AppConstant.APIUser}?$filter=UserID eq {userId}");
                if (userResponse.Message.Code == ResponseCode.SUCCESS)
                {
                    return userResponse.Result.Results;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<UserEntity>> GetUsers()
        {
            try
            {
                var userResponse = await GetRequestWithHandleErrorAsync<OdataResult<List<UserEntity>>>($"{AppConstant.AppConstant.APIUser}");
                if (userResponse.Message.Code == ResponseCode.SUCCESS)
                {
                    return userResponse.Result.Results;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<MessageResponse> Login(string userName, string password)
        {
            MessageResponse result = new MessageResponse();
            var reqData = new List<KeyValuePair<string, string>>();
            reqData.Add(new KeyValuePair<string, string>("username", userName));
            reqData.Add(new KeyValuePair<string, string>("password", password));
            reqData.Add(new KeyValuePair<string, string>("grant_type", "password"));

            try
            {
                var authenResponse = await PostRequestFormAsync<AuthenResponse>("/token", reqData);
                AccessToken = authenResponse.AccessToken;
                result.Code = ResponseCode.SUCCESS;
            }
            catch (Exception)
            {
                result.Code = ResponseCode.ERROR;
                return result;
            }
            return result;
        }

        public async Task<NhanKhauEntity> SaveNhanKhau(NhanKhauEntity model)
        {
            try
            {
                var result = await PostRequestWithHandleErrorAsync<NhanKhauEntity, NhanKhauEntity>(AppConstant.AppConstant.APIInsertOrUpdateNhanKhau, model);
                if (result.Message.IsSuccess)
                {
                    return result.Result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteNhanKhau(NhanKhauEntity model)
        {
            try
            {
                var result = await PostRequestWithHandleErrorAsync<NhanKhauEntity, NhanKhauEntity>(AppConstant.AppConstant.APIDeleteNhanKhau, model);
                return result.Message.IsSuccess;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateHGDAvatar(int hoGiaDinhId, string imageName)
        {
            try
            {
                var res = await GetRequestAsync<string>($"{AppConstant.AppConstant.APIUpdateHoGiaDinhAvatar}?hoGiaDinhId={hoGiaDinhId}&imageName={imageName}");
                return !string.IsNullOrEmpty(res);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
