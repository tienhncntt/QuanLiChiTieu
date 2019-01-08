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
        private NewMoney _selectedExpenditure;
        private List<NewMoney> _ExpenditureList;
        public Database db;
        private int _sumExpenditure;

        public int SumExpenditure
        {
            get => _sumExpenditure;
            set { SetProperty(ref _sumExpenditure, value); }
        }

        public NewMoney SelectedExpenditure
        {
            get => _selectedExpenditure;
            set { SetProperty(ref _selectedExpenditure, value); }
        }

        public List<NewMoney> ExpenditureList
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
            ExpenditureList = db.ListNewExpenditure();
            ToAdditionPageCommand = new Command(ToAdditionPage);
            ToMoneyDetailPageCommand = new Command(ToMoneyDetailPage);
        }

        private async void ToMoneyDetailPage()
        {
            var navigationParams = new NavigationParameters();
            var SelectedMoney = new Money()
            {
                MoneyID = SelectedExpenditure.MoneyID,
                MoneyName = SelectedExpenditure.MoneyName,
                Date = SelectedExpenditure.Date,
                Form = SelectedExpenditure.Form,
                Category = SelectedExpenditure.CategoryID,
                Cost = SelectedExpenditure.Cost,
                Note = SelectedExpenditure.Note,
                Image = SelectedExpenditure.Image
            };
            navigationParams.Add("model", SelectedMoney);
            await NavigationService.NavigateAsync("MoneyDetailPage", navigationParams);

        }

        private async void ToAdditionPage()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("form", 2);
            await NavigationService.NavigateAsync("AdditionPage", navigationParams);
        }

    }
}
