using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace QuanLiChiTieu.ViewModels
{
	public class RevenueListPageViewModel : BindableBase
	{
        public  ICommand ToAdditionPageCommand { get; set; }
        public ICommand ToMoneyDetailPageCommand { get; set; }
        private INavigationService _navigationService;
        public RevenueListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ToAdditionPageCommand = new Command(ToAdditionPage);
            ToMoneyDetailPageCommand = new Command(ToMoneyDetailPage);
        }

        private async void ToMoneyDetailPage()
        {
            await _navigationService.NavigateAsync("MoneyDetailPage");
        }

        private async void ToAdditionPage()
        {
            await _navigationService.NavigateAsync("AdditionPage");
        }
    }
}
