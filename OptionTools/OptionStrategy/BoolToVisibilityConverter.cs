using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace OptionStrategy
{
	public class BoolToVisibilityConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null) { return null; }
			if (targetType == typeof(Visibility))
			{
				return (bool)value ? Visibility.Visible : Visibility.Collapsed;
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null) { return null; }
			if (targetType == typeof(Visibility))
			{
				return (bool)value ? Visibility.Visible : Visibility.Collapsed;
			}
			return null;
		}

		#endregion
	}
}

