using System;
using System.Windows.Forms;

namespace YuumiInstrumentation
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new Main());
            Application.Exit();
        }
    }
}
