using System;
using System.Windows.Forms;

namespace AtTheFront
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            var app = new NoneForm(args.Length != 0 ? args[0] : null);
            Application.Run();
        }
    }
}
