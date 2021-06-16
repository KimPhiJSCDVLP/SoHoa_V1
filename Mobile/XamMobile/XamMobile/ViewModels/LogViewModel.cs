using Acr.UserDialogs;
using Plugin.Permissions.Abstractions;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XamMobile.DependencyServices;
using XamMobile.EntityModels;
using XamMobile.Models;
using XamMobile.Services;
using XamMobile.Ultil;

namespace XamMobile.ViewModels
{
    public class LogViewModel : ViewModelBase
    {
        private ObservableCollection<LogEntity> _logs;
        public ObservableCollection<LogEntity> Logs
        {
            get { return _logs; }
            set { SetProperty(ref _logs, value); }
        }

        private ObservableCollection<string> _actionDatasource;
        public ObservableCollection<string> ActionDatasource
        {
            get { return _actionDatasource; }
            set { SetProperty(ref _actionDatasource, value); }
        }

        private double _logListHeight;
        public double LogListHeight
        {
            get { return _logListHeight; }
            set { SetProperty(ref _logListHeight, value); }
        }

        ILogService logService;
        IPermissionService _permissionService;
        DownloadService downloadService { get; set; }
        IFileService _fileService;

        public LogViewModel(INavigationService navigationService, ILogService logService) : base(navigationService)
        {
            this.logService = logService;
            Logs = new ObservableCollection<LogEntity>();
            ActionDatasource = new ObservableCollection<string>(new List<string>() { "Xuất phiếu thu" });
            _permissionService = Xamarin.Forms.DependencyService.Get<DependencyServices.IPermissionService>();
            _fileService = Xamarin.Forms.DependencyService.Get<DependencyServices.IFileService>();
            downloadService = new DownloadService(_permissionService, _fileService);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            LoadAllData();
        }

        private void CalculateHeight()
        {
            LogListHeight = Logs.Count * 90;
        }

        private async void LoadAllData()
        {
            using (UserDialogs.Instance.Loading("Đang tải"))
            {
                var invoiceRes = await logService.GetLogs();
                if (invoiceRes == null)
                {
                    UserDialogs.Instance.Alert("Có lỗi khi tải thông tin phiếu thu");
                    return;
                }
                Logs.Clear();
                foreach (var item in invoiceRes)
                {
                    Logs.Add(item);
                }
                CalculateHeight();
            }
        }

        public async void ExortInvoice(object obj = null)
        {
            var invoice = obj as PhieuThuEntity;
            var isEnable = await PermisionChecking.CheckPermissions(Permission.Storage);
            if (!isEnable)
                return;
            await downloadService.DownloadFile($"{AppConstant.AppConstant.Endpoint}{AppConstant.AppConstant.APIPhieuThuExport}?Thang={invoice.Thang}&Nam={invoice.Nam}&PhongID={invoice.PhongId}&ExID={new Random().Next(1, 2000000)}", "CareDownload", null, $"phieuthu-{invoice.Thang}-{invoice.Nam}.xlsx", true, true, ActionFileDownload.Download, null, true).ConfigureAwait(false);
        }
    }
}
