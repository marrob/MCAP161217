

namespace Konvolucio.MTOO180701.Moduls.RLC.VectorRatioDetector
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Common;
    using System.Diagnostics;
    using System.Numerics;
    using System.ComponentModel;


    class VectorRatioDetector
    {

        /// <summary>
        /// VxRe
        /// </summary>
        [Description("a/VxRe")]
        public string Param_a
        {
            get { return a.ToString(); }
            set { a = double.Parse(value); }
        }
        double a = 1.3;

        /// <summary>
        /// VxIm
        /// </summary>
        [Description("b/VxIm")]
        public string Param_b
        {
            get { return b.ToString(); }
            set { b = double.Parse(value); }
        }
        double b = 0;

        /// <summary>
        /// VrRe
        /// </summary>
        [Description("c/VrRe")]
        public string Param_c
        {
            get { return c.ToString(); }
            set { c = double.Parse(value); }
        }
        double c = 1.27;

        /// <summary>
        /// VrIm
        /// </summary>
        [Description("d/VrIm")]
        public string Param_d
        {
            get { return d.ToString(); }
            set { d = double.Parse(value); }
        }
        double d = -0.23; 

        /// <summary>
        /// Ohm
        /// </summary>
        [Description("Rr[Ω]")]
        public string Param_Rr
        {
            get { return Rr.ToString(); }
            set { Rr = double.Parse(value); }
        }
        double Rr = 1E+3; /*OHM*/

        /// <summary>
        /// Freq
        /// </summary>
        [Description("f[Hz]")]
        public string Param_f
        {
            get { return f.ToString(); }
            set { f = double.Parse(value); }
        }
        double f = 1E+3; /*Hz*/


        [Description("Result[Hz]")]
        public string Result { get; private set; }

        public void Calc()
        {
            //var a = 1.3;    /*VxRe*/
            //var b = 0.00;   /*VxIm*/
            //var c = 1.27;   /*VrRe*/
            //var d = -0.23;  /*VrIm*/
            //var Rr = 1E+3;  /*OHM*/
            //var f = 1E+3;   /*Hz*/

            var Rx = Rr * ((a * c + b * d) / (c * c + d * d)); /*Vx/Vr komplex felírásának Valós/Re része*/
            var Xx = Rr * ((b * c - a * d) / (c * c + d * d)); /*Vx/Vr komplex felírásának képzetets/Im része*/

            Result = string.Empty;

            Result += @"Vx:" +  new Complex(a, b).ToString("F2");
            Result += "\r\n";
            Result += @"Vr:" +  new Complex(c, d).ToString("F2");
            Result += "\r\n";
            Result += @"Rr:" + string.Format(new SuffixFormatter(), "{0:sfx}Ω", Rr);
            Result += "\r\n";
            Result += @"f:" + string.Format(new SuffixFormatter(), "{0:sfx}Hz", f);
            Result += "\r\n";
            Result += @"---";
            Result += "\r\n";
            Result += @"Rx: " + string.Format(new SuffixFormatter(), "{0:sfx}Ω", Rx);
            Result += "\r\n";
            Result += @"Xx: " + string.Format(new SuffixFormatter(), "{0:sfx}Ω", Xx);
            Result += "\r\n";
            var phase = Math.Atan2(Xx, Rr) * (180 / Math.PI);
            Result += @"Phase: " + string.Format("{0:g3}°", phase);
            Result += "\r\n";
            var Cx = 1 / (Xx * 2 * f * Math.PI);
            Result += @"Cx: " + string.Format(new SuffixFormatter(), "{0:sfx}F", Cx);
            Result += "\r\n";


            Complex Zx = new Complex(Rx, Xx);
        }

    }
}