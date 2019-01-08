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
    public class ExpenditureListPageViewModel : ViewModelBase
    {
        private Money _selectedExpenditure;
        private List<Money> _ExpenditureList;
        public Database db;
        private int _sumExpenditure;

        public int SumExpenditure
        {
            get => _sumExpenditure;
            set { SetProperty(ref _sumExpenditure, value); }
        }

        public Money SelectedExpenditure
        {
            get => _selectedExpenditure;
            set { SetProperty(ref _selectedExpenditure, value); }
        }

        public List<Money> ExpenditureList
        {
            get => _ExpenditureList;
            set { SetProperty(ref _ExpenditureList, value); }
        }

        public ICommand ToAdditionPageCommand { get; set; }
        public ICommand ToMoneyDetailPageCommand { get; set; }

        public ExpenditureListPageViewModel(INavigationService navigationService) : base(navigationService)
        {

            db = new Database();
            SumExpenditure = db.SumExpenditure();
            ExpenditureList = db.ListExpenditure();
            ToAdditionPageCommand = new Command(ToAdditionPage);
            ToMoneyDetailPageCommand = new Command(ToMoneyDetailPage);

        }

        private async void ToMoneyDetailPage()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("model", SelectedExpenditure);
            await NavigationService.NavigateAsync("MoneyDetailPage", navigationParams);

        }

        private async void ToAdditionPage()
        {
            await NavigationService.NavigateAsync("AdditionPage");
        }

    }
}
