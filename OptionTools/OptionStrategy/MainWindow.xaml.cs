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

namespace OptionStrategy
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private class Item
		{
			//
			private string type;
			private int position;
			private double cost;

			//
			private double underlyingPrice;
			private int daysToExpiration;
			private double volatility;

			//
			private OptionTools.Option calc;

			public Item(string type, int position, double cost, double underlyingPrice, double strikePrice, int daysToExpiration, double volatility)
			{
				//
				this.type = type;
				this.position = position;
				this.cost = cost;

				//
				this.underlyingPrice = underlyingPrice;
				this.daysToExpiration = daysToExpiration;
				this.volatility = volatility;

				//
				calc = null;
				if ((type == "Call") || (type == "Put"))
				{
					calc = new OptionTools.Option();
					calc.Price = underlyingPrice;
					calc.Strike = strikePrice;
					calc.DaysToExpiration = daysToExpiration;
					calc.Volatility = volatility;
					calc.InterestRate = 0.05;
				}
			}

			public void SetUnderlyingPrice(double underlyingPrice)
			{
				this.underlyingPrice = underlyingPrice;
				if (calc != null)
				{
					calc.Price = underlyingPrice;
				}
			}

			public void SetDaysToExpiration(int daysToExpiration)
			{
				this.daysToExpiration = daysToExpiration;
				if (calc != null)
				{
					calc.DaysToExpiration = daysToExpiration;
				}
			}

			public void SetVolatility()
			{

			}

			public string Type
			{
				get
				{
					return type;
				}
			}

			public int Position
			{
				get
				{
					return position;
				}
			}

			public double Cost
			{
				get
				{
					return cost;
				}
			}

			public double Price
			{
				get
				{
					if (type == "Call") return calc.CallValue;
					else if (type == "Put") return calc.PutValue;
					else return underlyingPrice;
				}
			}

			public double Profit
			{
				get
				{
					return Position * (Price - Cost);
				}
			}

			public double Delta
			{
				get
				{
					if (type == "Call") return calc.CallDelta;
					else if (type == "Put") return calc.PutDelta;
					else return 1;
				}
			}

			public double Gamma
			{
				get
				{
					if (type == "Call") return calc.CallGamma;
					else if (type == "Put") return calc.PutGamma;
					return 0;
				}
			}

			public double Theta
			{
				get
				{
					if (type == "Call") return calc.CallTheta;
					else if (type == "Put") return calc.PutTheta;
					else return 0;
				}
			}

			public double Vega
			{
				get
				{
					if (type == "Call") return calc.CallVega;
					else if (type == "Put") return calc.PutVega;
					else return 0;
				}
			}

			public double Rho
			{
				get
				{
					if (type == "Call") return calc.CallRho;
					else if (type == "Put") return calc.PutRho;
					else return 0;
				}
			}

			public double Volatility
			{
				get
				{
					return volatility;
				}
			}

			public double VolatilityPercentage
			{
				get
				{
					return Volatility * 100.0f;
				}
				
			}
		}

		private List<Item> items;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//
			items = new List<Item>();
			items.Add(new Item("Underlying", 1, 100, 100, 0, 0, 0));
			items.Add(new Item("Call", 1, 2.12, 100, 110, 30, 0.2));
			items.Add(new Item("Call", 1, 2.1, 100, 110, 30, 0.2));
			items.Add(new Item("Put", -2, 1.13, 100, 110, 30, 0.2));

			//
			this.datagridOptions.ItemsSource = items;
		}

		private void sliderPrice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (items == null)
			{
				return;
			}
			for (int i = 0; i < items.Count; i++)
			{
				items[i].SetUnderlyingPrice(e.NewValue);
			}
			this.datagridOptions.Items.Refresh();
		}

		private void sliderDays_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (items == null)
			{
				return;
			}
			for (int i = 0; i < items.Count; i++)
			{
				items[i].SetDaysToExpiration((int)e.NewValue);
			}
			this.datagridOptions.Items.Refresh();
		}

		private void sliderVolatility_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
		}
	}
}

