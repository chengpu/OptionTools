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
			//
			items = new ObservableCollection<Item>();
			items.Add(new Item("Underlying", 1, 100, 0, 0));
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

