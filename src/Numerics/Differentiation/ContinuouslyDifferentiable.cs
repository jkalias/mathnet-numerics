// <copyright file="FiniteDifferenceCoefficients.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
//
// Copyright (c) 2009-2015 Math.NET
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

namespace MathNet.Numerics.Differentiation
{
    public abstract class ContinuouslyDifferentiable
    {
        public abstract double Value(double x);

        public abstract double Derivative(double x);

        public static ContinuouslyDifferentiable operator + (ContinuouslyDifferentiable f, ContinuouslyDifferentiable g)
        {
            return new ContinuouslyDifferentiableAddition(f, g);
        }

        public static ContinuouslyDifferentiable operator - (ContinuouslyDifferentiable f, ContinuouslyDifferentiable g)
        {
            return new ContinuouslyDifferentiableSubtraction(f, g);
        }

        public static ContinuouslyDifferentiable operator * (ContinuouslyDifferentiable f, ContinuouslyDifferentiable g)
        {
            return new ContinuouslyDifferentiableMultiplication(f, g);
        }

        public static ContinuouslyDifferentiable operator / (ContinuouslyDifferentiable f, ContinuouslyDifferentiable g)
        {
            return new ContinuouslyDifferentiableDivision(f, g);
        }

        public ContinuouslyDifferentiable this[ContinuouslyDifferentiable g] => new ContinuouslyDifferentiableComposition(this, g);

        private class ContinuouslyDifferentiableAddition : ContinuouslyDifferentiable
        {
            private readonly ContinuouslyDifferentiable _f;
            private readonly ContinuouslyDifferentiable _g;

            public ContinuouslyDifferentiableAddition(
                ContinuouslyDifferentiable f,
                ContinuouslyDifferentiable g)
            {
                _f = f;
                _g = g;
            }

            public override double Derivative(double x)
            {
                return _f.Derivative(x) + _g.Derivative(x);
            }

            public override double Value(double x)
            {
                return _f.Value(x) + _g.Value(x);
            }
        }

        private class ContinuouslyDifferentiableSubtraction : ContinuouslyDifferentiable
        {
            private readonly ContinuouslyDifferentiable _f;
            private readonly ContinuouslyDifferentiable _g;

            public ContinuouslyDifferentiableSubtraction(
                ContinuouslyDifferentiable f,
                ContinuouslyDifferentiable g)
            {
                _f = f;
                _g = g;
            }

            public override double Derivative(double x)
            {
                return _f.Derivative(x) - _g.Derivative(x);
            }

            public override double Value(double x)
            {
                return _f.Value(x) - _g.Value(x);
            }
        }

        private class ContinuouslyDifferentiableMultiplication : ContinuouslyDifferentiable
        {
            private readonly ContinuouslyDifferentiable _f;
            private readonly ContinuouslyDifferentiable _g;

            public ContinuouslyDifferentiableMultiplication(
                ContinuouslyDifferentiable f,
                ContinuouslyDifferentiable g)
            {
                _f = f;
                _g = g;
            }

            public override double Derivative(double x)
            {
                return _f.Derivative(x) * _g.Value(x) + _f.Value(x) * _g.Derivative(x);
            }

            public override double Value(double x)
            {
                return _f.Value(x) * _g.Value(x);
            }
        }

        private class ContinuouslyDifferentiableDivision : ContinuouslyDifferentiable
        {
            private readonly ContinuouslyDifferentiable _f;
            private readonly ContinuouslyDifferentiable _g;

            public ContinuouslyDifferentiableDivision(
                ContinuouslyDifferentiable f,
                ContinuouslyDifferentiable g)
            {
                _f = f;
                _g = g;
            }

            public override double Derivative(double x)
            {
                var denominator = _g.Value(x) * _g.Value(x);

                return denominator != 0
                    ? (_f.Derivative(x) * _g.Value(x) - _f.Value(x) * _g.Derivative(x)) / denominator
                    : double.NaN;
            }

            public override double Value(double x)
            {
                var denominator = _g.Value(x);

                return denominator != 0
                    ? _f.Value(x) / _g.Value(x)
                    : double.NaN;
            }
        }

        private class ContinuouslyDifferentiableComposition : ContinuouslyDifferentiable
        {
            private readonly ContinuouslyDifferentiable _f;
            private readonly ContinuouslyDifferentiable _g;

            public ContinuouslyDifferentiableComposition(
                ContinuouslyDifferentiable f,
                ContinuouslyDifferentiable g)
            {
                _f = f;
                _g = g;
            }

            public override double Derivative(double x)
            {
                return _f.Derivative(_g.Value(x)) * _g.Derivative(x);
            }

            public override double Value(double x)
            {
                return _f.Value(_g.Value(x));
            }
        }
    }
}
