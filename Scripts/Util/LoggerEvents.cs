using System.Linq;
using Microsoft.VisualBasic.Logging;

namespace MouseAndKeyboard.Util;

public static class LoggerEvents
{
	public static bool Enabled { get; set; } = true;

	public static event Action<string> OnLog;

	public static void Write(string log)
	{
		if (Enabled)
			OnLog?.Invoke(log);
	}
	public static void Write(object log)
	{
		if (Enabled)
			OnLog?.Invoke(log.ToString());
	}

	public static void WriteLine(string log)
	{
		if (Enabled)
			OnLog?.Invoke(log + Environment.NewLine);
	}
	public static void WriteLine(params object[] logs)
	{
		if (Enabled)
			OnLog?.Invoke(string.Join(' ', logs) + Environment.NewLine);
	}

}
