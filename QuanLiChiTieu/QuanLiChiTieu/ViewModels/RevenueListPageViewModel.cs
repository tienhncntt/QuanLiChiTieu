﻿using System.Collections.Generic;
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
        public Database db;
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

            db = new Database();
            SumRevenue = db.SumRevenue();
            RevenueList = db.ListRevenue();
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
            var navigationParams = new NavigationParameters();
            navigationParams.Add("form", 1);
            await NavigationService.NavigateAsync("AdditionPage", navigationParams);
        }

    }
}
