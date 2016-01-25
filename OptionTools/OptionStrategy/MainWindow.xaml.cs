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

			public Item(string type)
			{
				this.type = type;
			}

			public string Type
			{
				get
				{
					return type;
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
			items.Add(new Item("Call"));
			items.Add(new Item("Call"));
			items.Add(new Item("Put"));

			//
			this.datagridOptions.ItemsSource = items;
		}
	}
}
