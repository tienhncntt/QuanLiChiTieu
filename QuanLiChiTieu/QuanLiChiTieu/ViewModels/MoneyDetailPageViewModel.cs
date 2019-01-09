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
using System.IO;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Services;

namespace QuanLiChiTieu.ViewModels
{
    public class MoneyDetailPageViewModel : ViewModelBase
    {
        #region Khai báo

        public Database db;
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;

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

        private byte[] imageAsBytes;
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
        private string _titleCategory;
        private ImageSource source;

        public string Note
        {
            get => _note;
            set { SetProperty(ref _note, value); }
        }

        public string TitleCategory
        {
            get => _titleCategory;
            set { SetProperty(ref _titleCategory, value); }
        }

        public ImageSource Source
        {
            get => source;
            set { SetProperty(ref source, value); }
        }

        public Command Save { get; set; }
        public Command Delete { get; set; }
        public ICommand TakePictureCommand { get; set; }
        public ICommand PickPictureCommand { get; set; }
        #endregion

        public MoneyDetailPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            db = new Database();
            Save = new Command(SaveExecute);
            Delete = new Command(DeleteExecute);
            TakePictureCommand = new Command(TakePicture);
            PickPictureCommand = new Command(PickPicture);
            //Category = new Category();
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
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
            Category = db.SelectedCategory(FormID);
            Categories = db.ListCategories(FormID);
            //TitleCategory = Category.CategoryName;
            Source = ImageSource.FromStream(() => new MemoryStream(SelectedRevenue.Image));
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
            
        }


        private async void SaveExecute()
        {
            UpdateRevenue();
            await NavigationService.GoBackAsync();
        }

        private async void DeleteExecute()
        {
            db.Delete(SelectedRevenue);
            await NavigationService.NavigateAsync("RevenueListPage");
        }

        private void UpdateRevenue()
        {
            SelectedRevenue.MoneyName = Name;
            SelectedRevenue.Date = Date;
            SelectedRevenue.Cost = Cost;
            SelectedRevenue.Note = Note;
            SelectedRevenue.Category = Category.CategoryID;
            SelectedRevenue.Image = imageAsBytes;
            db.Update(SelectedRevenue);
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
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    imageAsBytes = memoryStream.ToArray();
                }
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
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    imageAsBytes = memoryStream.ToArray();
                }
                return stream;
            });

        }
    }
}
