using System;


namespace OptionTools
{
	public class Option
	{
		//
		private double price;
		private double strike;
		private int daysToExpiration;
		private double volatility;
		private double interestRate;

		//
		private double callValue;
		private double callDelta;
		private double callGamma;
		private double callTheta;
		private double callVega;
		private double callRho;

		//
		private double putValue;
		private double putDelta;
		private double putGamma;
		private double putTheta;
		private double putVega;
		private double putRho;

		//
		private bool calc;

		public Option()
		{
			//
			price = 0;
			strike = 0;
			daysToExpiration = 0;
			volatility = 0;
			interestRate = 0;

			//
			callValue = 0;
			callDelta = 0;
			callGamma = 0;
			callTheta = 0;
			callVega = 0;
			callRho = 0;

			//
			putValue = 0;
			putDelta = 0;
			putGamma = 0;
			putTheta = 0;
			putVega = 0;
			putRho = 0;

			//
			calc = false;
		}

		public double Price
		{
			get
			{
				return price;
			}

			set
			{
				price = value;
				calc = true;
			}
		}

		public double Strike
		{
			get
			{
				return strike;
			}

			set
			{
				strike = value;
				calc = true;
			}
		}

		public int DaysToExpiration
		{
			get
			{
				return daysToExpiration;
			}

			set
			{
				daysToExpiration = value;
				calc = true;
			}
		}

		public double Volatility
		{
			get
			{
				return volatility;
			}

			set
			{
				volatility = value;
				calc = true;
			}
		}

		public double InterestRate
		{
			get
			{
				return interestRate;
			}

			set
			{
				interestRate = value;
				calc = true;
			}
		}

		public double CallValue
		{
			get
			{
				Calc();
				return callValue;
			}
		}

		public double CallDelta
		{
			get
			{
				Calc();
				return callDelta;
			}
		}

		public double CallGamma
		{
			get
			{
				Calc();
				return callGamma;
			}
		}

		public double CallTheta
		{
			get
			{
				Calc();
				return callTheta;
			}
		}

		public double CallVega
		{
			get
			{
				Calc();
				return callVega;
			}
		}

		public double CallRho
		{
			get
			{
				Calc();
				return callRho;
			}
		}

		public double PutValue
		{
			get
			{
				Calc();
				return putValue;
			}
		}

		public double PutDelta
		{
			get
			{
				Calc();
				return putDelta;
			}
		}

		public double PutGamma
		{
			get
			{
				Calc();
				return putGamma;
			}
		}

		public double PutTheta
		{
			get
			{
				Calc();
				return putTheta;
			}
		}

		public double PutVega
		{
			get
			{
				Calc();
				return putVega;
			}
		}

		public double PutRho
		{
			get
			{
				Calc();
				return putRho;
			}
		}

		private void Calc()
		{
			//
			if (!calc) return;
			calc = false;

			//
			// TODO
		}
	}
}

