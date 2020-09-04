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
    public partial class Graph2D : Form
    {
        const int GRAPH_HEIGHT = 100;
        const int GRAPH_WIDTH = 320;
        private float[] DataArray;
        public string Title;

        /****************************************************************/
        public float[] DataSource 
        {
            get { return DataArray; }
            set { 
                    DataArray = value;
                    GraphUpdate();
                }
        }
        /****************************************************************/
        public Graph2D()
        {
            InitializeComponent();
            DataArray = new float[GRAPH_WIDTH];
        }
        /****************************************************************/
        public void GraphUpdate()
        {
            window.Refresh(); 
        }
        /****************************************************************/
        private void window_Paint(object sender, PaintEventArgs e)
        {
            Graph2DDraw(e, DataArray, GRAPH_WIDTH);
        }
        /****************************************************************/
        void Graph2DDraw(PaintEventArgs e, float[] Data, int Lenght)
        {
            Graphics gfx = e.Graphics;
            for (int X = 0; X < GRAPH_WIDTH; X++)
            {
                float Y = Data[X];
                DrawPixel(e, X, GRAPH_HEIGHT - ((int)Y + (int)(GRAPH_HEIGHT/2)));
            }
        }

        /****************************************************************/
        /// <summary>
        /// Once again GDI+ resolution independence jumps up to bite you in the soft and tender parts. 
        /// There is no PlotPixel function in GDI+ that allows you to draw directly on the GDI Graphics surface as we used to be able to in GDI..
        /// To draw a dot of a specific colour on your screen you must resort to the following shenanigans.
        /// -Create a 1x1 bitmap image
        /// -Set its only pixel to the colour you desire using Bitmap.SetPixel
        /// -use Graphics.DrawImageUnscaled(...) to draw the dot in the correct place
        /// </summary>
        /// <param name="e"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        void DrawPixel(PaintEventArgs e, int X, int Y)
        {
            Bitmap bm = new Bitmap(1, 1);
            bm.SetPixel(0, 0, Color.White);
            e.Graphics.DrawImageUnscaled(bm, X, Y);
        }

    }
}
