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
    public class RevenueListPageViewModel : ViewModelBase
    {
        private Money _selectedRevenue;
        private List<Money> _revenueList;
        public Database Database;
        private int _sumRevenue;

        public int SumRevenue
        {
            get => _sumRevenue;
            set { SetProperty(ref _sumRevenue, value); }
        }

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

        public RevenueListPageViewModel(INavigationService navigationService) : base(navigationService)
        {

            Database = new Database();
            SumRevenue = Database.SumRevenue();
            RevenueList = Database.ListRevenue();
            ToAdditionPageCommand = new Command(ToAdditionPage);
            ToMoneyDetailPageCommand = new Command(ToMoneyDetailPage);
            
        }

        private async void ToMoneyDetailPage()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("model", SelectedRevenue);
            await NavigationService.NavigateAsync("MoneyDetailPage", navigationParams);

        }

        private async void ToAdditionPage()
        {
            await NavigationService.NavigateAsync("AdditionPage");
        }

    }
}
