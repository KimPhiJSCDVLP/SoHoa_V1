using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamMobile.EntityModels;
using XamMobile.Services.Models;

namespace XamMobile.Services
{
    public interface IUserService
    {
        Task<MessageResponse> Login(string userName, string passWord);
        Task<bool> UpdateHGDAvatar(int hoGiaDinhId, string imageName);
        Task<UserInfo> GetUserInfo();
        Task<List<NhanKhauEntity>> GetNhanKhau(int hoGiaDinhId);
        Task<NhanKhauEntity> SaveNhanKhau(NhanKhauEntity model);
        Task<bool> DeleteNhanKhau(NhanKhauEntity model);
    }
}
