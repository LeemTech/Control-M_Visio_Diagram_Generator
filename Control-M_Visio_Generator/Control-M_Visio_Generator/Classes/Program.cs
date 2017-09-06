using System;
using System.Windows.Forms;
using System.Diagnostics;
namespace Control_M_Visio_Generator
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
            Application.Run(new MainForm());

            //Kill all Visio threads
            foreach (var process in Process.GetProcessesByName("VISIO"))
            {
                process.Kill();
            }

            //Exit application
            Application.Exit();
        }
    }
}
