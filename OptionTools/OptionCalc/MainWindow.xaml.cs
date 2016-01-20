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
			double price = 0;
			if (double.TryParse(this.textPrice.Text, out price))
			{
				option.Price = price;
				optionUpdate();
			}
			else
			{
				this.textPrice.Text = option.Price.ToString();
				this.textPrice.SelectionStart = this.textPrice.Text.Length;
			}
		}

		private void textStrike_TextChanged(object sender, TextChangedEventArgs e)
		{
			double strike = 0;
			if (double.TryParse(this.textStrike.Text, out strike))
			{
				option.Strike = strike;
				optionUpdate();
			}
			else
			{
				this.textStrike.Text = option.Strike.ToString();
				this.textStrike.SelectionStart = this.textStrike.Text.Length;
			}
		}

		private void textDaysToExpiration_TextChanged(object sender, TextChangedEventArgs e)
		{
			int daysToExpiration = 0;
			if (int.TryParse(this.textDaysToExpiration.Text, out daysToExpiration))
			{
				if (daysToExpiration >= 1)
				{
					option.DaysToExpiration = daysToExpiration;
					optionUpdate();
				}
				else
				{
					option.DaysToExpiration = 1;
					optionUpdate();

					this.textDaysToExpiration.Text = option.DaysToExpiration.ToString();
					this.textDaysToExpiration.SelectionStart = this.textDaysToExpiration.Text.Length;
				}
			}
			else
			{
				this.textDaysToExpiration.Text = option.DaysToExpiration.ToString();
				this.textDaysToExpiration.SelectionStart = this.textDaysToExpiration.Text.Length;
			}
		}

		private void textVolatility_TextChanged(object sender, TextChangedEventArgs e)
		{
			double volatility = 0;
			if (double.TryParse(this.textVolatility.Text, out volatility))
			{
				option.Volatility = volatility;
				optionUpdate();
			}
			else
			{
				this.textVolatility.Text = option.Volatility.ToString();
				this.textVolatility.SelectionStart = this.textVolatility.Text.Length;
			}
		}

		private void textInterestRate_TextChanged(object sender, TextChangedEventArgs e)
		{
			double interestRate = 0;
			if (double.TryParse(this.textInterestRate.Text, out interestRate))
			{
				option.InterestRate = interestRate;
				optionUpdate();
			}
			else
			{
				this.textInterestRate.Text = option.InterestRate.ToString();
				this.textInterestRate.SelectionStart = this.textInterestRate.Text.Length;
			}
		}

		private void optionUpdate()
		{
			if (this.textCallValue != null) this.textCallValue.Text = option.CallValue.ToString("F3");
			if (this.textCallDelta != null) this.textCallDelta.Text = option.CallDelta.ToString("F3");
			if (this.textCallGamma != null) this.textCallGamma.Text = option.CallGamma.ToString("F3");
			if (this.textCallTheta != null) this.textCallTheta.Text = option.CallTheta.ToString("F3");
			if (this.textCallVega != null) this.textCallVega.Text = option.CallVega.ToString("F3");
			if (this.textCallRho != null) this.textCallRho.Text = option.CallRho.ToString("F3");

			if (this.textPutValue != null) this.textPutValue.Text = option.PutValue.ToString("F3");
			if (this.textPutDelta != null) this.textPutDelta.Text = option.PutDelta.ToString("F3");
			if (this.textPutGamma != null) this.textPutGamma.Text = option.PutGamma.ToString("F3");
			if (this.textPutTheta != null) this.textPutTheta.Text = option.PutTheta.ToString("F3");
			if (this.textPutVega != null) this.textPutVega.Text = option.PutVega.ToString("F3");
			if (this.textPutRho != null) this.textPutRho.Text = option.PutRho.ToString("F3");
		}

		private void Grid_Initialized(object sender, EventArgs e)
		{
			optionUpdate();
		}
	}
}

