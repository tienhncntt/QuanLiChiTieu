using Prism.Mvvm;
using System.Collections.ObjectModel;
using QuanLiChiTieu.Models;

namespace QuanLiChiTieu.ViewModels
{
	public class RevenueReportPageViewModel : BindableBase
	{
        private Database Db { get; set; }

        private ObservableCollection<ReportOutput> _reportOutputList;
        public ObservableCollection<ReportOutput> ReportOutputList
        {
            get => _reportOutputList;
            set => SetProperty(ref _reportOutputList, value);
        }

        private int _sumRevenue;
        public int SumRevenue
        {
            get => _sumRevenue;
            set => SetProperty(ref _sumRevenue, value);
        }

        public RevenueReportPageViewModel()
        {
            Db= new Database();
            SumRevenue = Db.SumRevenue();
            LoadData();
        }

        private void LoadData()
        {
            ReportOutputList = new ObservableCollection<ReportOutput>(Db.ListReport(1));
            for (var i = 0; i < ReportOutputList.Count ; i++)
            {
                var name = ReportOutputList[i].Name;
                for (var j = i + 1; j < ReportOutputList.Count; j++)
                {
                    if (ReportOutputList[j].Name == name)
                    {
                        ReportOutputList[i].Sum += ReportOutputList[j].Sum;
                        ReportOutputList.RemoveAt(j);
                        j--;
                    }
                }
            }
        }
	}
}
