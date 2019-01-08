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
        private NewMoney _selectedRevenue;
        private List<NewMoney> _revenueList;
        public Database db;
        private int _sumRevenue;

        public int SumRevenue
        {
            get => _sumRevenue;
            set { SetProperty(ref _sumRevenue, value); }
        }

        public NewMoney SelectedRevenue
        {
            get => _selectedRevenue;
            set { SetProperty(ref _selectedRevenue, value); }
        }

        public List<NewMoney> RevenueList
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
            RevenueList = db.ListNewRevenue();
            ToAdditionPageCommand = new Command(ToAdditionPage);
            ToMoneyDetailPageCommand = new Command(ToMoneyDetailPage);
        }

        private async void ToMoneyDetailPage()
        {
            var navigationParams = new NavigationParameters();
            var SelectedMoney = new Money()
            {
                MoneyID = SelectedRevenue.MoneyID,
                MoneyName = SelectedRevenue.MoneyName,
                Date = SelectedRevenue.Date,
                Form = SelectedRevenue.Form,
                Category = SelectedRevenue.CategoryID,
                Cost = SelectedRevenue.Cost,
                Note = SelectedRevenue.Note,
                Image = SelectedRevenue.Image
            };
            navigationParams.Add("model", SelectedMoney);
            await NavigationService.NavigateAsync("MoneyDetailPage", navigationParams);

        }

        private async void ToAdditionPage()
        {
            await NavigationService.NavigateAsync("AdditionPage");
        }

    }
}
