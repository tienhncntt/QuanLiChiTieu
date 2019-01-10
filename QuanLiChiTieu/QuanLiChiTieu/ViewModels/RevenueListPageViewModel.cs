using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Navigation;
using QuanLiChiTieu.Models;
using Xamarin.Forms;

namespace QuanLiChiTieu.ViewModels
{
    public class RevenueListPageViewModel : ViewModelBase
    {
        private NewMoney _selectedRevenue;
        private ObservableCollection<NewMoney> _revenueList;
        public Database db;
        private string _revenueName;
        public string RevenueName
        {
            get => _revenueName;
            set => SetProperty(ref _revenueName, value);
        }
        private int _sumRevenue;

        public int SumRevenue
        {
            get => _sumRevenue;
            set => SetProperty(ref _sumRevenue, value);
        }

        public NewMoney SelectedRevenue
        {
            get => _selectedRevenue;
            set => SetProperty(ref _selectedRevenue, value);
        }

        public ObservableCollection<NewMoney> RevenueList
        {
            get => _revenueList;
            set => SetProperty(ref _revenueList, value);
        }

        public ICommand ToAdditionPageCommand { get; set; }
        public ICommand ToMoneyDetailPageCommand { get; set; }
        public  ICommand SearchCommand { get; set; }

        public RevenueListPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            db = new Database();
            SumRevenue = db.SumRevenue();
            RevenueList = new ObservableCollection<NewMoney>(db.ListNewRevenue());
            ToAdditionPageCommand = new Command(ToAdditionPage);
            ToMoneyDetailPageCommand = new Command(ToMoneyDetailPage);
            SearchCommand = new Command(Search);
        }

        private void Search()
        {
            RevenueList = new ObservableCollection<NewMoney>(db.Search(RevenueName, 1));
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
            var navigationParams = new NavigationParameters();
            navigationParams.Add("form", 1);
            await NavigationService.NavigateAsync("AdditionPage", navigationParams);
        }

    }
}
