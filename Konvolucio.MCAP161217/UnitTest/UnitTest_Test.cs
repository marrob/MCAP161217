
namespace Konvolucio.MCAP161217.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Net.Configuration;
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

        /// <summary>
        /// https://batchloaf.wordpress.com/2013/12/07/simple-dft-in-c/
        /// </summary>
        [Test]
        public void DftBruteForce()
        {

            // time and frequency domain data arrays
            int n, k, N;            // indices for time and frequency domains
            double[] x;             // discrete-time signal, x
            double[] Xre, Xim;      // DFT of x (real and imaginary parts)
            double[] P;             // power spectrum of x

            N = 100;

            x = new double[N];
            Xre = new double[N];
            Xim = new double[N];
            P= new double[N];

            // Calculate DFT of x using brute force
            for (k = 0; k < N; ++k)
            {
                // Real part of X[k]
                Xre[k] = 0;
                for (n = 0; n < N; ++n) Xre[k] += x[n] * Math.Cos(n * k * 2 * Math.PI / N);

                // Imaginary part of X[k]
                Xim[k] = 0;
                for (n = 0; n < N; ++n) Xim[k] -= x[n] * Math.Sin(n * k * 2 * Math.PI / N);

                // Power at kth frequency bin
                P[k] = Xre[k] * Xre[k] + Xim[k] * Xim[k];
            }
        }


        public void DftBruteForce(double[] signal)
        {
            // time and frequency domain data arrays
            int n, k, N;            // indices for time and frequency domains
            double[] Xre, Xim;      // DFT of x (real and imaginary parts)
            double[] P;             // power spectrum of x

            N = signal.Length;
            Xre = new double[N];
            Xim = new double[N];
            P = new double[N];

            // Calculate DFT of x using brute force
            for (k = 0; k < N; ++k)
            {
                // Real part of X[k]
                Xre[k] = 0;
                for (n = 0; n < N; ++n) Xre[k] += signal[n] * Math.Cos(n * k * 2 * Math.PI / N);

                // Imaginary part of X[k]
                Xim[k] = 0;
                for (n = 0; n < N; ++n) Xim[k] -= signal[n] * Math.Sin(n * k * 2 * Math.PI / N);

                // Power at kth frequency bin
                P[k] = Xre[k] * Xre[k] + Xim[k] * Xim[k];
            }

            DoubleArrayToFile(P, "D:\\power2.csv");
        }

        [Test]
        public void LoadTestVector()
        {

            DftBruteForce(FileToDoubleArray("D:\\sine2.csv"));
        }

        [Test]
        public void CreateTestVector()
        {
            string line = string.Empty;
            int maxCycle = 100;
            double maxRes = Math.PI/20;

            double[]  values = new double[maxCycle * (int)((Math.PI*2)/(Math.PI/ 20))];

            int index = 0;

            for (int cycle = 0; cycle < maxCycle; cycle++)
            {
                for (double p = 0; p < 2*Math.PI; p += maxRes)
                {
                    values[index++] = Math.Sin(p);
                }
            }

            DoubleArrayToFile(values, "D:\\tesztvector.csv");
        }



        [Test]
        public void CreateTest0002()
        {
           var signal =  SequenceGenerator( amplitude: 1,
                                             cycleNum: 1,
                                             resolution: Math.PI/1000,
                                             phase: Math.PI / 2,
                                             offset: 0);

            DoubleArrayToFile(signal, "D:\\tesztvector.csv");
        }

        [Test]
        public void CreateTest0003()
        {
            var signal = SequenceGenerator(amplitude: 128,
                                              cycleNum: 1,
                                              resolution: Math.PI / 1000,
                                              phase: 0,
                                              offset: 128);

            DoubleArrayToFile(signal, "D:\\tesztvector.csv");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="amplitude"></param>
        /// <param name="cycleNum">The periodic count. eg.:1,2,3...n</param>
        /// <param name="resolution">Period resolution  in radian. min: 2*Math.PI , or Math.PI/20 </param>
        /// <param name="phase">Phase in radian. eg.: Math.PI/2 = 90degree </param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static double[] SequenceGenerator(   double amplitude,
                                                    int cycleNum,
                                                    double resolution,
                                                    double phase,
                                                    double offset)
        {
            double[] squence = new double[0];
            int index = 0;
            for (int cycle = 0; cycle < cycleNum; cycle++)
                for (double p = 0; p <= (2 * Math.PI); p += resolution)
                {
                    Array.Resize(ref squence, index + 1);
                    squence[index++] = amplitude * Math.Sin(p + phase) + offset;   
                }

            return squence;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static double[] FileToDoubleArray(string path)
        {
            FileInfo f = new FileInfo(path);
            int lines = 0;
            double[] values = new double[0];
            if (f.Exists)
            {
                using (var sr = new StreamReader(path))
                {
                    string line = null;
                    do
                    {
                        line = sr.ReadLine();
                        if (line != null)
                        {
                            Array.Resize(ref values, lines + 1);
                            values[lines++] = double.Parse(line);
                        }
                    } while (line != null);
                }
            }
            return values;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        static void DoubleArrayToFile(double[] data, string path)
        {
            var f = new FileInfo(path);
            if (f.Exists)
                f.Delete();

            var sw = new StreamWriter(path);
            foreach (var value in data)
                sw.WriteLine(value.ToString());
            
        }

    }
}
