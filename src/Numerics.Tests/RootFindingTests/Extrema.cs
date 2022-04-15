namespace Analysis
{
    public abstract partial class Extrema
	{
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
	}
}
