using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace OptionStrategy
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ObservableCollection<Item> items;
		private bool lockVolatility = false;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			/*
			//
			items = new ObservableCollection<Item>();
			items.Add(new Item("Underlying", 2, 110, 0, 0));
			items.Add(new Item("Underlying", -3, 120, 0, 0));
			items.Add(new Item("Call", 1, 2.12, 30, 110));
			items.Add(new Item("Call", 1, 2.1, 30, 110));
			items.Add(new Item("Put", -2, 1.13, 30, 110));

			for (int i = 0; i < items.Count; i++)
			{
				items[i].SetUnderlyingPrice(100);
				items[i].SetDaysPast(0);
				items[i].SetVolatility(0.25, false);
			}

			//
			this.datagridOptions.ItemsSource = items;

			//
			this.labelPrice.Content = "" + 100.0f;
			this.sliderPrice.Minimum = 50.0f;
			this.sliderPrice.Maximum = 150.0f;
			this.sliderPrice.Value = 100.0f;

			//
			this.labelDays.Content = "0";
			this.sliderDays.Minimum = 0;
			this.sliderDays.Maximum = 30;
			this.sliderDays.Value = 5;

			//
			UpdateTotal();
			 * */
		}

		private void Window_Drop(object sender, DragEventArgs e)
		{
			//
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			string file = files[0];

			//
			try
			{
				//
				XmlDocument doc = new XmlDocument();
				doc.Load(file);

				//
				var nodeStrategy = doc.GetElementsByTagName("Strategy")[0];
				string strategyName = nodeStrategy.Attributes["Name"].Value;
				double volatilityMin = double.Parse(nodeStrategy.Attributes["VolatilityMin"].Value) / 100.0;
				double volatilityMax = double.Parse(nodeStrategy.Attributes["VolatilityMax"].Value) / 100.0;
				double interestRate = double.Parse(nodeStrategy.Attributes["InterestRate"].Value) / 100.0;

				//
				ObservableCollection<Item> itemsTemp = new ObservableCollection<Item>();
				foreach (XmlNode node in nodeStrategy.ChildNodes)
				{
					//
					if (node.Name != "Position") continue;

					//
					string type = node.Attributes["Type"].Value;
					int count = int.Parse(node.Attributes["Count"].Value);
					double cost = double.Parse(node.Attributes["Cost"].Value);
					int days = int.Parse(node.Attributes["Days"].Value);
					double strike = double.Parse(node.Attributes["Strike"].Value);

					//
					itemsTemp.Add(new Item(type, count, cost, days, strike, volatilityMin, volatilityMax, interestRate));
				}

				//
				for (int i = 0; i < itemsTemp.Count; i++)
				{
					itemsTemp[i].SetUnderlyingPrice(100);
					itemsTemp[i].SetDaysPast(0);
					itemsTemp[i].SetVolatility(volatilityMin, false);
				}

				//
				items = itemsTemp;

				//
				this.Title = string.Format("OptionStrategy - {0}", strategyName);

				//
				this.datagridOptions.ItemsSource = items;

				//
				this.labelPrice.Content = "" + 100.0f;
				this.sliderPrice.Minimum = 50.0f;
				this.sliderPrice.Maximum = 150.0f;
				this.sliderPrice.Value = 100.0f;

				//
				this.labelDays.Content = "0";
				this.sliderDays.Minimum = 0;
				this.sliderDays.Maximum = 30;
				this.sliderDays.Value = 5;

				//
				UpdateTotal();
			}
			catch
			{
				MessageBox.Show(string.Format("Read {0} error", file));
				return;
			}
		}

		private void UpdateTotal()
		{
			double profit = 0;
			double delta = 0;
			double gamma = 0;
			double theta = 0;
			double vega = 0;
			double rho = 0;

			if (items != null)
			{
				for (int i = 0; i < items.Count; i++)
				{
					profit += items[i].Profit;
					delta += items[i].Delta * items[i].Position;
					gamma += items[i].Gamma * items[i].Position;
					theta += items[i].Theta * items[i].Position;
					vega += items[i].Vega * items[i].Position;
					rho += items[i].Rho * items[i].Position;
				}
			}

			if (this.labelProfit != null) this.labelProfit.Content = string.Format("{0:F4}", profit);
			if (this.labelDelta != null) this.labelDelta.Content = string.Format("{0:F4}", delta);
			if (this.labelGamma != null) this.labelGamma.Content = string.Format("{0:F4}", gamma);
			if (this.labelTheta != null) this.labelTheta.Content = string.Format("{0:F4}", theta);
			if (this.labelVega != null) this.labelVega.Content = string.Format("{0:F4}", vega);
			if (this.labelRho != null) this.labelRho.Content = string.Format("{0:F4}", rho);
		}

		private void sliderPrice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			//
			if (this.labelPrice != null)
			{
				this.labelPrice.Content = string.Format("{0:F4}", e.NewValue);
			}

			//
			if (items != null)
			{
				for (int i = 0; i < items.Count; i++)
				{
					items[i].SetUnderlyingPrice(e.NewValue);
				}
			}

			//
			UpdateTotal();
		}

		private void sliderDays_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			//
			if (this.labelDays != null)
			{
				this.labelDays.Content = string.Format("{0}", (int)e.NewValue);
			}

			//
			if (items != null)
			{
				for (int i = 0; i < items.Count; i++)
				{
					items[i].SetDaysPast((int)e.NewValue);
				}
			}

			//
			UpdateTotal();
		}

		private void sliderVolatility_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			//
			if (items != null)
			{
				//
				Slider slider = (Slider)sender;
				Item item = (Item)slider.DataContext;
				item.SetVolatility(e.NewValue / 100.0, false);

				//
				if (lockVolatility)
				{
					
					for (int i = 0; i < items.Count; i++)
					{
						Item item2 = items[i];
						if (item2 != item)
						{
							item2.SetVolatility(e.NewValue / 100.0, true);
						}
					}
				}
			}

			//
			UpdateTotal();
		}

		private void checkBoxLock_Checked(object sender, RoutedEventArgs e)
		{
			lockVolatility = true;
		}

		private void checkBoxLock_Unchecked(object sender, RoutedEventArgs e)
		{
			lockVolatility = false;
		}
	}
}

