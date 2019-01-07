using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using QuanLiChiTieu.Models;
using Xamarin.Forms;
using QuanLiChiTieu.Views;
using Xamarin.Forms.Xaml;

namespace QuanLiChiTieu.ViewModels
{
    public class MoneyDetailPageViewModel : ViewModelBase
    {
        private string _name;
        private List<Form> _forms;
        private List<Category> _categories;
        private int _cost;
        private DateTime _date;
        private string _note;
        private Money _money;
        public Form form;
        public Database Database;
        private Form _index;
        private INavigationService _navigationService;

        public Form Index
        {
            get => _index;
            set { SetProperty(ref _index, value); }
        }

        public int FormID { get; set; }
        public Money SelectedRevenue
        {
            get => _money;
            set { SetProperty(ref _money, value); }
        }

        public string Name
        {
            get => _name;
            set { SetProperty(ref _name, value); }
        }

        public List<Form> Forms
        {
            get => _forms;
            set { SetProperty(ref _forms, value); }
        }

        public List<Category> Categories
        {
            get => _categories;
            set { SetProperty(ref _categories, value); }
        }

        public int Cost
        {
            get => _cost;
            set { SetProperty(ref _cost, value); }
        }

        public DateTime Date
        {
            get => _date;
            set { SetProperty(ref _date, value); }
        }

        public string Note
        {
            get => _note;
            set { SetProperty(ref _note, value); }
        }

        public Command Save { get; set; }

        public MoneyDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            //form = new Form();
            //AssignValue();
            Database = new Database();
            Save = new Command(SaveExecute);
            _navigationService = navigationService;
            //Forms = Database.ListForms();
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            SelectedRevenue = parameters["model"] as Money;
            Name = SelectedRevenue.MoneyName;
            Date = SelectedRevenue.Date;
            Cost = SelectedRevenue.Cost;
            Note = SelectedRevenue.Note;
            FormID = SelectedRevenue.Form;
        }
        private async void SaveExecute()
        {
            SelectedRevenue.MoneyName = Name;
            SelectedRevenue.Date = Date;
            SelectedRevenue.Cost = Cost;
            SelectedRevenue.Note = Note;
            Database.Update(SelectedRevenue);
            await NavigationService.GoBackToRootAsync();
            //if (FormID == 1)
            //    NavigationService.NavigateAsync("RevenueListPage");
            //else
            //    NavigationService.NavigateAsync("ExpenditureListPage");
        }
    }
}
