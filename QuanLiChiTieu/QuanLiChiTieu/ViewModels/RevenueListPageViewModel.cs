using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using QuanLiChiTieu.Models;
using Xamarin.Forms;

namespace QuanLiChiTieu.ViewModels
{
    public class RevenueListPageViewModel : BindableBase
    {
        private Money _selectedRevenue;
        private List<Money> _revenueList;
        public Database Database;

        public Money SelectedRevenue
        {
            get => _selectedRevenue;
            set { SetProperty(ref _selectedRevenue, value); }
        }

        public List<Money> RevenueList
        {
            get => _revenueList;
            set { SetProperty(ref _revenueList, value); }
        }

        public ICommand ToAdditionPageCommand { get; set; }
        public ICommand ToMoneyDetailPageCommand { get; set; }
        private INavigationService _navigationService;


        public RevenueListPageViewModel(INavigationService navigationService)
        {

            Database = new Database();
            RevenueList = Database.ListRevenue();
            _navigationService = navigationService;
            ToAdditionPageCommand = new Command(ToAdditionPage);
            ToMoneyDetailPageCommand = new Command(ToMoneyDetailPage);

        }

        private async void ToMoneyDetailPage()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("model", SelectedRevenue);
            await _navigationService.NavigateAsync("MoneyDetailPage", navigationParams);

        }

        private async void ToAdditionPage()
        {
            await _navigationService.NavigateAsync("AdditionPage");
        }
    }
}
