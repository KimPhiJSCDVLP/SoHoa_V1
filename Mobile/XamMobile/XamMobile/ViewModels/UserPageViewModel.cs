using Acr.UserDialogs;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamMobile.DependencyServices;
using XamMobile.EntityModels;
using XamMobile.Models;
using XamMobile.Services;
using XamMobile.Views.MasterDetail;

namespace XamMobile.ViewModels
{
    public class UserPageViewModel : ViewModelBase
    {
        private ObservableCollection<NhanKhauEntity> _nhanKhaus;
        public ObservableCollection<NhanKhauEntity> NhanKhaus
        {
            get { return _nhanKhaus; }
            set { SetProperty(ref _nhanKhaus, value); }
        }

        private ObservableCollection<string> _actionDatasource;
        public ObservableCollection<string> ActionDatasource
        {
            get { return _actionDatasource; }
            set { SetProperty(ref _actionDatasource, value); }
        }

        public UserPage CurrentPage { get; set; }

        private double _nhanKhauHeight;
        public double NhanKhauHeight
        {
            get { return _nhanKhauHeight; }
            set { SetProperty(ref _nhanKhauHeight, value); }
        }

        private UserInfo _userInfoModel;
        public UserInfo UserInfoModel
        {
            get { return _userInfoModel; }
            set { SetProperty(ref _userInfoModel, value); }
        }


        private ImageSource _imageSource;
        public ImageSource ImageSourceAvatar
        {
            get { return _imageSource; }
            set { SetProperty(ref _imageSource, value); }
        }

        IPermissionService _permissionService;
        DownloadService downloadService { get; set; }
        IFileService _fileService;
        IUserService iUserService;
        IUploadFileService iUploadFileService;
        public DelegateCommand UpdatePictureCommand { get; private set; }


        public UserPageViewModel(INavigationService navigationService, IUserService iUserService, IUploadFileService iUploadFileService) : base(navigationService)
        {
            this.iUserService = iUserService;
            this.iUploadFileService = iUploadFileService;
            NhanKhaus = new ObservableCollection<NhanKhauEntity>();
            UserInfoModel = UserInfoSetting.UserInfos;
            ActionDatasource = new ObservableCollection<string>(new List<string>() { "Chỉnh sửa", "Xóa" });
            _permissionService = Xamarin.Forms.DependencyService.Get<DependencyServices.IPermissionService>();
            _fileService = Xamarin.Forms.DependencyService.Get<DependencyServices.IFileService>();
            downloadService = new DownloadService(_permissionService, _fileService);
            UpdatePictureCommand = new DelegateCommand(async() => { await UpdatePicture(); });
            MessagingCenter.Unsubscribe<App, NhanKhauEntity>((App)Application.Current, "UpdateNhanKhau");
            MessagingCenter.Subscribe<App, NhanKhauEntity>((App)Application.Current, "UpdateNhanKhau", (o, serverItem) =>
            {
                try
                {
                    var updateItem = NhanKhaus.FirstOrDefault(x => x.NhanKhauId == serverItem.NhanKhauId);
                    if (updateItem == null)
                    {
                        NhanKhaus.Add(serverItem);
                    }
                    else
                    {
                        updateItem = serverItem;
                    }
                    CalculateHeight();
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.Alert("Lỗi cập nhật ui");
                }
            });
        }

        private async Task UpdatePicture()
        {
            try
            {
                var fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking

                var fileName = fileData.FileName;
                var contents = fileData.DataArray;

                var imageRes = await iUploadFileService.UploadFile(new Services.Models.FileUploaded() { FileName = fileName, Content = contents });
                if (!string.IsNullOrEmpty(imageRes))
                {
                    var updateAvatar = await iUserService.UpdateHGDAvatar(UserInfoSetting.UserInfos.HoGiaDinhId, imageRes.Replace("\"", ""));
                    if (updateAvatar)
                    {
                        UserInfoSetting.UserInfos.AnhDaiDien = imageRes.Replace("\"", "");
                        ImageSourceAvatar = string.IsNullOrEmpty(UserInfoSetting.UserInfos.AnhDaiDien) ? null : await downloadService.DownloadFileIntoMemory($"{AppConstant.AppConstant.Endpoint}{AppConstant.AppConstant.APIGetImage}{UserInfoSetting.UserInfos.AnhDaiDien}");
                        UserDialogs.Instance.Toast("Cập nhật ảnh đại diện thành công");
                    }
                }

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            LoadAllData();
        }

        private async void LoadAllData()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Đang tải"))
                {
                    ImageSourceAvatar = string.IsNullOrEmpty(UserInfoSetting.UserInfos.AnhDaiDien) ? null : await downloadService.DownloadFileIntoMemory($"{AppConstant.AppConstant.Endpoint}{AppConstant.AppConstant.APIGetImage}{UserInfoSetting.UserInfos.AnhDaiDien}");
                    var nhanKhauRes = await iUserService.GetNhanKhau(UserInfoSetting.UserInfos.HoGiaDinhId);
                    if (nhanKhauRes == null)
                    {
                        UserDialogs.Instance.Alert("Có lỗi khi tải thông tin nhân khẩu");
                        return;
                    }
                    NhanKhaus.Clear();
                    foreach (var item in nhanKhauRes)
                    {
                        NhanKhaus.Add(item);
                    }
                    CalculateHeight();
                }
            }
            catch (Exception)
            {
                UserDialogs.Instance.Toast("Có lỗi khi tải dữ liệu");
            }
        }

        private void CalculateHeight()
        {
            NhanKhauHeight = NhanKhaus.Count * 90;
        }

        public async void OpenUserPopUp(object obj = null)
        {
            var navigationParamters = new NavigationParameters();
            if (obj == null)
                obj = new NhanKhauEntity();
            navigationParamters.Add("obj", obj);
            await NavigationService.NavigateAsync("UserPopupPage", navigationParamters);
        }

        public async void ConfirmDelete(object obj = null)
        {
            if (obj == null)
                return;

            var con = await UserDialogs.Instance.ConfirmAsync("Xác nhận muốn xóa bản ghi");
            if (con)
            {
                var model = (obj as NhanKhauEntity);
                var deleted = await iUserService.DeleteNhanKhau(model);
                if (deleted)
                {
                    var itemDeleted = NhanKhaus.FirstOrDefault(x => x.NhanKhauId == model.NhanKhauId);
                    NhanKhaus.Remove(itemDeleted);
                    CalculateHeight();
                    UserDialogs.Instance.Toast("Xóa thành công", TimeSpan.FromSeconds(2));
                }
            }

        }
    }
}
