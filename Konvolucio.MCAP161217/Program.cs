using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
/// <summary>
/// https://github.com/marrob/MCAP161217.git
/// </summary>
namespace Konvolucio.MCAP161217
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainView());
        }
    }
}
