global using System;
global using System.Collections.Generic;
global using System.Windows.Forms;

namespace MouseAndKeyboard;

internal static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	private static void Main()
	{
		ApplicationConfiguration.Initialize();
		Application.Run(new MainForm());
	}
}