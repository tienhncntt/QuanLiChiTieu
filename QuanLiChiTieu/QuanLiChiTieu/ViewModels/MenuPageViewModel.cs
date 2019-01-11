using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using QuanLiChiTieu.Models;
using Xamarin.Forms;

namespace QuanLiChiTieu.ViewModels
{
	public class MenuPageViewModel : BindableBase
    {

        private ObservableCollection<PageInfo> _listPage;
        public ObservableCollection<PageInfo> ListPage
        {
            get => _listPage;
            set => SetProperty(ref _listPage, value);
        }
        private PageInfo _chosenPage;
        public PageInfo ChosenPage     
        {
            get => _chosenPage;
            set => SetProperty(ref _chosenPage, value);
        }
        private INavigationService _navigationService;
        public ICommand NavigationCommand { get; set; }
        public MenuPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Init();
            NavigationCommand = new Command(Navigate);
        }

        private void Init()
        {
            ChosenPage = new PageInfo();
            ListPage = new ObservableCollection<PageInfo>
            {
                new PageInfo(){Name = "THỐNG KÊ", NameParam = "ReportTabbedPage", Icon = "icon_chart"},
                new PageInfo(){Name = "DANH SÁCH THU/CHI", NameParam = "MoneyTabbedPage", Icon = "icon_list"},
                new PageInfo(){Name = "CÀI ĐẶT", NameParam = "SettingPage", Icon = "icon_setting"},
                new PageInfo(){Name = "THÔNG TIN ỨNG DỤNG", NameParam = "AboutPage", Icon = "icon_info"}
            };

        }

        private async void Navigate()
        {
            await _navigationService.NavigateAsync("NavigationPage/" + ChosenPage.NameParam);
        }
    }
}
