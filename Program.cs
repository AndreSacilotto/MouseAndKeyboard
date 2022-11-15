global using System;
global using System.Collections.Generic;
global using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MouseAndKeyboard;

internal static class Program
{
	[STAThread]
	static void Main()
	{
		ApplicationConfiguration.Initialize();
		Application.Run(new MainForm());
	}

}