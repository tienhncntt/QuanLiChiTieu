using Prism.Navigation;
using QuanLiChiTieu.ViewModels;
using Xamarin.Forms;

namespace QuanLiChiTieu.Views
{
    public partial class RevenueListPage : ContentPage
    {
        public RevenueListPage()
        {
            InitializeComponent();
            //BindingContext = new RevenueListPageViewModel();
        }
        protected override void OnAppearing()
        {
            (BindingContext as RevenueListPageViewModel)?.ReLoad();
        }

    }
}
