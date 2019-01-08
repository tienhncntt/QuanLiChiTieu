using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
            set => SetProperty(ref _newMoney, value);
        }

        private Form _form;
        public Form Form
        {
            get => _form;
            set
            {
                SetProperty(ref _form, value);
                if (_form.FormID == 1 || _form.FormID == 2)
                {
                    Categories = db.ListCategories(_form.FormID);
                }
            }
        }

        private List<Form> _forms;
        public List<Form> Forms
        {
            get => _forms;
            set => SetProperty(ref _forms, value);
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

        private ImageSource source;
        public ImageSource Source
        {
            get { return source; }
            set { SetProperty(ref source, value, "Source"); }
        }

        public ICommand AddNewCommand { get; set; }
        public ICommand ClearDataCommand { get; set; }
        public ICommand TakePictureCommand { get; set; }
        public ICommand PickPictureCommand { get; set; }

        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        #endregion

        public AdditionPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            NewMoney = new Money(){Date = DateTime.Now};
            Forms = new List<Form>();
            Categories = new List<Category>();
            Form = new Form(); 
            Category = new Category();

            TakePictureCommand = new Command(TakePicture);
            PickPictureCommand = new Command(PickPicture);
            AddNewCommand = new Command<Money>(AddNew);
            ClearDataCommand = new Command(ClearData);
            db = new Database();
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            LoadData();
        }

        private void ClearData()
        {
            NewMoney = new Money(){ Date = DateTime.Now };
            Category = new Category();
            Form = new Form();
            Source = new StreamImageSource();
        }

        private void LoadData()
        {
            
            
            //Categories = db.ListCategories(1);
            Forms = db.ListForms();
        }

        private void AddNew(Money money)
        {
            NewMoney.Form = Form.FormID;
            NewMoney.Category = Category.CategoryID;
            //db.Insert(NewMoney);
        }

        private async void TakePicture()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await _pageDialogService.DisplayAlertAsync("Notification", "No camera available", "OK");
                return;
            }

            var options = new StoreCameraMediaOptions();
            options.SaveToAlbum = true;
            options.PhotoSize = PhotoSize.Small;

            var file = await CrossMedia.Current.TakePhotoAsync(
                options);

            if (file == null)
                return;
            Source = ImageSource.FromStream(() =>
            {
                System.IO.Stream stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        private async void PickPicture()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await _pageDialogService.DisplayAlertAsync("Notification", "Pick Photo is not supprted", "OK");
                return;
            }


            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;
            Source = ImageSource.FromStream(() =>
            {
                System.IO.Stream stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }


    }
}
