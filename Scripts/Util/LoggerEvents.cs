﻿namespace MouseAndKeyboard.Util.Log;

public static class Logger
{
    public static bool Enabled { get; set; } = true;

    public static event Action<string>? OnLog;

    public static void Write(string log)
    {
        if (Enabled)
            OnLog?.Invoke(log);
    }

    public static void Write(object log)
    {
        if (Enabled)
            OnLog?.Invoke(log.ToString()!);
    }

    //[Conditional("DEBUG")]
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
