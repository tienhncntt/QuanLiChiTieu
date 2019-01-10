using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using QuanLiChiTieu.Models;

namespace QuanLiChiTieu.ViewModels
{
	public class ExpenditureReportPageViewModel : BindableBase
	{
        private Database db { get; set; }

        private ObservableCollection<ReportOutput> _reportOutputList;
        public ObservableCollection<ReportOutput> ReportOutputList
        {
            get => _reportOutputList;
            set => SetProperty(ref _reportOutputList, value);
        }

        private int _sumExpenditure;
        public int SumExpenditure
        {
            get => _sumExpenditure;
            set => SetProperty(ref _sumExpenditure, value);
        }

        public ExpenditureReportPageViewModel()
        {
            db = new Database();
            SumExpenditure = db.SumExpenditure();
            LoadData();
        }

        private void LoadData()
        {
            ReportOutputList = new ObservableCollection<ReportOutput>();
            ReportOutputList = new ObservableCollection<ReportOutput>(db.ListReport(2));
            for (var i = 0; i < ReportOutputList.Count; i++)
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
