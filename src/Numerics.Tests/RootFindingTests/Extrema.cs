using MathNet.Numerics.RootFinding;
using System;
using System.Collections.Generic;

namespace MathNet.Numerics.Tests.RootFindingTests
{
    public abstract class Extrema
	{
		private const int INTERVAL_COUNT = 20;
		private const int MAX_ITTERATIONS = 50;
		private const double DELTA = 1e-8;

		private Extrema(double x, double y)
		{
			X = x;
			Y = y;
		}

		public double X { get; }
		public double Y { get; }

		public sealed class Minima : Extrema
		{
			public Minima(double x, double y) : base(x, y) { }
		}

		public sealed class Maxima : Extrema
		{
			public Maxima(double x, double y) : base(x, y) { }
		}

		public static IEnumerable<Extrema> FindAll(
			Func<double, double> f,
			Func<double, double> df,
			Func<double, double> ddf,
			double x0,
			double x1,
			int intervalCount = INTERVAL_COUNT,
			double delta = DELTA)
		{
			var intervalSize = (x1 - x0) / intervalCount;

			var x = x0;

			while (x < x1)
			{
				if (RobustNewtonRaphson.TryFindRoot(df, ddf, x, Math.Min(x + intervalSize, x1), delta, MAX_ITTERATIONS, 5, out var root))
				{
					x = root + delta;
					var ddy = ddf(x);

					if (ddy > 0)
					{
						yield return new Minima(x, f(x));
					}
					else if (ddy < 0)
					{
						yield return new Maxima(x, f(x));
					}
				}
				else
				{
					x += intervalSize;
				}
			}
		}
	}
}
