﻿using Acr.UserDialogs;
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
    public class InvoiceViewModel : ViewModelBase
    {
        private ObservableCollection<PhieuThuEntity> _invoices;
        public ObservableCollection<PhieuThuEntity> Invoices
        {
            get { return _invoices; }
            set { SetProperty(ref _invoices, value); }
        }

        private ObservableCollection<string> _actionDatasource;
        public ObservableCollection<string> ActionDatasource
        {
            get { return _actionDatasource; }
            set { SetProperty(ref _actionDatasource, value); }
        }

        private double _invoiceListHeight;
        public double InvoiceListHeight
        {
            get { return _invoiceListHeight; }
            set { SetProperty(ref _invoiceListHeight, value); }
        }

        IInvoiceService invoiceService;
        IPermissionService _permissionService;
        DownloadService downloadService { get; set; }
        IFileService _fileService;

        public InvoiceViewModel(INavigationService navigationService, IInvoiceService invoiceService) : base(navigationService)
        {
            this.invoiceService = invoiceService;
            Invoices = new ObservableCollection<PhieuThuEntity>();
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
            InvoiceListHeight = Invoices.Count * 90;
        }

        private async void LoadAllData()
        {
            using (UserDialogs.Instance.Loading("Đang tải"))
            {
                var invoiceRes = await invoiceService.GetInvoices(UserInfoSetting.UserInfos.PhongId);
                if (invoiceRes == null)
                {
                    UserDialogs.Instance.Alert("Có lỗi khi tải thông tin phiếu thu");
                    return;
                }
                Invoices.Clear();
                foreach (var item in invoiceRes)
                {
                    Invoices.Add(item);
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
