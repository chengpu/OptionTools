﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace OptionStrategy
{
	public class Item : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;


		


		//
		private string type;
		private int position;
		private double cost;
		private int daysToExpiration;
		private double strikePrice;

		//
		private double underlyingPrice;
		private int daysPast;
		private double volatility;
		private double interestRate;

		//
		private OptionTools.Option calc;

		public Item(string type, int position, double cost, int daysToExpiration, double strikePrice)
		{
			//
			this.type = type;
			this.position = position;
			this.cost = cost;
			this.daysToExpiration = daysToExpiration;
			this.strikePrice = strikePrice;

			//
			this.underlyingPrice = 0;
			this.daysPast = 0;
			this.volatility = 0;
			this.interestRate = 0.04;

			//
			calc = null;
			if ((type == "Call") || (type == "Put"))
			{
				calc = new OptionTools.Option();
				calc.Price = underlyingPrice;
				calc.Strike = strikePrice;
				calc.DaysToExpiration = daysToExpiration - daysPast;
				calc.Volatility = volatility;
				calc.InterestRate = interestRate;
			}
		}

		public void SetUnderlyingPrice(double underlyingPrice)
		{
			//
			this.underlyingPrice = underlyingPrice;
			if (calc != null)
			{
				calc.Price = underlyingPrice;
			}

			//
			OnPropertyChanged();
		}

		public void SetDaysPast(int daysPast)
		{
			//
			this.daysPast = daysPast;
			if (calc != null)
			{
				calc.DaysToExpiration = daysToExpiration - daysPast;
			}

			//
			OnPropertyChanged();
		}

		public void SetVolatility(double volatility, bool notify)
		{
			//
			this.volatility = volatility;
			if (calc != null)
			{
				calc.Volatility = volatility;
			}

			//
			OnPropertyChanged();
			if (notify) OnPropertyChangedVolatility();
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

		public int Days
		{
			get
			{
				if ((type == "Call") || (type == "Put"))
				{
					return daysToExpiration - daysPast;
				}
				else
				{
					return 0;
				}
			}
		}

		public double Strike
		{
			get
			{
				return strikePrice;
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
				return volatility * 100.0f;
			}
		}

		public double VolatilitySlider
		{
			get
			{
				return volatility * 100.0f;
			}
		}

		private void OnPropertyChanged()
		{
			OnPropertyChanged(this, "Price");
			OnPropertyChanged(this, "Profit");
			OnPropertyChanged(this, "Days");
			OnPropertyChanged(this, "Strike");
			OnPropertyChanged(this, "Delta");
			OnPropertyChanged(this, "Gamma");
			OnPropertyChanged(this, "Theta");
			OnPropertyChanged(this, "Vega");
			OnPropertyChanged(this, "Rho");
			OnPropertyChanged(this, "Volatility");
		}

		private void OnPropertyChangedVolatility()
		{
			OnPropertyChanged(this, "VolatilitySlider");
		}

		private void OnPropertyChanged(object sender, string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
