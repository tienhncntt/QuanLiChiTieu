using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using QuanLiChiTieu.Models;
using Xamarin.Forms;

namespace QuanLiChiTieu.ViewModels
{
	public class AdditionPageViewModel : ViewModelBase
    {
         #region Khai bao
        private Database db;

        private Money _newMoney;
        public Money NewMoney
        {
            get => _newMoney;
            set => SetProperty(ref _newMoney, value);
        }

        private string _newMoneyName;
        public string NewMoneyName
        {
            get => _newMoneyName;
            set => SetProperty(ref _newMoneyName, value);
        }

        private int _newMoneyCost;
        public int NewMoneyCost
        {
            get => _newMoneyCost;
            set => SetProperty(ref _newMoneyCost, value);
        }

        private string _form;
        public string Form
        {
            get => _form;
            set => SetProperty(ref _form, value);
        }

        private Color _color;
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        private int _paramId;
        public int ParamId
        {
            get => _paramId;
            set => SetProperty(ref _paramId, value);
        }

        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private Category _category;
        public Category Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }
        private ImageSource _source;
        public ImageSource Source
        {
            get { return _source; }
            set => SetProperty(ref _source, value);
        }

        private byte[] _imageAsBytes { get; set; }

        public ICommand AddNewCommand { get; set; }
        public ICommand ClearDataCommand { get; set; }
        public ICommand TakePictureCommand { get; set; }
        public ICommand PickPictureCommand { get; set; }

        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        #endregion

        public AdditionPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            NewMoney = new Money(){Date = DateTime.Now};
            Categories = new ObservableCollection<Category>();
            Category = new Category();
            Color = new Color();
            TakePictureCommand = new Command(TakePicture);
            PickPictureCommand = new Command(PickPicture);
            AddNewCommand = new DelegateCommand(AddNew, CanExe).ObservesProperty((() => Category))
                .ObservesProperty((() => NewMoneyName)).ObservesProperty((() => NewMoneyCost));
            ClearDataCommand = new Command(ClearData);
            db = new Database();
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
        }
        private bool CanExe()
        {
            if (Category != null && NewMoneyCost > 0 && !string.IsNullOrEmpty(NewMoneyName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ClearData()
        {
            NewMoney = new Money(){ Date = DateTime.Now };
            Category = new Category();
            Source = new StreamImageSource();
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            ParamId = parameters.GetValue<int>("form");
            LoadData();
        }

        private void LoadData()
        {
            if (ParamId == 1)
            {
                Form = "Thu";
                Color = Color.FromHex("#41C09B");
                Categories = new ObservableCollection<Category>(db.ListCategories(ParamId));


            }
            else
            {
                Form = "Chi";
                Color = Color.Red;
                Categories = new ObservableCollection<Category>(db.ListCategories(ParamId));
            }
        }

        private void AddNew()
        {
            NewMoney.Form = ParamId;
            NewMoney.Category = Category.CategoryID;
            NewMoney.MoneyName = NewMoneyName;
            NewMoney.Cost = NewMoneyCost;
            NewMoney.Image = _imageAsBytes;
            db.Insert(NewMoney);
            _pageDialogService.DisplayAlertAsync("Thông báo", "Thêm thành công", "OK");
            _navigationService.GoBackAsync();
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
                var stream = file.GetStream();
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    _imageAsBytes = memoryStream.ToArray();
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
                var stream = file.GetStream();
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    _imageAsBytes = memoryStream.ToArray();
                }
                return stream;
            });

        }
    }
}
