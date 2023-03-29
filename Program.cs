global using MouseAndKeyboard.Util.Log;
global using System;
global using System.Collections.Generic;
global using System.Windows.Forms;

namespace MouseAndKeyboard;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

#if DEBUGs
        var client = new MainForm();

        var server = new MainForm();

        var finfo = server.GetType().GetField("chbReceiver", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var field = (CheckBox)finfo!.GetValue(server)!;
        field.Checked = true;

        Application.Run(new MultiFormContext(server, client));
#else
        Application.Run(new MainForm());
#endif

    }

}
