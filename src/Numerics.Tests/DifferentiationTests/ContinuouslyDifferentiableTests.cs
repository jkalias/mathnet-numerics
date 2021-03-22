// <copyright file="ContinuouslyDifferentiableTests.cs" company="Math.NET">
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
    public class ContinuouslyDifferentiableTests
    {
        private ContinuouslyDifferentiable sin = new Sin();
        private ContinuouslyDifferentiable cos = new Cos();
        private ContinuouslyDifferentiable linear = new X();

        [Test]
        public void ContinuouslyDifferentiableScalarMultiplyTest()
        {
            var a = 3.2;
            var scalarMultiplication = a * sin;
            var x = 0.1;
            Assert.AreEqual(a * Math.Sin(x), scalarMultiplication.Value(x));
            Assert.AreEqual(a * Math.Cos(x), scalarMultiplication.Derivative(x));
        }

        [Test]
        public void ContinuouslyDifferentiableUnaryMinusTest()
        {
            var unaryMinus = -sin;
            var x = 0.1;
            Assert.AreEqual(-Math.Sin(x), unaryMinus.Value(x));
            Assert.AreEqual(-Math.Cos(x), unaryMinus.Derivative(x));
        }

        [Test]
        public void ContinuouslyDifferentiableAdditionTest()
        {
            var addition = sin + cos;
            var x = 0.1;
            Assert.AreEqual(Math.Sin(x) + Math.Cos(x), addition.Value(x));
            Assert.AreEqual(Math.Cos(x) - Math.Sin(x), addition.Derivative(x));
        }

        [Test]
        public void ContinuouslyDifferentiableSubtractionTest()
        {
            var subtraction = sin - cos;
            var x = 0.1;
            Assert.AreEqual(Math.Sin(x) - Math.Cos(x), subtraction.Value(x));
            Assert.AreEqual(Math.Cos(x) + Math.Sin(x), subtraction.Derivative(x));
        }

        [Test]
        public void ContinuouslyDifferentiableMultiplicationTest()
        {
            var multiplication = sin * cos;
            var x = 0.1;
            Assert.AreEqual(Math.Sin(x) * Math.Cos(x), multiplication.Value(x));
            Assert.AreEqual(Math.Cos(x) * Math.Cos(x) - Math.Sin(x) * Math.Sin(x), multiplication.Derivative(x));
        }

        [Test]
        public void ContinuouslyDifferentiableDivisionTest()
        {
            var division = sin / cos;
            var x = 0.1;
            Assert.AreEqual(Math.Tan(x), division.Value(x));
            Assert.AreEqual(2 / (Math.Cos(2 * x) + 1), division.Derivative(x));
        }

        [Test]
        public void ContinuouslyDifferentiableCompositionTest()
        {
            var f = sin[cos];
            var x = 0.3;
            Assert.AreEqual(Math.Sin(Math.Cos(x)), f.Value(x));
            Assert.AreEqual(-Math.Sin(x) * (Math.Cos(Math.Cos(x))), f.Derivative(x));
        }

        [Test]
        public void ContinuouslyDifferentiableExponentiationTest()
        {
            var r = 3.2;
            var f = sin ^ r;
            var x = 0.2;
            Assert.AreEqual(Math.Pow(Math.Sin(x), r), f.Value(x));
            Assert.AreEqual(r * Math.Pow(Math.Sin(x), r - 1), f.Derivative(x));
        }

        [Test]
        public void ContinuouslyDifferentiableInverseTest()
        {
            var f = linear ^ (-1);
            var x = 0.2;
            Assert.AreEqual(1 / x, f.Value(x));
            Assert.AreEqual(-1 / (x * x), f.Derivative(x));
        }

        [Test]
        public void ContinuouslyDifferentiableGeneralizedPowerRuleTest()
        {
            var f = sin ^ cos;
            var x = 0.2;
            Assert.AreEqual(Math.Pow(Math.Sin(x), Math.Cos(x)), f.Value(x));
            Assert.AreEqual(1.0578530168324183, f.Derivative(x));
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

    public class X : ContinuouslyDifferentiable
    {
        public override double Derivative(double x)
        {
            return 1;
        }

        public override double Value(double x)
        {
            return x;
        }
    }
}
