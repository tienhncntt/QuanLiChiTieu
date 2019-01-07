using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Text;
using QuanLiChiTieu.Models;
using Xamarin.Forms;

namespace QuanLiChiTieu.Converter
{
    class ToColorConverter : IValueConverter
    {
        private Color color = new Color();
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 1)
                return color = Color.Red;
            else if ((int)value == 2)
            {
                return color = Color.FromHex("41C09B");
            }
            else
            {
                return color = Color.Black;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
