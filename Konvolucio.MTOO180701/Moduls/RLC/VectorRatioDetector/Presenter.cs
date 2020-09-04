
namespace Konvolucio.MTOO180701.Moduls.RLC.VectorRatioDetector
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using Common;

    public class Presenter : IModul
    {
        public string Name { get { return  "Vector Ratio Detector"; } }
        public string IconName { get { return  "ConverterIcon"; } }
        public string ToolTip { get { return "This is Converter"; } }
        public UserControl Page { get; private set; }
        public TreeNode TreeNode { get; private set; }

        public Presenter()
        {
            TreeNode = new Node(this);
            Page = new Page();
        }

        public class Node : TreeNode
        {
            public Node(IModul descriptor)
            {
                Text = descriptor.Name;
                Tag = descriptor;
            }
        }

    }
}
