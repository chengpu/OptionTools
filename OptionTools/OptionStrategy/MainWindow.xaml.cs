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
			private string type;
			private int position;
			private double cost;
			private OptionTools.Option calc;

			public Item(string type, int position, double cost)
			{
				//
				this.type = type;
				this.position = position;
				this.cost = cost;

				//
				calc = new OptionTools.Option();
				calc.Price = 100;
				calc.Strike = 110;
				calc.DaysToExpiration = 30;
				calc.Volatility = 0.25;
				calc.InterestRate = 0.05;
			}

			public void SetUnderlyingPrice()
			{

			}

			public void SetDaysToExpiration()
			{

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
					else return calc.PutValue;
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
					else return calc.PutDelta;
				}
			}

			public double Gamma
			{
				get
				{
					if (type == "Call") return calc.CallGamma;
					else return calc.PutGamma;
				}
			}

			public double Theta
			{
				get
				{
					if (type == "Call") return calc.CallTheta;
					else return calc.PutTheta;
				}
			}

			public double Vega
			{
				get
				{
					if (type == "Call") return calc.CallVega;
					else return calc.PutVega;
				}
			}

			public double Rho
			{
				get
				{
					if (type == "Call") return calc.CallRho;
					else return calc.PutRho;
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
			items.Add(new Item("Call", 1, 2.12));
			items.Add(new Item("Call", 1, 2.1));
			items.Add(new Item("Put", -2, 1.13));

			//
			this.datagridOptions.ItemsSource = items;
		}
	}
}

