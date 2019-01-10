using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microcharts;
using Prism.Services;
using QuanLiChiTieu.Models;
using SkiaSharp;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace QuanLiChiTieu.ViewModels
{
    public class GenaralReportPageViewModel : ViewModelBase
    {
        #region Khai báo
        private DateTime _fromdate;

        public DateTime FromDate
        {
            get => _fromdate;
            set => SetProperty(ref _fromdate, value);
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private DateTime _minTime;
        public DateTime MinTime
        {
            get => _minTime;
            set => SetProperty(ref _minTime, value);
        }

        private DateTime _maxTime;
        public DateTime MaxTime
        {
            get => _maxTime;
            set => SetProperty(ref _maxTime, value);
        }

        private DateTime _todate;
        private DateTime _date;

        public DateTime ToDate
        {
            get => _todate;
            set => SetProperty(ref _todate, value);
        }

        private int _sumRevenue;
        public int SumRevenue
        {
            get => _sumRevenue;
            set => SetProperty(ref _sumRevenue, value);
        }

        private int _sumExpenditure;
        public int SumExpenditure
        {
            get => _sumExpenditure;
            set => SetProperty(ref _sumExpenditure, value);
        }

        public Chart ReportChart
        {
            get => _reportChart;
            set => SetProperty(ref _reportChart, value);
        }

        public Database db;
        private Chart _reportChart;
        public ICommand ShowChartCommand { get; set; }
        public ICommand SetMinCommand { get; set; }
        public ICommand SetMaxCommand { get; set; }
        private IPageDialogService _pageDialogService;
        #endregion
        public GenaralReportPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            db = new Database();
            AssignValue();
            _pageDialogService = pageDialogService;
            MinTime = FromDate;
            MaxTime = ToDate;
            ShowChartCommand = new Command(ShowChart);
            SetMinCommand = new Command(SetMin);
            SetMaxCommand = new Command(SetMax);
            ShowChart();
        }

        private void SetMin()
        {
            MinTime = FromDate;
            ShowChart();
        }
        private void SetMax()
        {
            MaxTime = ToDate;
            ShowChart();
        }

        private void ShowChart()
        {
            SumRevenue = db.FilterRevenue(FromDate, ToDate);
            SumExpenditure = db.FilterExpenditure(FromDate, ToDate);
            if (SumExpenditure == 0 && SumRevenue == 0)
            {
                _pageDialogService.DisplayAlertAsync("Thông báo", "Không có dữ liệu", "OK");
            }
            var entries = new[]
            {
                new Entry(SumRevenue)
                {
                    Color = SKColor.Parse("#41C09B")
                },
                new Entry(SumExpenditure)
                {
                    Color = SKColor.Parse("#FF0000")
                }};
            ReportChart = new DonutChart() { Entries = entries };
        }

       
        private void AssignValue()
        {
            var thisMonth = DateTime.Now.Month;
            var thisYear = DateTime.Now.Year;
            var dayInThisMonth = DateTime.DaysInMonth(thisYear, thisMonth);
            FromDate = new DateTime(thisYear, thisMonth, 1);
            ToDate = new DateTime(thisYear, thisMonth, dayInThisMonth);
            //SumRevenue = db.FilterRevenue(FromDate, ToDate);
            //SumExpenditure = db.FilterExpenditure(FromDate, ToDate);
        }
    }
}