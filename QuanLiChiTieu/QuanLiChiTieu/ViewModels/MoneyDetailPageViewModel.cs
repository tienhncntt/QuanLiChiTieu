using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            set => SetProperty(ref _category, value);
        }

        private string _form;
        public string Form
        {
            get => _form;
            set => SetProperty(ref _form, value);
        }

        private byte[] imageAsBytes;
        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private Money _money;
        public Money SelectedMoney
        {
            get => _money;
            set => SetProperty(ref _money, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private int _formID;
        public int FormID
        {
            get => _formID;
            set => SetProperty(ref _formID, value);
        }

        private int _categoryID;
        public int CategoryID
        {
            get => _categoryID;
            set => SetProperty(ref _categoryID, value);
        }

        private int _cost;
        public int Cost
        {
            get => _cost;
            set => SetProperty(ref _cost, value);
        }
        private Color _color;

        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private string _note;
        private string _titleCategory;
        private ImageSource source;

        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }

        public string TitleCategory
        {
            get => _titleCategory;
            set => SetProperty(ref _titleCategory, value);
        }

        public ImageSource Source
        {
            get => source;
            set => SetProperty(ref source, value);
        }

        public ICommand Save { get; set; }
        public Command Delete { get; set; }
        public ICommand TakePictureCommand { get; set; }
        public ICommand PickPictureCommand { get; set; }
        #endregion

        public MoneyDetailPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            db = new Database();
            Save = new DelegateCommand(SaveExecute,CanExe).ObservesProperty((() => Cost));
            Delete = new Command(DeleteExecute);
            TakePictureCommand = new Command(TakePicture);
            PickPictureCommand = new Command(PickPicture);
            //Category = new Category();
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
        }
        private bool CanExe()
        {
            if (Cost > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            SelectedMoney = parameters["model"] as Money;
            AssignValue();
        }

        private void AssignValue()
        {
            Name = SelectedMoney.MoneyName;
            Date = SelectedMoney.Date;
            Cost = SelectedMoney.Cost;
            Note = SelectedMoney.Note;
            FormID = SelectedMoney.Form;
            Category = db.SelectedCategory(SelectedMoney.Category);
            Categories = new ObservableCollection<Category>(db.ListCategories(FormID)); 
            //TitleCategory = Category.CategoryName;
            Source = ImageSource.FromStream(() => new MemoryStream(SelectedMoney.Image));
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
            db.Delete(SelectedMoney);
            _pageDialogService.DisplayAlertAsync("Thông báo", "Xoá thành công", "OK");
            await NavigationService.GoBackAsync();
        }

        private void UpdateRevenue()
        {
            SelectedMoney.MoneyName = Name;
            SelectedMoney.Date = Date;
            if (Cost < 0)
            {
                Cost *= -1;
            }
            SelectedMoney.Cost = Cost;
            SelectedMoney.Note = Note;
            SelectedMoney.Category = Category.CategoryID;
            //SelectedRevenue.Image = imageAsBytes;
            db.Update(SelectedMoney);
            _pageDialogService.DisplayAlertAsync("Thông báo", "Cập nhật thành công", "OK");
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
                    SelectedMoney.Image = imageAsBytes;
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
                    SelectedMoney.Image = imageAsBytes;
                }
                return stream;
            });

        }
    }
}
