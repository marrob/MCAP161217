using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Garph
{
    public partial class Form1 : Form
    {
        Graph2D DispGenerator;
       // Graph2D DispResult;
        public const int SAMPLE_LENGHT = 320;
        float[] SinusTable;
        int Amp;
        int Offset;
        int Freq;

        public Form1()
        {
            SinusTable = new float[SAMPLE_LENGHT];
            InitializeComponent();
            DispGenerator = new Graph2D();
            Amp = 0;
            Offset = 0;
            Freq = 1;

            DispGenerator.Show();
        
        }
        /****************************************************************/
        void Calc()
        {
            SignalLibGenerator(ref SinusTable, SignalType.PerilinNoise, Amp, (float)(1.0 / (SAMPLE_LENGHT / Freq)), Offset, SAMPLE_LENGHT);
            lbMean.Text = string.Format("{0:F2}",Mean(SinusTable, SAMPLE_LENGHT));
            lbAv.Text = string.Format("{0:F2}",Average(SinusTable, SAMPLE_LENGHT));
            lbAmp.Text = string.Format("{0:F2}",Amp);
            lbFreq.Text = string.Format("{0:F2}",Freq);
            lbOffset.Text = string.Format("{0:F2}",Offset);
            lbPeak.Text = string.Format("{0:F2}", Peak2Peak(SinusTable,SAMPLE_LENGHT));
            DispGenerator.DataSource = SinusTable;

            for (int N = 0; N < SAMPLE_LENGHT; N++)
            {
                listBox1.Items.Add(string.Format("{0:F2}", SinusTable[N]));
            }
           
        }
      
        /****************************************************************/
        public enum SignalType
        {
            SinusWave,
            CosinusWave,
            TriangleWave,
            PerilinNoise

        }
        /****************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="D"> sine wavetable of length D</param>
        /// <param name="i"></param>
        /// <returns></returns>
        double Sine(int D, int i)
        {
            double pi = 4 * Math.Atan(1.0);
            return Math.Sin((2*pi * i / D));
        }
        /****************************************************************/

        /*	SDA_SignalGenerate (pInput,				// Pointer to destination array 
						SIGLIB_IMPULSE,				//Signal type - Impulse function 
						SIGLIB_ONE,					// Signal peak level 
						SIGLIB_FILL,				// Fill (overwrite) or add to existing array contents 
						SIGLIB_ZERO,				// Signal frequency - Unused 
						SIGLIB_ZERO,				// D.C. Offset 
						SIGLIB_ZERO,				// Unused 
						SIGLIB_ZERO,				// Signal end value - Unused 
						SIGLIB_NULL_DATA_PTR,		// Unused 
						SIGLIB_NULL_DATA_PTR,		// Unused 
						SAMPLE_LENGTH);				// Output array length*/ 
         /*The signal frequency parameter specifies the frequency, 
        normalised to a sample rate of 1.0. Signal offset adds a specified DC offset to the 
        signal, before storing it. 
        freq = 1/SmapleLenght */
        public void SignalLibGenerator(ref float[] Data,SignalType Type,float PeakValue, float Frequency, float DCOffset,int SampleLenght)
        {
            int step = (int) Math.Round(SampleLenght/(1.0/Frequency));
            int n = 0;
            switch(Type)
            {
                case SignalType.SinusWave:
                    {
                        for (int N = 0; N < SampleLenght; N++)
                        {
                            Data[N] = (float)Sine(SampleLenght, n+=step) * PeakValue + DCOffset;
                        }
                       break;
                    }
                case SignalType.PerilinNoise:
                    {

                        for (int N = 0; N < SampleLenght; N++)
                        {
                            Data[N] = GetNoise((UInt32)N) * PeakValue + DCOffset;
                        }
                        break;
                    }
            }
        
        
        }

        /****************************************************************/
        private void trackAmp_Scroll(object sender, EventArgs e)
        {
            Amp = trackAmp.Value;
            Calc();
        }

        /****************************************************************/
        private void trackOffset_Scroll(object sender, EventArgs e)
        {
            Offset = trackOffset.Value;
            Calc();
        }
        /****************************************************************/
        private void trackFreq_Scroll(object sender, EventArgs e)
        {
            Freq = trackFreq.Value;
            Calc();
        }
        /****************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="SampleLenght"></param>
        /// <returns></returns>
        float Mean(float[] Data, int SampleLenght)
        {

            float sum = 0;
            for (int i = 0; i < SampleLenght; i++)
            {
                sum += Data[i];
            }
            return (sum / SampleLenght);
        }
        /****************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="SampleLenght"></param>
        /// <returns></returns>
        float Average (float[] Data, int SampleLenght)
        {

            float sum = 0;
            float n = 0;
            for (int i = 0; i < SampleLenght; i++)
            {
                if (Data[i] > 0)
                {
                    sum += Data[i];
                    n++;
                }
            }
            return (sum / n);
        }
        /****************************************************************/
        float Peak2Peak(float[] Data, int SampleLenght)
        {
            float biggest = 0;
            float smallest = 0;

            for (int i = 0; i < SampleLenght; i++)
            {
                if (Data[i] >= 0)
                {

                    //Biggesztől keresek nagyobbat
                    if (biggest > Data[i])
                    {
                        //Találtam nagyobbat
                        //Újra kezdem a vizsgálatot a nagyobb értékkel
                        biggest = Data[i];

                    }
                }
            }

            return biggest;
        }
        /****************************************************************/
        /// <summary>
        /// http://freespace.virgin.net/hugo.elias/models/m_perlin.htm
        /// </summary>
        /// <returns>A random value between 0 and 1 is assigned to every point on the X axis.</returns>
        float GetNoise(UInt32 x)
        {
             x = (x<<13) ^ x;
             return (float)( 1.0 - ( (x * (x * x * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0);
        
        }


    }
}
