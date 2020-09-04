
namespace Konvolucio.MTOO180701.Moduls.RLC.SeriesRC
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using Common;

    public partial class Page : UserControl
    {

        VectorRatioDetector vrd = new VectorRatioDetector();

        public Page()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            if (!DesignMode)
                SetBinding();

            //var link = @"https://literature.cdn.keysight.com/litweb/pdf/5950-3000.pdf";
            //linkLabel1.Links.Add(0, link.Length, link);
            //linkLabel1.LinkClicked += (s, e) => { System.Diagnostics.Process.Start(e.Link.LinkData.ToString()); };

        }

        public void SetBinding()
        {
            paramControl1.DataBindings.Add("ParamValue", vrd, PropertyPlus.GetPropertyName(() => vrd.Param_a));
            paramControl1.ParamName = PropertyPlus.GetPropertyDescription(vrd, PropertyPlus.GetPropertyName(() => vrd.Param_a));

            paramControl2.DataBindings.Add("ParamValue", vrd, PropertyPlus.GetPropertyName(() => vrd.Param_b));
            paramControl2.ParamName = PropertyPlus.GetPropertyDescription(vrd, PropertyPlus.GetPropertyName(() => vrd.Param_b));

            paramControl3.DataBindings.Add("ParamValue", vrd, PropertyPlus.GetPropertyName(() => vrd.Param_c));
            paramControl3.ParamName = PropertyPlus.GetPropertyDescription(vrd, PropertyPlus.GetPropertyName(() => vrd.Param_c));

            paramControl4.DataBindings.Add("ParamValue", vrd, PropertyPlus.GetPropertyName(() => vrd.Param_d));
            paramControl4.ParamName = PropertyPlus.GetPropertyDescription(vrd, PropertyPlus.GetPropertyName(() => vrd.Param_d));

            paramControl5.DataBindings.Add("ParamValue", vrd, PropertyPlus.GetPropertyName(() => vrd.Param_Rr));
            paramControl5.ParamName = PropertyPlus.GetPropertyDescription(vrd, PropertyPlus.GetPropertyName(() => vrd.Param_Rr));

            paramControl6.DataBindings.Add("ParamValue", vrd, PropertyPlus.GetPropertyName(() => vrd.Param_f));
            paramControl6.ParamName = PropertyPlus.GetPropertyDescription(vrd, PropertyPlus.GetPropertyName(() => vrd.Param_f));

  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vrd.Calc();
            textBox1.Text = vrd.Result;
        }

    }
}
