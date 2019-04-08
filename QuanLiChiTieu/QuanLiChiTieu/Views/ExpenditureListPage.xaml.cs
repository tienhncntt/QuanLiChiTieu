using QuanLiChiTieu.ViewModels;
using Xamarin.Forms;

namespace QuanLiChiTieu.Views
{
    public partial class ExpenditureListPage : ContentPage
    {
        public ExpenditureListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            (BindingContext as ExpenditureListPageViewModel)?.ReLoad();
        }
    }
}
