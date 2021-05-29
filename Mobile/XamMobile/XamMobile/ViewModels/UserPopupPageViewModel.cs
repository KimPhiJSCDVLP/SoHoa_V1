using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamMobile.EntityModels;
using XamMobile.Models;
using XamMobile.Services;

namespace XamMobile.ViewModels
{
    public class UserPopupPageViewModel:ViewModelBase
    {
        public DelegateCommand ClosePopupCommand { get; private set; }
        public DelegateCommand SaveNhanKhauCommand { get; private set; }
        private NhanKhauEntity _currentData;

        IUserService iUserService;
        public NhanKhauEntity CurrentData
        {
            get { return _currentData; }
            set { SetProperty(ref _currentData, value); }
        }
        public UserPopupPageViewModel(INavigationService navigationService, IUserService iUserService) : base(navigationService)
        {
            this.iUserService = iUserService;
            SaveNhanKhauCommand = new DelegateCommand(async() => { await SaveNhanKhau(); });
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            CurrentData = parameters.GetValue<NhanKhauEntity>("obj");
            base.OnNavigatedTo(parameters);
        }

        public async Task SaveNhanKhau()
        {
            if(CurrentData.NhanKhauId == 0)
            {
                if (!CurrentData.HoGiaDinhId.HasValue || CurrentData.HoGiaDinhId == 0)
                    CurrentData.HoGiaDinhId = UserInfoSetting.UserInfos.HoGiaDinhId;
                if (CurrentData.NgaySinh == null)
                    CurrentData.NgaySinh = DateTime.Now;
            }
            var res = await iUserService.SaveNhanKhau(CurrentData);
            MessagingCenter.Send((App)Application.Current, "UpdateNhanKhau", res);
            UserDialogs.Instance.Toast("Saved");
        }
    }
}
