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
		private NormalDistribution norm;

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
			norm = new NormalDistribution();

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
			if (strike <= 0) return;

			//
			if (daysToExpiration <= 0)
			{
				// TODO
				return;
			}

			//
			double t = daysToExpiration / 365.0;
			double sqrt_t = Math.Sqrt(t);
			double d1 = (Math.Log(price / strike) + (interestRate + 0.5 * volatility * volatility) * t) / (volatility * sqrt_t);
			double d2 = d1 - volatility * sqrt_t;

			//
			callValue = price * norm.CDF(d1) - strike * Math.Exp(-interestRate * t) * norm.CDF(d2);
			callDelta = norm.CDF(d1);
			callGamma = norm.PDF(d1) / (price * volatility * sqrt_t);
			callTheta = -price * norm.PDF(d1) * volatility / (2 * sqrt_t) - interestRate * strike * Math.Exp(-interestRate * t) * norm.CDF(d2);
			callVega = price * sqrt_t * norm.PDF(d1);
			callRho = strike * t * Math.Exp(-interestRate * t) * norm.CDF(d2);

			//
			putValue = strike * Math.Exp(-interestRate * t) * norm.CDF(-d2) - price * norm.CDF(-d1);
			putDelta = norm.CDF(d1) - 1;
			//putGamma = 0;
			putTheta = -price * norm.PDF(d1) * volatility / (2 * sqrt_t) + interestRate * strike * Math.Exp(-interestRate * t) * norm.CDF(-d2);
			//putVega = 0;
			putRho = -strike * t * Math.Exp(-interestRate * t) * norm.CDF(-d2);
		}
	}
}

