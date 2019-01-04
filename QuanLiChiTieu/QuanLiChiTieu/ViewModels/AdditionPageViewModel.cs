using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using QuanLiChiTieu.Models;
using Xamarin.Forms;

namespace QuanLiChiTieu.ViewModels
{
	public class AdditionPageViewModel : BindableBase
    {
         #region Khai bao
        private Database db;
        private Money _newMoney;
        public Money NewMoney
        {
            get => _newMoney;
            set
            {
                SetProperty(ref _newMoney, value);
                //Categories = db.ListCategories(NewMoney.Form);
            }
        }

        private Form _form;
        public Form Form
        {
            get => _form;
            set
            {
                SetProperty(ref _form, value);
            }
        }

        public string FormName
        {
            get => _form.FormName;
        }

        private List<Form> _forms;
        public List<Form> Forms
        {
            get => _forms;
            set
            {
                SetProperty(ref _forms, value);               
            }
        }
        private List<Category> _categories;
        public List<Category> Categories
        {
            get => _categories;
            set { SetProperty(ref _categories, value); }
        }

        private Category _category;
        public Category Category
        {
            get => _category;
            set { SetProperty(ref _category, value); }
        }

        public string CategoryName
        {
            get => _category.CategoryName;
        }

        public ICommand AddNewCommand { get; set; }
        private INavigationService _navigationService;
        #endregion

        public AdditionPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            NewMoney = new Money();
            Forms = new List<Form>();
            Categories = new List<Category>();
            Form = new Form(); 
            Category = new Category();
            AddNewCommand = new Command<Money>(AddNew);
            db = new Database();
            _navigationService = navigationService;
            LoadData();
        }

        private void LoadData()
        {
            Forms = db.ListForms();
            Categories = db.ListCategories(1);
        }

        private void AddNew(Money money)
        {
            
        }


    }
}
