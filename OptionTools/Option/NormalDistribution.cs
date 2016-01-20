using System;


namespace OptionTools
{
	internal class NormalDistribution
	{
		private double r_sqrt_2_pi;

		public NormalDistribution()
		{
			r_sqrt_2_pi = 1.0 / Math.Sqrt(2 * Math.PI);
		}

		public double PDF(double x)
		{
			return r_sqrt_2_pi * Math.Exp(-0.5 * x * x);
		}

		public double CDF(double x)
		{
			//
			if (x < -7) return 0;
			if (x > 7) return 1;

			//
			double x2 = x * x;
			double sum = 0;
			double c = x;
			sum += c;
			for (int i = 3; i < 9999; i += 2)
			{
				c *= (x2 / i);
				sum += c;
				if (Math.Abs(c) < 0.0001)
				{
					break;
				}
			}

			//
			return 0.5 + r_sqrt_2_pi * Math.Exp(-0.5 * x * x) * sum;
		}
	}
}

