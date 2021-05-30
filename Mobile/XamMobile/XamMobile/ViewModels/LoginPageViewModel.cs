﻿using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamMobile.Models;
using XamMobile.Services;
using XamMobile.Views;

namespace XamMobile.ViewModels
{
    public class LoginPageViewModel: ViewModelBase
    {
        public DelegateCommand GotoHomePageCommand { get; private set; }

        private string _userName;
        public string UserName {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        IUserService iUserService;
        public LoginPageViewModel(INavigationService navigationService, IUserService iUserService) : base(navigationService)
        {
            GotoHomePageCommand = new DelegateCommand(GotoHomePage);
            this.iUserService = iUserService;
        }

        public async void GotoHomePage()
        {
            using (UserDialogs.Instance.Loading("Đang đăng nhập"))
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    UserDialogs.Instance.Alert("Không được để trống mục tài khoản hoặc mật khẩu");
                    return;
                }
                var res = await iUserService.Login(UserName, Password);
                if (res.IsSuccess)
                {
                    var userInfoResponse = await iUserService.GetUserInfo();
                    if (userInfoResponse != null)
                    {
                        UserInfoSetting.UserInfos = userInfoResponse;
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("Có lỗi xảy ra khi lấy thông tin người dùng");
                        return;
                    }

                    await NavigationService.NavigateAsync(nameof(MenuPage) + "/" + nameof(NavigationPage) + "/" + nameof(HomeMenuPage));
                }
                else
                {
                    UserDialogs.Instance.Alert("Tài khoản hoặc mật khẩu không hợp lệ");
                }
            }
        }
    }
}
