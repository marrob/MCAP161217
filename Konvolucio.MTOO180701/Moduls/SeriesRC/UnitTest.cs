

namespace Konvolucio.MTOO180701.Moduls.RLC.SeriesRC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;
    using System.Numerics;
    using System.Diagnostics;
    using Common;

    [TestFixture]
    class UnitTest
    {
        [Test]
        public void VectorRatioDetector()
        {
            var vrd = new VectorRatioDetector();
        }

        /// <summary>
        /// Agilent Impedance Measurement Handbook
        /// </summary>
        [Test]
        public void VectorRatioDetector2()
        {
            var a = 1.3;    /*VxRe*/
            var b = 0.00;   /*VxIm*/
            var c = 1.27;   /*VrRe*/
            var d = -0.23;  /*VrIm*/
            var Rr = 1E+3;  /*OHM*/
            var f = 1E+3;   /*Hz*/

            var Rx = Rr * ((a * c + b * d) / (c * c + d * d)); /*Vx/Vr komplex felírásának Valós/Re része*/
            var Xx = Rr * ((b * c - a * d) / (c * c + d * d)); /*Vx/Vr komplex felírásának képzetets/Im része*/

            Debug.WriteLine(@"Rx: " + string.Format(new SuffixFormatter(), "{0:sfx}Ω", Rx));
            Debug.WriteLine(@"Xx: " + string.Format(new SuffixFormatter(), "{0:sfx}Ω", Xx));
            var phase = Math.Atan2(Xx, Rr) * (180 / Math.PI);
            Debug.WriteLine(@"Phase: " + string.Format("{0:g3}°", phase));
            var Cx = 1 / (Xx * 2 * f * Math.PI);
            Debug.WriteLine(@"Cx: " + string.Format(new SuffixFormatter(), "{0:sfx}F", Cx));
            Complex Zx = new Complex(Rx, Xx);
        }
    }
}
