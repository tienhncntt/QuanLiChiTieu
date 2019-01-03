using Prism.Navigation;
using Xamarin.Forms;

namespace QuanLiChiTieu.Views
{
    public partial class MenuPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation { get { return false; } }
    }
}