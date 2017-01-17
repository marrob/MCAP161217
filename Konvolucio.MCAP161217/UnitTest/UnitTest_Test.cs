
namespace Konvolucio.MCAP161217.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using System.Numerics;
    using NUnit.Framework.Constraints;

    [TestFixture]
    public class UnitTest_Test
    {
        [Test]
        public void First()
        {
            Assert.Pass();
        }

        [Test]
        public void ComplexImpedance()
        {
            double cap = 1E-9; // 1nF
            double f = 1000;   // 1KHz
            double xc = 1/(2*f*Math.PI*cap);
            xc *= -1;
                
            double impRe = 159000; //Resistor = 159kOHM
            double impIm = xc;
            double sourceRMS = 0.35350;

            Complex imp = new Complex(impRe, impIm);
            double impValue = Complex.Abs(imp);
            Assert.AreEqual(224969.54440675842d, impValue);

            Complex current = sourceRMS/imp;
            double currrentValue = Complex.Abs(current);
            Assert.AreEqual(1.5713238026603759E-06, currrentValue);

            var phase = Math.Atan2(current.Imaginary,current.Real) * 180/Math.PI;
            Assert.AreEqual(45.027903336789905d, phase);

          
            Console.WriteLine(imp.ToString());

        }

        [Test]
        public void Complex1()
        {
            double cap = 100E-9; // 100nF
            double f = 1000;   // 1KHz
            double xc = 1 / (2 * f * Math.PI * cap);
            xc *= -1;

            double impRe = 0;// 159000; //Resistor = 159kOHM
            double impIm = xc;
            double sourceRMS = 0.180;

            Complex imp = new Complex(impRe, impIm);
            double impValue = Complex.Abs(imp);
            Console.WriteLine(@"Impedance Value: " + impValue.ToString());

            Complex current = sourceRMS / imp;
            double currrentValue = Complex.Abs(current);
            Console.WriteLine(@"Current Value: " + currrentValue.ToString());

            var phase = Math.Atan2(current.Imaginary, current.Real) * (180 / Math.PI);
            Console.WriteLine(@"Phase: " + phase.ToString());
        }

        [Test]
        public void NewTest()
        {

        }

        [Test]
        public void BetterNewTest()
        {
            Console.WriteLine("Hello World");
        }

    }
}
