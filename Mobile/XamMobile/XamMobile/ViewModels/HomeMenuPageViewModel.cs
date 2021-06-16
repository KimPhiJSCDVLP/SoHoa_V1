using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XamMobile.EntityModels;
using XamMobile.Models;
using XamMobile.Services;
using XamMobile.Views;

namespace XamMobile.ViewModels
{
    public class HomeMenuPageViewModel : ViewModelBase
    {
        private ObservableCollection<NotificationEntity> _notifications;
        public ObservableCollection<NotificationEntity> Notifications
        {
            get { return _notifications; }
            set { SetProperty(ref _notifications, value); }
        }

        public DelegateCommand GotoUserInfoPageCommand { get; private set; }
        public DelegateCommand GotoLogPageCommand { get; private set; }

        INotificationService iNotificationService;

        public HomeMenuPageViewModel(INavigationService navigationService, INotificationService iNotificationService) : base(navigationService)
        {
            this.iNotificationService = iNotificationService;
            Notifications = new ObservableCollection<NotificationEntity>();
            //{
            //    new NotificationEntity(){Title = "A young woman leads refugees toward independence", Content = "She’s lived in a refugee camp since her family fled war when she was a little girl, but Grace Nshimiyumukiza has always wanted to be the one to help, not just be helped."},
            //    new NotificationEntity(){Title = "News reporting grantee winner quantifies how climate change is affecting everyday life", Content = "Josh Landis lived and worked in Antarctica from 1999 to 2001 as an editor at The Antarctic Sun, the only newspaper on the continent at that time. And while he was hardly the only person on the continent, it could often feel like that in a peaceful sort of way."},
            //    new NotificationEntity(){Title = "A young woman leads refugees toward independence", Content = "She’s lived in a refugee camp since her family fled war when she was a little girl, but Grace Nshimiyumukiza has always wanted to be the one to help, not just be helped."},
            //    new NotificationEntity(){Title = "Learning how climate change affects everyday life", Content = "“Being able to sit on the ice, with penguins walking by and seeing killer whales spyhopping in the water, rising and falling right in front of me, stuck with me forever,”"},
            //    new NotificationEntity(){Title = "A young woman leads refugees toward independence", Content = "She’s lived in a refugee camp since her family fled war when she was a little girl, but Grace Nshimiyumukiza has always wanted to be the one to help, not just be helped."}
            //});

            GotoUserInfoPageCommand = new DelegateCommand(() => { GotoPage("UserPage"); });
            GotoLogPageCommand = new DelegateCommand(() => { GotoPage("LogPage"); });
            LoadAllData();
        }

        public void GotoPage(string page)
        {
            NavigatetoPage(page);
        }

        public async void NavigatetoPage(string page)
        {
            await NavigationService.NavigateAsync(page);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        private async void LoadAllData()
        {
            //using (UserDialogs.Instance.Loading("Đang tải"))
            //{
            //    var notificationResults = await iNotificationService.GetAllNotification(UserInfoSetting.UserInfos.ToaNhaId);
            //    if (notificationResults == null)
            //    {
            //        UserDialogs.Instance.Alert("Có lỗi khi tải thông báo");
            //        return;
            //    }
            //    Notifications.Clear();
            //    foreach (var item in notificationResults)
            //    {
            //        Notifications.Add(item);
            //    }
            //    Console.WriteLine("");
            //}
        }

        public async void OpenUserPopUp(object obj = null)
        {
            var navigationParamters = new NavigationParameters();
            navigationParamters.Add("obj", obj);
            await NavigationService.NavigateAsync("NotificationPopupPage", navigationParamters);
        }
    }
}
