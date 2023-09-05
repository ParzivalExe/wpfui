using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFUI.Converters
{
    public class SizeToCornerRadiusConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double width = -1;
            double height = -1;
            double minRadius = -1;
            Thickness margin = new Thickness(0);
            foreach(object value in values)
            {
                if (value is Thickness)
                    margin = (Thickness)value;
                else if (value is double && width == -1)
                    width = (double)value;
                else if (value is double && height == -1)
                    height = (double)value;
                else if(value is double)
                    throw new ArgumentException($"Both width and height have already been set. A third double is unnecessary");
            }

            if (width < 0)
                width = 0;
            if (height < 0)
                height = 0;

            if(parameter is string @string)
                double.TryParse(@string, NumberStyles.Any, CultureInfo.InvariantCulture, out minRadius);

            if (width <= height)
            {
                double round = (width - (margin.Left + margin.Right)) / 2.0;
                if(round > minRadius) 
                    return new CornerRadius(round);
            }
            else
            {
                double round = (height - (margin.Top + margin.Bottom)) / 2.0;
                if (round > minRadius)
                    return new CornerRadius(round);
            }
            return new CornerRadius(0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
