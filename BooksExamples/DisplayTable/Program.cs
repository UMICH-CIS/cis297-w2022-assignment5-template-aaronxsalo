using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayTable
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application; forms application starts here
        /// </summary>
        /// <Student>Aaron Salo</Student>
        /// <Class>CIS297</Class>
        /// <Semester>Winter 2022</Semester>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DisplayAuthorsTable());
        }
    }
}
