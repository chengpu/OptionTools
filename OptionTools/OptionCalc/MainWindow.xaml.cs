using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OptionCalc
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private OptionTools.Option option;
		public MainWindow()
		{
			//
			option = new OptionTools.Option();

			//
			InitializeComponent();
		}

		private void textPrice_TextChanged(object sender, TextChangedEventArgs e)
		{
			//option.Price = double.Parse(this.textPrice.Text);
		}

		private void textStrike_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void textDaysToExpiration_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void textVolatility_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void textInterestRate_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void updateOption()
		{

		}
	}
}
