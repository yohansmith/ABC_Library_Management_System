using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Library_Management_System
{
   
    internal static class Program
    {    /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static string ActiveUser = string.Empty;
        public static string UserType = string.Empty;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Splash());
        }
    }
}
