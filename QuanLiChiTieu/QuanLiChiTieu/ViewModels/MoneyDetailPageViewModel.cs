using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ImTools;
using QuanLiChiTieu.Models;
using Xamarin.Forms;
using QuanLiChiTieu.Views;
using Xamarin.Forms.Xaml;

namespace QuanLiChiTieu.ViewModels
{
    public class MoneyDetailPageViewModel : ViewModelBase
    {
        #region Khai báo

        public Database db;
        private INavigationService _navigationService;

        private Category _category;
        public Category Category
        {
            get => _category;
            set { SetProperty(ref _category, value); }
        }

        private string _form;
        public string Form
        {
            get => _form;
            set
            {
                SetProperty(ref _form, value);
            }
        }

        private List<Category> _categories;
        public List<Category> Categories
        {
            get => _categories;
            set { SetProperty(ref _categories, value); }
        }

        private Money _money;
        public Money SelectedRevenue
        {
            get => _money;
            set { SetProperty(ref _money, value); }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set { SetProperty(ref _name, value); }
        }

        private int _formID;
        public int FormID
        {
            get => _formID;
            set
            {
                SetProperty(ref _formID, value);
            }
        }

        private int _categoryID;
        public int CategoryID
        {
            get => _categoryID;
            set
            {
                SetProperty(ref _categoryID, value);
            }
        }

        private int _cost;
        public int Cost
        {
            get => _cost;
            set { SetProperty(ref _cost, value); }
        }
        private Color _color;

        public Color Color
        {
            get => _color;
            set { SetProperty(ref _color, value); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set { SetProperty(ref _date, value); }
        }

        private string _note;
        public string Note
        {
            get => _note;
            set { SetProperty(ref _note, value); }
        }

        public Command Save { get; set; }
        public Command Delete { get; set; }
        #endregion

        public MoneyDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            db = new Database();
            Save = new Command(SaveExecute);
            Delete = new Command(DeleteExecute);
            //Forms = new List<Form>();
            //Categories = new List<Category>();
            Category = new Category();
            _navigationService = navigationService;
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            SelectedRevenue = parameters["model"] as Money;
            AssignValue();
        }

        private void AssignValue()
        {
            Name = SelectedRevenue.MoneyName;
            Date = SelectedRevenue.Date;
            Cost = SelectedRevenue.Cost;
            Note = SelectedRevenue.Note;
            FormID = SelectedRevenue.Form;
            CategoryID = SelectedRevenue.Category - 1;
            if (FormID == 1)
            {
                Form = "Thu";
                Color = Color.FromHex("#41C09B");
            }
            else
            {
                Form = "Chi";
                Color = Color.Red;
            }

            Categories = db.ListCategories(FormID);
            Category = db.SelectedCategory(CategoryID + 1);
        }


        private async void SaveExecute()
        {
            UpdateRevenue();
            new RevenueListPage();
            await NavigationService.GoBackAsync();
        }

        private async void DeleteExecute()
        {
            db.Delete(SelectedRevenue);
            await NavigationService.GoBackAsync();
        }

        private void UpdateRevenue()
        {
            SelectedRevenue.MoneyName = Name;
            SelectedRevenue.Date = Date;
            SelectedRevenue.Cost = Cost;
            SelectedRevenue.Note = Note;
            db.Update(SelectedRevenue);
        }
    }
}
