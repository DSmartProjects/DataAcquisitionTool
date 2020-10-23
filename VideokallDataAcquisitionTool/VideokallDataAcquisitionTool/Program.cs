using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideokallDataAcquisitionTool
{
    static class Program
    {
        private static Mutex mutexforSingleInstance = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string appName = "VKApp";
            bool createdNew = false; 
            mutexforSingleInstance = new Mutex(true, appName, out createdNew);
            if (!createdNew)
                return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new VKApp());
        }
    }
}
