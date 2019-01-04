using Prism;
using Prism.Ioc;
using QuanLiChiTieu.ViewModels;
using QuanLiChiTieu.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace QuanLiChiTieu
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //await NavigationService.NavigateAsync("MenuPage/NavigationPage/MoneyTabbedPage");
            await NavigationService.NavigateAsync("MenuPage/NavigationPage/MoneyTabbedPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<RevenueListPage, RevenueListPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
            containerRegistry.RegisterForNavigation<ReportTabbedPage, ReportTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<RevenueReportPage, RevenueReportPageViewModel>();
            containerRegistry.RegisterForNavigation<ExpenditureReportPage, ExpenditureReportPageViewModel>();
            containerRegistry.RegisterForNavigation<GenaralReportPage, GenaralReportPageViewModel>();
            containerRegistry.RegisterForNavigation<ExpenditureListPage, ExpenditureListPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingPage, SettingPageViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutPageViewModel>();
            containerRegistry.RegisterForNavigation<MoneyDetailPage, MoneyDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<AdditionPage, AdditionPageViewModel>();
            containerRegistry.RegisterForNavigation<MoneyTabbedPage, MoneyTabbedPageViewModel>();
        }
    }
}
