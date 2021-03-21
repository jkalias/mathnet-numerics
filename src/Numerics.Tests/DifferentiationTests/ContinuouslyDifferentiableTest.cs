// <copyright file="FiniteDifferenceCoefficientTests.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
//
// Copyright (c) 2009-2016 Math.NET
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

using System;
using MathNet.Numerics.Differentiation;
using NUnit.Framework;

namespace MathNet.Numerics.Tests.DifferentiationTests
{
    [TestFixture, Category("Differentiation")]
    public class ContinuouslyDifferentiableTest
    {
        private ContinuouslyDifferentiable sin = new Sin();
        private ContinuouslyDifferentiable cos = new Cos();

        [Test]
        public void ContinuouslyDifferentiableAdditionTest()
        {
            var addition = sin + cos;
            Assert.AreEqual(1.094837581924854, addition.Value(0.1));
        }

        [Test]
        public void ContinuouslyDifferentiableCompositionTest()
        {
            var f = sin[cos];
            Assert.AreEqual(0.81650805329435794, f.Value(0.3));
            Assert.AreEqual(-0.17061387613475204, f.Derivative(0.3));
        }
    }

    public class Sin : ContinuouslyDifferentiable
    {
        public override double Derivative(double x)
        {
            return Math.Cos(x);
        }

        public override double Value(double x)
        {
            return Math.Sin(x);
        }
    }

    public class Cos : ContinuouslyDifferentiable
    {
        public override double Derivative(double x)
        {
            return -Math.Sin(x);
        }

        public override double Value(double x)
        {
            return Math.Cos(x);
        }
    }
}
