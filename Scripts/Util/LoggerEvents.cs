namespace MouseAndKeyboard.Util;

public static class LoggerEvents
{
	public static event Action<string> OnLog;


	public static void Write(string log)
	{
		OnLog?.Invoke(log);
	}

	public static void WriteLine(string log)
	{
		OnLog?.Invoke(log + Environment.NewLine);
	}

}
