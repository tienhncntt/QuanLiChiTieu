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
	public class MenuPageViewModel : BindableBase
    {
        private INavigationService _navigationService;
        public ICommand NavigationCommand { get; set; }
        public MenuPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigationCommand = new Command<string>(Navigate);
        }

        private async void Navigate(string page)
        {
            await _navigationService.NavigateAsync("NavigationPage/" + page);
        }
    }
}
