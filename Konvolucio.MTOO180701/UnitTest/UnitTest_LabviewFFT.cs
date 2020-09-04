
namespace Konvolucio.MTOO180701.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Net.Configuration;
    using System.Net.Mail;
    using System.Text;
    using NUnit.Framework;
    using System.Numerics;
    using System.Threading;
    using NUnit.Framework.Constraints;
    using System.Diagnostics;
    using Common;

    [TestFixture]
    class UnitTest_LabviewFFT
    {

        double[] SinTable_8 = { 0, 0.707, 1, 0.707, 0, -0.707, -1, -0.707 };
        double[] CosTable_8 = { 1, 0.707, 0, -0.707, -1, -0.707, 0, 0.707 };

        double[] SinTable_16 =
        {
            0,
            0.382683432,
            0.707106781,
            0.923879533,
            1,
            0.923879533,
            0.707106781,
            0.382683432,
            0.0,
            -0.382683432,
            -0.707106781,
            -0.923879533,
            -1,
            -0.923879533,
            -0.707106781,
            -0.382683432,
        };

        double[] CosTable_16 =  
        {
            1.0 ,
            0.923879533,
            0.707106781,
            0.382683432,
            0.0,
            -0.382683432,
            -0.707106781,
            -0.923879533,
            -1.0,
            -0.923879533,
            -0.707106781,
            -0.382683432,
            0.0,
            0.382683432,
            0.707106781,
            0.923879533,
        };




        double[] imp_table = { };

        [Test]
        public void LabViewSignalRead()
        {

            var complex =  FftLight(CosTable_16);

            string report = string.Empty;
            report += "VrRe: " + complex.Real + "\r\n";
            report += "VrIm: " + complex.Imaginary+ "\r\n";
            Debug.WriteLine(report);

            var complexArray = FFTBruteFroce(CosTable_16);

            report = string.Empty;
            report += "VrRe: " + complexArray[1].Real + "\r\n";
            report += "VrIm: " + complexArray[1].Imaginary + "\r\n";
            Debug.WriteLine(report);

        }


        public Complex FftLight(double[]x )
        {

            Byte dft_index = 0;
            const Byte DFT_LEN = 64;

            double Re = 0;
            double Im = 0;
            int N = x.Length;

            for (int n = 0; n < N; n++)
            {
                double _cos = CosTable_16[dft_index & 15];
                double _sin = SinTable_16[dft_index & 15];

                Re += x[dft_index] * _cos;
                Im += x[dft_index] * _sin;;

                dft_index++;
                dft_index &= (DFT_LEN - 1);
            }
            return  new Complex(Math.Round(Re, 4), Math.Round(Im, 4));
        }

        public void FftLightTest(int N, double[] Vx, double[] Vr)
        {

            Byte dft_index = 0;
            const Byte DFT_LEN = 64;

            double VxRe = 0;
            double VxIm = 0;
            double VrRe = 0;
            double VrIm = 0;

            for (int n = 0; n < N; n++)
            {
                var j = dft_index & 15;
                double _cos = CosTable_16[dft_index & 15];
                double _sin = SinTable_16[dft_index & 15];

                VxRe += Vx[dft_index] * _cos;
                VxIm += Vx[dft_index] * _sin;

                VrRe += Vr[dft_index] * _cos;
                VrIm += Vr[dft_index] * _sin;

                dft_index++;
                dft_index &= (DFT_LEN - 1);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void RLCMeter_VxVr_20072018_112918_82_Fs2500_N64()
        {
            var source = LoadLabViewAdcSamples(@"D:\@@@!ProjectS\KonvolucioApp\Media\VxVr_20072018-112918.82_Fs2500_N64.txt");

            double[] Vx = new double[source.Length];
            double[] Vr = new double[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                Vx[i] = source[i].Vx;
                Vr[i] = source[i].Vr;
            }

            RlcMeter(N:64, Fs:2500, Fmeas:995, Rr:1000, Vx:Vx, Vr:Vr);
        }

        /// <summary>
        /// RLC Meter
        /// </summary>
        /// <param name="N">Samples cont</param>
        /// <param name="Fs">DAQ aquisation frequency</param>
        /// <param name="Fmeas">measurement frequency</param>
        /// <param name="Rr">Current shount resistor</param>
        /// <param name="Vx">RC voltage</param>
        /// <param name="Vr">RC current</param>
        public void RlcMeter(int N, int Fs, int Fmeas, double Rr, double[]Vx, double[] Vr)
        {
            var fftVx = FFTBruteFroce(Vx);
            var fftVr = FFTBruteFroce(Vr);
            var bin = Fmeas / (Fs / N);
            RlcCalculator( Rr, Fmeas, fftVx[bin].Real, fftVx[bin].Imaginary, fftVr[bin].Real, fftVr[bin].Imaginary);
            string report = string.Empty;
            report += "VxRe: " + fftVx[bin].Real.ToString() + "\r\n";
            report += "VxIm: " + fftVx[bin].Imaginary.ToString() + "\r\n";
            report += "VrRe: " + fftVr[bin].Real.ToString() + "\r\n";
            report += "VrIm: " + fftVr[bin].Imaginary.ToString() + "\r\n";
            Debug.WriteLine(report);
        }

        /// <summary>
        /// Brute Forece FFT Calcualtor
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Complex[] FFTBruteFroce(double[] x)
        {
            var N = x.Length;
            var Xre = new double[N];
            var Xim = new double[N];
            var complexArray = new Complex[N];
            for (int k = 0; k < N; ++k)
            {
                for (int n = 0; n < N; ++n) Xre[k] += x[n] * Math.Cos(n * k * 2 * Math.PI / N);
                for (int n = 0; n < N; ++n) Xim[k] -= x[n] * Math.Sin(n * k * 2 * Math.PI / N);
                complexArray[k] = new Complex(Math.Round(Xre[k], 3), Math.Round(Xim[k], 3));
            }
            return complexArray;
        }
        
        /// <summary>
        /// RLC Results Calculator 
        /// </summary>
        /// <param name="Rr">value of current Meas resistor (OHM) </param>
        /// <param name="Fmeas">value of measurement freauency (Hz)</param>
        /// <param name="a">VxRe(V)</param>
        /// <param name="b">VxIm(V)</param>
        /// <param name="c">Vr(Re)</param>
        /// <param name="d">Vr(Im)</param>
        public void RlcCalculator(double Rr, double Fmeas,  double a, double b, double c, double d)
        {
            var Rx = Rr * ((a * c + b * d) / (c * c + d * d)); /*Vx/Vr komplex felírásának Valós/Re része*/
            var Xx = Rr * ((b * c - a * d) / (c * c + d * d)); /*Vx/Vr komplex felírásának képzetets/Im része*/

            Debug.WriteLine(@"Rx: " + string.Format(new SuffixFormatter(), "{0:sfx}Ω", Rx));
            Debug.WriteLine(@"Xx: " + string.Format(new SuffixFormatter(), "{0:sfx}Ω", Xx));
            var phase = Math.Atan2(Xx, Rr) * (180 / Math.PI);
            Debug.WriteLine(@"Phase: " + string.Format("{0:g3}°", phase));
            var Cx = 1 / (Xx * 2 * Fmeas * Math.PI);
            Debug.WriteLine(@"Cx: " + string.Format(new SuffixFormatter(), "{0:sfx}F", Cx));
            Complex Zx = new Complex(Rx, Xx);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static LabViewAdcSample[] LoadLabViewAdcSamples(string path)
        {

            var f = new FileInfo(path);
            int sampleIndex = 0;
            var values = new LabViewAdcSample[0];
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
                            var record = line.Split(';');
                            Array.Resize(ref values, sampleIndex + 1);
                            values[sampleIndex] = new LabViewAdcSample();
                            values[sampleIndex].Vx = double.Parse(record[0]);
                            values[sampleIndex].Vr = double.Parse(record[1]);
                            sampleIndex++;
                        }
                    } while (line != null);
                }
            }
            return values;
        }

        /// <summary>
        /// 
        /// </summary>
        private class LabViewAdcSample
        {
            public double Vx { get; set; }
            public double Vr { get; set; }
            public override string ToString()
            {
                return Vx.ToString() + ", " + Vr.ToString();
            }
        }
    }
}
